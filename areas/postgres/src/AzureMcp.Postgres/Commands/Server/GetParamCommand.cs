// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Postgres.Commands;
using AzureMcp.Postgres.Options;
using AzureMcp.Postgres.Options.Server;
using AzureMcp.Postgres.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Postgres.Commands.Server;

public sealed class GetParamCommand(ILogger<GetParamCommand> logger) : BaseServerCommand<GetParamOptions>(logger)
{
    private const string CommandTitle = "Get PostgreSQL Server Parameter";
    private readonly Option<string> _paramOption = PostgresOptionDefinitions.Param;
    public override string Name => "param";

    public override string Description =>
        "Retrieves a specific parameter of a PostgreSQL server. Can analyze replication status when querying replication-related parameters like wal_level.";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_paramOption);
    }

    protected override GetParamOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Param = parseResult.GetValueForOption(_paramOption);
        return options;
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var options = BindOptions(parseResult);

            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);

            IPostgresService pgService = context.GetService<IPostgresService>() ?? throw new InvalidOperationException("PostgreSQL service is not available.");
            
            // Check if this is a replication-related query
            if (IsReplicationQuery(options.Param))
            {
                var replicationStatus = await pgService.GetReplicationStatusAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!);
                context.Response.Results = replicationStatus?.Length > 0 ?
                    ResponseResult.Create(
                        new GetParamCommandResult(replicationStatus),
                        PostgresJsonContext.Default.GetParamCommandResult) :
                    null;
            }
            else
            {
                // Try to get the specific parameter
                try
                {
                    var parameterValue = await pgService.GetServerParameterAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!, options.Param!);
                    context.Response.Results = parameterValue?.Length > 0 ?
                        ResponseResult.Create(
                            new GetParamCommandResult(parameterValue),
                            PostgresJsonContext.Default.GetParamCommandResult) :
                        null;
                }
                catch (Exception paramEx) when (paramEx.Message.Contains("not found"))
                {
                    // If the parameter wasn't found and it might be a replication query, 
                    // try to provide replication analysis instead
                    if (IsLikelyReplicationQuery(options.Param))
                    {
                        _logger.LogInformation("Parameter '{Param}' not found, providing replication analysis instead", options.Param);
                        var replicationStatus = await pgService.GetReplicationStatusAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!);
                        context.Response.Results = replicationStatus?.Length > 0 ?
                            ResponseResult.Create(
                                new GetParamCommandResult(replicationStatus),
                                PostgresJsonContext.Default.GetParamCommandResult) :
                            null;
                    }
                    else
                    {
                        throw; // Re-throw the original exception if it's not replication-related
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred retrieving the parameter.");
            HandleException(context, ex);
        }
        return context.Response;
    }

    private static bool IsReplicationQuery(string? param)
    {
        if (string.IsNullOrEmpty(param))
            return false;

        // Direct replication parameter checks
        if (param.Equals("wal_level", StringComparison.OrdinalIgnoreCase))
            return true;

        // Check if the parameter name or description suggests replication analysis
        var replicationKeywords = new[] { "replication", "replica", "streaming" };
        return replicationKeywords.Any(keyword => param.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsLikelyReplicationQuery(string? param)
    {
        if (string.IsNullOrEmpty(param))
            return false;

        // More lenient check for queries that might be asking about replication but using different terms
        var replicationIndicators = new[] { "replication", "replica", "enabled", "enable", "status", "wal" };
        return replicationIndicators.Any(keyword => param.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    internal record GetParamCommandResult(string ParameterValue);
}
