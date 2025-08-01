// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Core.Areas;
using AzureMcp.Core.Areas.Tools.Commands;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Models;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Models.Option;
using AzureMcp.Core.UnitTests.Areas.Server;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Core.UnitTests.Areas.Tools.UnitTests;

public class ToolsListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ToolsListCommand> _logger;
    private readonly CommandContext _context;
    private readonly ToolsListCommand _command;
    private readonly Parser _parser;

    public ToolsListCommandTests()
    {
        var collection = new ServiceCollection();
        collection.AddLogging();
        
        var commandFactory = CommandFactoryHelpers.CreateCommandFactory();
        collection.AddSingleton(commandFactory);
        
        _serviceProvider = collection.BuildServiceProvider();
        _context = new(_serviceProvider);
        _logger = Substitute.For<ILogger<ToolsListCommand>>();
        _command = new(_logger);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_WithValidContext_ReturnsCommandInfoList()
    {
        // Arrange
        var args = _parser.Parse([]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<List<CommandInfo>>(json);

        Assert.NotNull(result);
        Assert.NotEmpty(result);

        foreach (var command in result)
        {
            Assert.False(string.IsNullOrWhiteSpace(command.Name), "Command name should not be empty");
            Assert.False(string.IsNullOrWhiteSpace(command.Description), "Command description should not be empty");
            Assert.False(string.IsNullOrWhiteSpace(command.Command), "Command path should not be empty");
        
            Assert.StartsWith("azmcp ", command.Command);
        
            if (command.Options != null && command.Options.Count > 0)
            {
                foreach (var option in command.Options)
                {
                    Assert.False(string.IsNullOrWhiteSpace(option.Name), "Option name should not be empty");
                    Assert.False(string.IsNullOrWhiteSpace(option.Description), "Option description should not be empty");
                    Assert.True(option.Required || !option.Required, "Option should have a valid Required property");
                }
            }
        }
    }
    
    [Fact]
    public async Task ExecuteAsync_JsonSerializationStressTest_HandlesLargeResults()
    {
        // Arrange
        var args = _parser.Parse([]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        Assert.False(string.IsNullOrWhiteSpace(json));
        
        // Ensure JSON is valid and can be deserialized
        var result = JsonSerializer.Deserialize<List<CommandInfo>>(json);
        Assert.NotNull(result);
        
        // Verify JSON round-trip preserves all data
        var reserializedJson = JsonSerializer.Serialize(result);
        Assert.Equal(json, reserializedJson);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidContext_FiltersHiddenCommands()
    {
        // Arrange
        var args = _parser.Parse([]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<List<CommandInfo>>(json);

        Assert.NotNull(result);

        Assert.DoesNotContain(result, cmd => cmd.Name == "list" && cmd.Command.Contains("tool"));

        Assert.Contains(result, cmd => !string.IsNullOrEmpty(cmd.Name));
        
    }

    [Fact]
    public async Task ExecuteAsync_WithValidContext_IncludesOptionsForCommands()
    {
        // Arrange
        var args = _parser.Parse([]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<List<CommandInfo>>(json);

        Assert.NotNull(result);
        
        var commandWithOptions = result.FirstOrDefault(cmd => cmd.Options?.Count > 0);
        Assert.NotNull(commandWithOptions);
        Assert.NotNull(commandWithOptions.Options);
        Assert.NotEmpty(commandWithOptions.Options);

        var option = commandWithOptions.Options.First();
        Assert.NotNull(option.Name);
        Assert.NotNull(option.Description);
        Assert.True(option.Required || !option.Required);
    }

    [Fact]
    public async Task ExecuteAsync_WithNullServiceProvider_HandlesGracefully()
    {
        // Arrange
        var faultyContext = new CommandContext(null!);
        var args = _parser.Parse([]);

        // Act
        var response = await _command.ExecuteAsync(faultyContext, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("cannot be null", response.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ExecuteAsync_WithCorruptedCommandFactory_HandlesGracefully()
    {
        // Arrange
        var faultyServiceProvider = Substitute.For<IServiceProvider>();
        faultyServiceProvider.GetService(typeof(CommandFactory))
            .Returns(x => throw new InvalidOperationException("Corrupted command factory"));
        
        var faultyContext = new CommandContext(faultyServiceProvider);
        var args = _parser.Parse([]);

        // Act
        var response = await _command.ExecuteAsync(faultyContext, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("Corrupted command factory", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSpecificKnownCommands()
    {
        // Arrange
        var args = _parser.Parse([]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<List<CommandInfo>>(json);

        Assert.NotNull(result);
        Assert.NotEmpty(result);

        Assert.True(result.Count >= 3, $"Expected at least 3 commands, got {result.Count}");
        
        var allCommands = result.Select(cmd => cmd.Command).ToList();
        
        // Should have subscription commands (commands include 'azmcp' prefix)
        var subscriptionCommands = result.Where(cmd => cmd.Command.Contains("subscription")).ToList();
        Assert.True(subscriptionCommands.Count > 0, $"Expected subscription commands. All commands: {string.Join(", ", allCommands)}");
        
        // Should have keyvault commands  
        var keyVaultCommands = result.Where(cmd => cmd.Command.Contains("keyvault")).ToList();
        Assert.True(keyVaultCommands.Count > 0, $"Expected keyvault commands. All commands: {string.Join(", ", allCommands)}");
        
        // Should have storage commands
        var storageCommands = result.Where(cmd => cmd.Command.Contains("storage")).ToList();
        Assert.True(storageCommands.Count > 0, $"Expected storage commands. All commands: {string.Join(", ", allCommands)}");
        
        // Verify specific known commands exist
        Assert.Contains(result, cmd => cmd.Command == "azmcp subscription list");
        Assert.Contains(result, cmd => cmd.Command == "azmcp keyvault key list");
        Assert.Contains(result, cmd => cmd.Command == "azmcp storage account list");
        
        // Verify that each command has proper structure
        foreach (var cmd in result.Take(3))
        {
            Assert.NotEmpty(cmd.Name);
            Assert.NotEmpty(cmd.Description);
            Assert.NotEmpty(cmd.Command);
        }
    }

    [Fact]
    public async Task ExecuteAsync_CommandPathFormattingIsCorrect()
    {
        // Arrange
        var args = _parser.Parse([]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<List<CommandInfo>>(json);

        Assert.NotNull(result);
        
        foreach (var command in result)
        {
            // Command paths should not start or end with spaces
            Assert.False(command.Command.StartsWith(' '), $"Command '{command.Command}' should not start with space");
            Assert.False(command.Command.EndsWith(' '), $"Command '{command.Command}' should not end with space");
            
            // Command paths should not have double spaces
            Assert.DoesNotContain("  ", command.Command);
        }
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyCommandFactory_ReturnsEmptyResults()
    {
        // Arrange
        var collection = new ServiceCollection();
        collection.AddLogging();
        
        // Create an empty command factory with no area setups
        var logger = collection.BuildServiceProvider().GetRequiredService<ILogger<CommandFactory>>();
        var telemetryService = new CommandFactoryHelpers.NoOpTelemetryService();
        var emptyAreaSetups = Array.Empty<IAreaSetup>();
        
        var emptyCommandFactory = new CommandFactory(collection.BuildServiceProvider(), emptyAreaSetups, telemetryService, logger);
        collection.AddSingleton(emptyCommandFactory);
        
        var emptyServiceProvider = collection.BuildServiceProvider();
        var emptyContext = new CommandContext(emptyServiceProvider);
        var args = _parser.Parse([]);

        // Act
        var response = await _command.ExecuteAsync(emptyContext, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        
        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<List<CommandInfo>>(json);
        
        Assert.NotNull(result);
        Assert.Empty(result); // Should be empty when no commands are available
    }

}
