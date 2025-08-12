// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.ResourceManager.ResourceGraph;
using Azure.ResourceManager.ResourceGraph.Models;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Core.Services.Azure;

/// <summary>
/// Base class for Azure services that need to query Azure Resource Graph for resource management operations.
/// Provides common methods for executing resource queries against Azure Resource Manager resources.
/// </summary>
public abstract class BaseAzureResourceService(
    ISubscriptionService subscriptionService,
    ITenantService tenantService,
    ILoggerFactory? loggerFactory = null) : BaseAzureService(tenantService, loggerFactory)
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ITenantService _tenantService = tenantService ?? throw new ArgumentNullException(nameof(tenantService));

    /// <summary>
    /// Executes a Resource Graph query and returns a list of resources of the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert each resource to</typeparam>
    /// <param name="resourceType">The Azure resource type to query for (e.g., "Microsoft.Sql/servers/databases")</param>
    /// <param name="resourceGroup">The resource group name to filter by</param>
    /// <param name="subscription">The subscription ID or name</param>
    /// <param name="retryPolicy">Optional retry policy configuration</param>
    /// <param name="converter">Function to convert JsonElement to the target type</param>
    /// <param name="additionalFilter">Optional additional KQL filter conditions</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of resources converted to the specified type</returns>
    protected async Task<List<T>> ExecuteResourceQueryAsync<T>(
        string resourceType,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        Func<JsonElement, T> converter,
        string? additionalFilter = null,
        CancellationToken cancellationToken = default)
    {
        ValidateRequiredParameters(resourceType, resourceGroup, subscription);
        ArgumentNullException.ThrowIfNull(converter);

        var results = new List<T>();

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);
        var tenantResource = (await _tenantService.GetTenants()).FirstOrDefault(t => t.Data.TenantId == subscriptionResource.Data.TenantId);

        if (tenantResource == null)
        {
            throw new InvalidOperationException($"No accessible tenant found for subscription '{subscription}'");
        }

        var queryFilter = $"Resources | where type =~ '{EscapeKqlString(resourceType)}' and resourceGroup =~ '{EscapeKqlString(resourceGroup)}'";
        if (!string.IsNullOrEmpty(additionalFilter))
        {
            queryFilter += $" and {additionalFilter}";
        }

        var queryContent = new ResourceQueryContent(queryFilter)
        {
            Subscriptions = { subscriptionResource.Data.SubscriptionId }
        };

        ResourceQueryResult result = await tenantResource.GetResourcesAsync(queryContent, cancellationToken);
        if (result != null && result.Count > 0)
        {
            using var jsonDocument = JsonDocument.Parse(result.Data);
            var dataArray = jsonDocument.RootElement;
            if (dataArray.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataArray.EnumerateArray())
                {
                    results.Add(converter(item));
                }
            }
        }

        return results;
    }

    /// <summary>
    /// Executes a Resource Graph query and returns a single resource of the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert the resource to</typeparam>
    /// <param name="resourceType">The Azure resource type to query for (e.g., "Microsoft.Sql/servers/databases")</param>
    /// <param name="resourceGroup">The resource group name to filter by</param>
    /// <param name="subscription">The subscription ID or name</param>
    /// <param name="retryPolicy">Optional retry policy configuration</param>
    /// <param name="converter">Function to convert JsonElement to the target type</param>
    /// <param name="additionalFilter">Optional additional KQL filter conditions</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Single resource converted to the specified type, or null if not found</returns>
    protected async Task<T?> ExecuteSingleResourceQueryAsync<T>(
        string resourceType,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        Func<JsonElement, T> converter,
        string? additionalFilter = null,
        CancellationToken cancellationToken = default) where T : class
    {
        ValidateRequiredParameters(resourceType, resourceGroup, subscription);
        ArgumentNullException.ThrowIfNull(converter);

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);
        var tenantResource = (await _tenantService.GetTenants()).FirstOrDefault(t => t.Data.TenantId == subscriptionResource.Data.TenantId);

        if (tenantResource == null)
        {
            throw new InvalidOperationException($"No accessible tenant found for subscription '{subscription}'");
        }

        var queryFilter = $"Resources | where type =~ '{resourceType}' and resourceGroup =~ '{resourceGroup}'";
        if (!string.IsNullOrEmpty(additionalFilter))
        {
            queryFilter += $" and {additionalFilter}";
        }

        var queryContent = new ResourceQueryContent(queryFilter)
        {
            Subscriptions = { subscriptionResource.Data.SubscriptionId }
        };

        ResourceQueryResult result = await tenantResource.GetResourcesAsync(queryContent, cancellationToken);
        if (result != null && result.Count > 0)
        {
            using var jsonDocument = JsonDocument.Parse(result.Data);
            var dataArray = jsonDocument.RootElement;
            var item = dataArray.ValueKind == JsonValueKind.Array && dataArray.GetArrayLength() > 0
                ? dataArray[0]
                : default;
            if (item.ValueKind == JsonValueKind.Object)
            {
                return converter(item);
            }
        }
        return null;
    }
}
