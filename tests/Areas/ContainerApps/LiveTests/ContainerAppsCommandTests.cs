// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Client;
using AzureMcp.Tests.Commands;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace AzureMcp.Tests.Areas.ContainerApps.LiveTests;

[Trait("Area", "ContainerApps")]
[Trait("Category", "Live")]
public class ContainerAppsCommandTests : CommandTestsBase, IClassFixture<LiveTestFixture>
{
    protected const string TenantNameReason = "Service principals cannot use TenantName for lookup";
    protected LiveTestSettings Settings { get; }
    protected StringBuilder FailureOutput { get; } = new();
    protected ITestOutputHelper Output { get; }
    protected IMcpClient Client { get; }

    public ContainerAppsCommandTests(LiveTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
        Client = fixture.Client;
        Settings = fixture.Settings;
        Output = output;
    }

    [Theory]
    [InlineData(AuthMethod.Credential)]
    public async Task Should_ListContainerApps_WithAuth(AuthMethod authMethod)
    {
        // Arrange
        var result = await CallToolAsync(
            "azmcp-containerapps-list",
            new()
            {
                { "subscription", Settings.Subscription },
                { "auth-method", authMethod.ToString().ToLowerInvariant() }
            });

        // Assert
        Assert.Equal(200, result.GetProperty("status").GetInt32());
        
        // Check if we have results (Container Apps might not exist in test subscription)
        if (result.TryGetProperty("results", out var resultsProperty) && resultsProperty.ValueKind != JsonValueKind.Null)
        {
            var containerApps = resultsProperty.GetProperty("containerApps");
            Assert.Equal(JsonValueKind.Array, containerApps.ValueKind);

            // If we have Container Apps, verify their structure
            foreach (var app in containerApps.EnumerateArray())
            {
                Assert.True(app.TryGetProperty("name", out _));
                Assert.True(app.TryGetProperty("location", out _));
            }
        }
    }

    [Theory]
    [InlineData("--invalid-param")]
    [InlineData("--subscription invalidSub")]
    public async Task Should_Return400_WithInvalidInput(string args)
    {
        var result = await CallToolAsync(
            $"azmcp-containerapps-list {args}");

        Assert.NotEqual(200, result.GetProperty("status").GetInt32());
    }

    [Fact]
    public async Task Should_ListContainerApps_WithResourceGroup()
    {
        // Test with a resource group filter (even if it doesn't exist)
        var result = await CallToolAsync(
            "azmcp-containerapps-list",
            new()
            {
                { "subscription", Settings.Subscription },
                { "resource-group", "non-existent-rg" }
            });

        // Should succeed even with empty results for non-existent resource group
        Assert.Equal(200, result.GetProperty("status").GetInt32());
    }
}