// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.CloudArchitect.Options;
using AzureMcp.CloudArchitect.Options.Design;
using AzureMcp.CloudArchitect.Services;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Models;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace AzureMcp.CloudArchitect.Commands.Design;

public sealed class DesignCommand(ILogger<DesignCommand> logger) : BaseCloudArchitectCommand<DesignOptions>
{
    private const string CommandTitle = "Design Azure cloud architectures through guided questions";
    private readonly ILogger<DesignCommand> _logger = logger;
    private readonly CloudArchitectService _cloudArchitectService = cloudArchitectService;

    // Define options from OptionDefinitions
    private readonly Option<string> _architectureDesignToolOptions = CloudArchitectOptionDefinitions.ArchitectureDesignToolOptions;

    private static readonly string s_designArchitectureText = LoadArchitectureDesignText();

    private static string GetArchitectureDesignText() => s_designArchitectureText;

    public override string Name => "design";

    public override string Description =>
        "A tool for designing Azure cloud architectures through guided questions.";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new()
    {
        Destructive = false,
        ReadOnly = true
    };

    private static string LoadArchitectureDesignText()
    {
        Assembly assembly = typeof(DesignCommand).Assembly;
        string resourceName = EmbeddedResourceHelper.FindEmbeddedResource(assembly, "azure-architecture-design.txt");
        return EmbeddedResourceHelper.ReadEmbeddedResource(assembly, resourceName);
    }


    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_architectureDesignToolOptions);
    }

    protected override DesignOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ArchitectDesignTool = parseResult.GetValueForOption(_architectureDesignToolOptions);
        return options;
    }

    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var designArchitecture = GetArchitectureDesignText();
        context.Response.Status = 200;
        context.Response.Results = ResponseResult.Create(new List<string> { designArchitecture }, CloudArchitectJsonContext.Default.ListString);
        context.Response.Message = string.Empty;
        return Task.FromResult(context.Response);
    }
}

// Strongly-typed result record
public record DesignCommandResult(AzureMcp.CloudArchitect.Models.ArchitectureDesign Design);
