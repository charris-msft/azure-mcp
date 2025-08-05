// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Postgres.Commands.Server;
using AzureMcp.Postgres.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Postgres.UnitTests.Server;

public class ReplicationStatusCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPostgresService _postgresService;
    private readonly ILogger<ReplicationStatusCommand> _logger;

    public ReplicationStatusCommandTests()
    {
        _postgresService = Substitute.For<IPostgresService>();
        _logger = Substitute.For<ILogger<ReplicationStatusCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_postgresService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsReplicationStatus_WhenServerExists()
    {
        var expectedStatus = "PostgreSQL Server test-server Replication Status\n\nReplication is ENABLED";
        _postgresService.GetReplicationStatusAsync("sub123", "rg1", "user1", "server123").Returns(expectedStatus);

        var command = new ReplicationStatusCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--resource-group", "rg1", "--user-name", "user1", "--server", "server123"]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ReplicationStatusResult>(json);

        Assert.NotNull(result);
        Assert.Contains("Replication Status", result.Status);
    }

    [Theory]
    [InlineData("--subscription")]
    [InlineData("--resource-group")]
    [InlineData("--user-name")]
    [InlineData("--server")]
    public async Task ExecuteAsync_ReturnsError_WhenParameterIsMissing(string missingParameter)
    {
        var command = new ReplicationStatusCommand(_logger);
        var args = command.GetCommand().Parse(new string[]
        {
            missingParameter == "--subscription" ? "" : "--subscription", "sub123",
            missingParameter == "--resource-group" ? "" : "--resource-group", "rg1",
            missingParameter == "--user-name" ? "" : "--user-name", "user1",
            missingParameter == "--server" ? "" : "--server", "server123"
        });

        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Missing Required options: {missingParameter}", response.Message);
    }

    private class ReplicationStatusResult
    {
        [JsonPropertyName("ReplicationStatus")]
        public string Status { get; set; } = string.Empty;
    }
}