// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Storage.Models;
using AzureMcp.Areas.Storage.Options.DataLake.Directory;
using AzureMcp.Areas.Storage.Services;
using AzureMcp.Commands.Storage;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Storage.Commands.DataLake.Directory;

public sealed class DirectoryListPathsCommand(ILogger<DirectoryListPathsCommand> logger) : BaseDirectoryCommand<ListPathsOptions>
{
    private const string CommandTitle = "List Data Lake Directory Paths";
    private readonly ILogger<DirectoryListPathsCommand> _logger = logger;

    public override string Name => "list-paths";

    public override string Description =>
        """
        List all paths in a Data Lake directory. This command retrieves and displays all paths (files and directories)
        available in the specified directory within a Data Lake storage account. The directory path should include the
        file system name (e.g., 'filesystem/directory'). Results include path names, types (file or directory), 
        and metadata, returned as a JSON array. Requires account-name and directory-name.
        """;

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);

            var storageService = context.GetService<IStorageService>();
            var paths = await storageService.ListDataLakeDirectoryPaths(
                options.Account!,
                options.Directory!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new DirectoryListPathsCommandResult(paths ?? []),
                StorageJsonContext.Default.DirectoryListPathsCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing Data Lake directory paths. Account: {Account}, Directory: {Directory}.", 
                options.Account, options.Directory);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record DirectoryListPathsCommandResult(List<DataLakePathInfo> Paths);
}