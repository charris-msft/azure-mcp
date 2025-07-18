// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Extension.Commands;
using AzureMcp.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AzureMcp.Core.Areas;

namespace AzureMcp.Extension;

internal sealed class ExtensionSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // No additional services needed for Extension area
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var extension = new CommandGroup("extension", "Extension commands for additional functionality");
        rootGroup.AddSubGroup(extension);

        extension.AddCommand("az", new AzCommand(loggerFactory.CreateLogger<AzCommand>()));
        extension.AddCommand("azd", new AzdCommand(loggerFactory.CreateLogger<AzdCommand>()));
        extension.AddCommand("azqr", new AzqrCommand(loggerFactory.CreateLogger<AzqrCommand>()));
    }
}
