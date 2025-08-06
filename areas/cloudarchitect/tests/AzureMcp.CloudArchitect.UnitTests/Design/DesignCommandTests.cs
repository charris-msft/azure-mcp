// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.CloudArchitect.Commands.Design;
using AzureMcp.CloudArchitect.Options;
using AzureMcp.CloudArchitect;
using AzureMcp.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text;
using System.Text.Json;
using Xunit;

namespace AzureMcp.CloudArchitect.UnitTests.Design;

public class DesignCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DesignCommand> _logger;
    private readonly DesignCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public DesignCommandTests()
    {
        _logger = Substitute.For<ILogger<DesignCommand>>();

        var collection = new ServiceCollection();
        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("design", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("A tool for designing Azure cloud architectures through guided questions", command.Description);
    }

    [Fact]
    public void Command_HasCorrectOptions()
    {
        var command = _command.GetCommand();

        // Check that the command has the expected options
        var optionNames = command.Options.Select(o => o.Name).ToList();

        Assert.Contains("question", optionNames);
        Assert.Contains("question-number", optionNames);
        Assert.Contains("total-questions", optionNames);
        Assert.Contains("answer", optionNames);
        Assert.Contains("next-question-needed", optionNames);
        Assert.Contains("confidence-score", optionNames);
        Assert.Contains("architecture-component", optionNames);
    }

    [Theory]
    [InlineData("")]
    [InlineData("--question \"What is your application type?\"")]
    [InlineData("--question-number 1")]
    [InlineData("--total-questions 5")]
    [InlineData("--answer \"Web application\"")]
    [InlineData("--next-question-needed true")]
    [InlineData("--confidence-score 0.8")]
    [InlineData("--architecture-component \"Frontend\"")]
    [InlineData("--question \"App type?\" --question-number 1 --total-questions 5")]
    public async Task ExecuteAsync_ReturnsArchitectureDesignText(string args)
    {
        // Arrange
        var parseResult = _parser.Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Empty(response.Message);

        // Verify that results contain the architecture design text by serializing it
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        response.Results.Write(writer);
        writer.Flush();

        var serializedResult = Encoding.UTF8.GetString(stream.ToArray());
        var resultList = JsonSerializer.Deserialize(serializedResult, CloudArchitectJsonContext.Default.ListString);

        Assert.NotNull(resultList);
        Assert.Single(resultList);
        Assert.NotEmpty(resultList[0]);

        // Verify it contains some expected architecture-related content
        var architectureText = resultList[0];
        Assert.Contains("architecture", architectureText.ToLower());
    }

    [Fact]
    public async Task ExecuteAsync_ConsistentResults()
    {
        // Arrange
        var parseResult1 = _parser.Parse(["--question", "test question 1"]);
        var parseResult2 = _parser.Parse(["--question", "test question 2"]);

        // Act
        var response1 = await _command.ExecuteAsync(_context, parseResult1);
        var response2 = await _command.ExecuteAsync(_context, parseResult2);

        // Assert - Both calls should return the same architecture design text
        Assert.Equal(200, response1.Status);
        Assert.Equal(200, response2.Status);

        // Serialize both results to compare them
        string serializedResult1 = SerializeResponseResult(response1.Results!);
        string serializedResult2 = SerializeResponseResult(response2.Results!);

        Assert.Equal(serializedResult1, serializedResult2);
    }

    [Fact]
    public async Task ExecuteAsync_WithAllOptionsSet()
    {
        // Arrange
        var args = new[]
        {
            "--question", "What is your application type?",
            "--question-number", "1",
            "--total-questions", "5",
            "--answer", "Web application",
            "--next-question-needed", "true",
            "--confidence-score", "0.8",
            "--architecture-component", "Frontend"
        };

        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Empty(response.Message);

        // Verify the command executed successfully regardless of the input options
        string serializedResult = SerializeResponseResult(response.Results);
        var resultList = JsonSerializer.Deserialize(serializedResult, CloudArchitectJsonContext.Default.ListString);
        Assert.NotNull(resultList);
        Assert.Single(resultList);
        Assert.NotEmpty(resultList[0]);
    }

    [Fact]
    public void Metadata_IsConfiguredCorrectly()
    {
        // Arrange & Act
        var metadata = _command.Metadata;

        // Assert
        Assert.False(metadata.Destructive);
        Assert.True(metadata.ReadOnly);
    }

    private static string SerializeResponseResult(ResponseResult responseResult)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        responseResult.Write(writer);
        writer.Flush();
        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
