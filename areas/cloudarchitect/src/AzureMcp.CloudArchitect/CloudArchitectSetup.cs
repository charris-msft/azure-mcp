// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.CloudArchitect.Commands.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.CloudArchitect;

public class CloudArchitectSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create CloudArchitect command group
        var cloudArchitect = new CommandGroup("cloudarchitect", "Cloud Architecture operations - Commands for generating Azure architecture designs and recommendations based on requirements.");
        rootGroup.AddSubGroup(cloudArchitect);

        // Register CloudArchitect commands
        cloudArchitect.AddCommand("design", new DesignCommand(
            loggerFactory.CreateLogger<DesignCommand>()));
    }
}
