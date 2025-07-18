// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Server.UnitTests;

internal class CommandFactoryHelpers
{
    public static CommandFactory CreateCommandFactory(IServiceProvider? serviceProvider = default)
    {
        IServiceProvider services = serviceProvider ?? new ServiceCollection()
            .AddLogging()
            .AddSingleton<Storage.StorageSetup>()
            .AddSingleton<KeyVault.KeyVaultSetup>()
            .AddSingleton<AppConfig.AppConfigSetup>()
            .AddSingleton<AzureBestPractices.AzureBestPracticesSetup>()
            .BuildServiceProvider();

        var logger = services.GetRequiredService<ILogger<CommandFactory>>();
        var telemetryService = services.GetService<ITelemetryService>() ?? new NoOpTelemetryService();
        var areaSetups = AppDomain.CurrentDomain.GetAssemblies()
           .SelectMany(a => a.GetTypes())
           .Where(t => typeof(IAreaSetup).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
           .Select(t => ActivatorUtilities.CreateInstance(services, t) as IAreaSetup)
           .OfType<IAreaSetup>()
           .ToArray();

        var commandFactory = new CommandFactory(services, areaSetups, telemetryService, logger);

        return commandFactory;
    }

    public class NoOpTelemetryService : ITelemetryService
    {
        public void Dispose()
        {
        }

        public Activity? StartActivity(string activityName)
        {
            return null;
        }

        public Activity? StartActivity(string activityName, Implementation? clientInfo)
        {
            return null;
        }
    }
}
