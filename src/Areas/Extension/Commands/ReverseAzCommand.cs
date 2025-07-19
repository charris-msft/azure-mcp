// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text;
using AzureMcp.Areas.Group.Commands;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Extension.Commands;

public sealed class ReverseAzCommand
{
    private readonly Dictionary<string, Func<CommandContext, string[], Task<CommandResponse>>> _azCommandHandlers;
    private readonly ILogger<ReverseAzCommand> _logger;

    public ReverseAzCommand(ILogger<ReverseAzCommand> logger)
    {
        _logger = logger;
        
        // Initialize command handlers
        _azCommandHandlers = new Dictionary<string, Func<CommandContext, string[], Task<CommandResponse>>>
        {
            ["group list"] = HandleGroupList,
        };
    }

    public async Task<CommandResponse?> ExecuteAzCommand(string commandLine, CommandContext context)
    {
        if (string.IsNullOrWhiteSpace(commandLine))
        {
            _logger.LogWarning("Command line is empty or null");
            return null;
        }

        commandLine = commandLine.Trim();

        // Find matching Azure command
        foreach (var (azCommand, handler) in _azCommandHandlers)
        {
            if (commandLine.StartsWith(azCommand, StringComparison.OrdinalIgnoreCase))
            {
                var remainingArgs = commandLine[azCommand.Length..].Trim();
                var args = string.IsNullOrWhiteSpace(remainingArgs) 
                    ? Array.Empty<string>() 
                    : ParseCommandLineArguments(remainingArgs);
                
                try
                {
                    var response = await handler(context, args);
                    _logger.LogInformation("Successfully executed Azure command '{AzCommand}'", azCommand);
                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing Azure command '{AzCommand}'", azCommand);
                    return CreateErrorResponse($"Error executing command: {ex.Message}", 500);
                }
            }
        }

        _logger.LogWarning("Azure command '{Command}' not recognized", commandLine);
        return CreateErrorResponse($"Command '{commandLine}' is not recognized", 404);
    }

    private async Task<CommandResponse> HandleGroupList(CommandContext context, string[] args)
    {
        var logger = context.GetService<ILogger<GroupListCommand>>();
        var command = new GroupListCommand(logger);
        var systemCommand = command.GetCommand();
        var parseResult = systemCommand.Parse(args);
        return await command.ExecuteAsync(context, parseResult);
    }

    private static CommandResponse CreateErrorResponse(string message, int statusCode)
    {
        return new CommandResponse
        {
            Status = statusCode,
            Message = message,
            Results = null
        };
    }

    private static string[] ParseCommandLineArguments(string commandLine)
    {
        var args = new List<string>();
        var currentArg = new StringBuilder();
        var inQuotes = false;
        var escapeNext = false;

        for (int i = 0; i < commandLine.Length; i++)
        {
            char c = commandLine[i];

            if (escapeNext)
            {
                currentArg.Append(c);
                escapeNext = false;
                continue;
            }

            if (c == '\\')
            {
                escapeNext = true;
                continue;
            }

            if (c == '"')
            {
                inQuotes = !inQuotes;
                continue;
            }

            if (c == ' ' && !inQuotes)
            {
                if (currentArg.Length > 0)
                {
                    args.Add(currentArg.ToString());
                    currentArg.Clear();
                }
                continue;
            }

            currentArg.Append(c);
        }

        if (currentArg.Length > 0)
        {
            args.Add(currentArg.ToString());
        }

        return args.ToArray();
    }
}
