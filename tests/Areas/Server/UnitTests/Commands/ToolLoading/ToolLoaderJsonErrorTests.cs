// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.UnitTests.Commands.ToolLoading;

[Trait("Area", "Server")]
public sealed class ToolLoaderJsonErrorTests
{
    [Fact]
    public async Task CompositeToolLoader_ShouldReturnValidJsonForUnknownTool()
    {
        // Arrange
        var mockLogger = Substitute.For<ILogger<CompositeToolLoader>>();
        var mockToolLoaders = new List<IToolLoader>();
        var compositeLoader = new CompositeToolLoader(mockToolLoaders, mockLogger);

        var mockServer = Substitute.For<IMcpServer>();
        var request = new RequestContext<CallToolRequestParams>(mockServer)
        {
            Params = new CallToolRequestParams
            {
                Name = "non_existent_tool",
                Arguments = new Dictionary<string, JsonElement>()
            }
        };

        // Act
        var result = await compositeLoader.CallToolHandler(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.NotNull(result.Content);
        Assert.Single(result.Content);
        
        var textContent = result.Content.First() as TextContentBlock;
        Assert.NotNull(textContent);
        
        // Verify the response is valid JSON
        Assert.True(IsValidJson(textContent.Text), $"Response should be valid JSON but was: {textContent.Text}");
        
        // Verify the JSON contains the error message
        using var doc = JsonDocument.Parse(textContent.Text);
        Assert.True(doc.RootElement.TryGetProperty("error", out var errorElement));
        Assert.Contains("non_existent_tool", errorElement.GetString());
        Assert.Contains("was not found", errorElement.GetString());
    }

    private static bool IsValidJson(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return false;

        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}