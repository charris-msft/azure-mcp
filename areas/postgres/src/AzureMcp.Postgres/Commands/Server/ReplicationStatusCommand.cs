// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Postgres.Commands;
using AzureMcp.Postgres.Options.Server;
using AzureMcp.Postgres.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Postgres.Commands.Server;

public sealed class ReplicationStatusCommand(ILogger<ReplicationStatusCommand> logger) : BaseServerCommand<GetConfigOptions>(logger)
{
    private const string CommandTitle = "Get PostgreSQL Server Replication Status";
    public override string Name => "replication-status";

    public override string Description =>
        "Checks if replication is enabled on a PostgreSQL server by analyzing replication-related parameters.";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

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
            var replicationStatus = await pgService.GetReplicationStatusAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!);
            context.Response.Results = replicationStatus?.Length > 0 ?
                ResponseResult.Create(
                    new ReplicationStatusCommandResult(replicationStatus),
                    PostgresJsonContext.Default.ReplicationStatusCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred retrieving replication status.");
            HandleException(context, ex);
        }
        return context.Response;
    }
    internal record ReplicationStatusCommandResult(string ReplicationStatus);
}