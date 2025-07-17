// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Storage.Commands;
using AzureMcp.Areas.Storage.Options;
using AzureMcp.Areas.Storage.Options.DataLake;
using AzureMcp.Commands;

namespace AzureMcp.Areas.Storage.Commands.DataLake.Directory;

public abstract class BaseDirectoryCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : BaseStorageCommand<TOptions> where TOptions : BaseDirectoryOptions, new()
{
    protected readonly Option<string> _directoryOption = StorageOptionDefinitions.Directory;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_directoryOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Directory = parseResult.GetValueForOption(_directoryOption);
        return options;
    }
}