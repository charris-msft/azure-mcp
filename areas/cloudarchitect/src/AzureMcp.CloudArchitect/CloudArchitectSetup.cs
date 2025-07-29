// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.CloudArchitect.Commands;
using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
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
        // Register Cloud Architect command at the root level
        var cloudArchitect = new CommandGroup(
            "cloudarchitect",
            "A tool for designing Azure cloud architectures through guided questions.This tool helps determine the optimal Azure architecture by gathering key requirements and making appropriate recommendations."
        );
        rootGroup.AddSubGroup(cloudArchitect);

        cloudArchitect.AddCommand(
            "get",
            new CloudArchitectCommand(loggerFactory.CreateLogger<CloudArchitectCommand>())
        );

        cloudArchitect.AddCommand(
            "design-architecture",
            new ArchitectureDesignCommand(loggerFactory.CreateLogger<ArchitectureDesignCommand>())
        );
    }
}
