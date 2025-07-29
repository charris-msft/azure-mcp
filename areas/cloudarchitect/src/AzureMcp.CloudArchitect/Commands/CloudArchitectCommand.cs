// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using AzureMcp.CloudArchitect.Options;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Helpers;
using Microsoft.Extensions.Logging;

namespace AzureMcp.CloudArchitect.Commands;

public sealed class CloudArchitectCommand(ILogger<CloudArchitectCommand> logger) : BaseCommand
{
    private const string CommandTitle = "Get Azure Cloud Architecture";
    private readonly ILogger<CloudArchitectCommand> _logger = logger;
    private static readonly Dictionary<string, string> s_cloudArchitectCache = new();

    private readonly Option<string> _resourceOption = CloudArchitectOptionDefinitions.Resource;
    private readonly Option<string> _actionOption = CloudArchitectOptionDefinitions.Action;

    public override string Name => "get";

    public override string Description =>
        "Returns best practices for secure, production-grade Azure usage. " +
        "Call this command and follow its guidance before generating or suggesting any Azure code. " +
        "This command returns the content of the best practices file as a string array.";

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        command.AddOption(_resourceOption);
        command.AddOption(_actionOption);
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var validationResult = Validate(parseResult.CommandResult, context.Response);
            if (!validationResult.IsValid)
            {
                return Task.FromResult(context.Response);
            }

            var resource = parseResult.GetValueForOption(_resourceOption);
            var action = parseResult.GetValueForOption(_actionOption);

            var resourceFileName = GetResourceFileName(resource!, action!);
            var cloudArchitect = GetCloudArchitectText(resourceFileName);

            context.Response.Status = 200;
            context.Response.Results = ResponseResult.Create(new List<string> { cloudArchitect }, CloudArchitectJsonContext.Default.ListString);
            context.Response.Message = string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting cloud architecture for Resource: {Resource}, Action: {Action}",
                parseResult.GetValueForOption(_resourceOption), parseResult.GetValueForOption(_actionOption));
            HandleException(context, ex);
        }

        return Task.FromResult(context.Response);
    }

    public override ValidationResult Validate(CommandResult commandResult, CommandResponse? commandResponse = null)
    {
        var validationResult = new ValidationResult { IsValid = true };

        var resource = commandResult.GetValueForOption(CloudArchitectOptionDefinitions.Resource);
        var action = commandResult.GetValueForOption(CloudArchitectOptionDefinitions.Action);

        if (string.IsNullOrEmpty(resource) || string.IsNullOrEmpty(action))
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Both resource and action parameters are required.";
        }
        else if (resource != "general" && resource != "azurefunctions" && resource != "static-web-app")
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Invalid resource. Must be 'general', 'azurefunctions', or 'static-web-app'.";
        }
        else if (action != "all" && action != "code-generation" && action != "deployment")
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Invalid action. Must be 'all', 'code-generation' or 'deployment'.";
        }
        else if ((resource == "general" || resource == "azurefunctions") && action == "all")
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "The 'general' or 'azurefunctions' resource doesn't support 'all' action.";
        }
        else if (resource == "static-web-app" && action != "all")
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "The 'static-web-app' resource only supports 'all' action.";
        }

        if (!validationResult.IsValid && commandResponse != null)
        {
            commandResponse.Status = 400;
            commandResponse.Message = validationResult.ErrorMessage!;
        }

        return validationResult;
    }

    private static string GetResourceFileName(string resource, string action)
    {
        return (resource, action) switch
        {
            ("general", "code-generation") => "azure-general-codegen-best-practices.txt",
            ("general", "deployment") => "azure-general-deployment-best-practices.txt",
            ("azurefunctions", "code-generation") => "azure-functions-codegen-best-practices.txt",
            ("azurefunctions", "deployment") => "azure-functions-deployment-best-practices.txt",
            ("static-web-app", "all") => "azure-swa-best-practices.txt",
            _ => throw new ArgumentException($"Invalid combination of resource '{resource}' and action '{action}'")
        };
    }

    private string GetCloudArchitectText(string resourceFileName)
    {
        if (!s_cloudArchitectCache.TryGetValue(resourceFileName, out string? cloudArchitect))
        {
            cloudArchitect = LoadCloudArchitectText(resourceFileName);
            s_cloudArchitectCache[resourceFileName] = cloudArchitect;
        }
        return cloudArchitect;
    }

    private string LoadCloudArchitectText(string resourceFileName)
    {
        Assembly assembly = typeof(CloudArchitectCommand).Assembly;
        string resourceName = EmbeddedResourceHelper.FindEmbeddedResource(assembly, resourceFileName);
        return EmbeddedResourceHelper.ReadEmbeddedResource(assembly, resourceName);
    }
}
