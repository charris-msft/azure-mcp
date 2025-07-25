// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.ResourceGraph;
using Azure.ResourceManager.ResourceGraph.Models;
using Azure.ResourceManager.Resources;
using AzureMcp.Areas.Aks.Models;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Caching;
using System.Text.Json;

namespace AzureMcp.Areas.Aks.Services;

public sealed class AksService(
    ISubscriptionService subscriptionService,
    ITenantService tenantService,
    ICacheService cacheService) : BaseAzureService(tenantService), IAksService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));

    private const string CacheGroup = "aks";
    private const string AksClustersCacheKey = "clusters";
    private static readonly TimeSpan s_cacheDuration = TimeSpan.FromHours(1);

    public async Task<List<Cluster>> ListClusters(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscription);

        // Create cache key
        var cacheKey = string.IsNullOrEmpty(tenant)
            ? $"{AksClustersCacheKey}_{subscription}"
            : $"{AksClustersCacheKey}_{subscription}_{tenant}";

        // Try to get from cache first
        var cachedClusters = await _cacheService.GetAsync<List<Cluster>>(CacheGroup, cacheKey, s_cacheDuration);
        if (cachedClusters != null)
        {
            return cachedClusters;
        }

        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);
        var clusters = new List<Cluster>();

        try
        {
            var tenantResource = await GetTenantResourceAsync(tenant);
            var queryContent = new ResourceQueryContent("Resources | where type =~ 'Microsoft.ContainerService/managedClusters' | project id, name, type, location, tags, sku, properties | limit 20")
            {
                Subscriptions = { subscriptionResource.Data.SubscriptionId }
            };
            ResourceQueryResult result = await tenantResource.GetResourcesAsync(queryContent);
            if (result == null || result.Count == 0)
            {
                return clusters; // Return empty list if no clusters found
            }
#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
            var list = result.Data.ToObjectFromJson<List<IDictionary<string, string>>>();
            if (list == null || list.Count == 0)
            {
                return clusters; // Return empty list if no clusters found
            }
            foreach (var item in list)
            {
                clusters.Add(ConvertToClusterModel(item));
            }
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
            /*
            await foreach (var cluster in subscriptionResource.GetGenericResourcesAsync(filter: "resourceType eq 'Microsoft.ContainerService/managedClusters'"))
            {
                if (cluster?.Data != null)
                {
                    var resource = await cluster.GetAsync();
                    clusters.Add(ConvertToClusterModel(resource));
                }
            }
            */
            // Cache the results
            await _cacheService.SetAsync(CacheGroup, cacheKey, clusters, s_cacheDuration);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving AKS clusters: {ex.Message}", ex);
        }

        return clusters;
    }

    private static Cluster ConvertToClusterModel(IDictionary<string, string> item)
    {
        var clusterProperties = AksClusterProperties.FromJson(BinaryData.FromString(item["properties"]));
        var agentPool = clusterProperties?.AgentPoolProfiles?.FirstOrDefault();

        return new Cluster
        {
            Name = item["name"]?.ToString(),
            SubscriptionId = item["subscriptionId"]?.ToString(),
            ResourceGroupName = item["resourceGroupName"]?.ToString(),
            Location = item["location"]?.ToString(),
            IdentityType = item["identityType"]?.ToString(),
            ProvisioningState = clusterProperties?.ProvisioningState,
            SkuTier = item["Sku"]?.ToString(),
            Tags = ConvertToTagsDictionary(item["tags"]),
            KubernetesVersion = clusterProperties?.KubernetesVersion,
            PowerState = clusterProperties?.PowerState?.Code,
            DnsPrefix = clusterProperties?.DnsPrefix,
            Fqdn = clusterProperties?.Fqdn,
            NodeCount = agentPool?.Count,
            NodeVmSize = agentPool?.VmSize,
            EnableRbac = clusterProperties?.EnableRbac,
            NetworkPlugin = clusterProperties?.NetworkProfile?.NetworkPlugin?.ToString(),
            NetworkPolicy = clusterProperties?.NetworkProfile?.NetworkPolicy?.ToString(),
            ServiceCidr = clusterProperties?.NetworkProfile?.ServiceCidr,
            DnsServiceIP = clusterProperties?.NetworkProfile?.DnsServiceIP?.ToString()
        };
    }

    private static Dictionary<string, string> ConvertToTagsDictionary(string? tagsString)
    {
        if (string.IsNullOrEmpty(tagsString))
            return new Dictionary<string, string>();

        try
        {
#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
            var tags = JsonSerializer.Deserialize<Dictionary<string, string>>(tagsString, new JsonSerializerOptions { });
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
            return tags ?? new Dictionary<string, string>();
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }

    private static Cluster ConvertToClusterModel(GenericResource clusterResource)
    {
        // Retrieve all information about the resource
        var data = clusterResource.Get().Value.Data;

        var clusterProperties = AksClusterProperties.FromJson(data.Properties);
        var agentPool = clusterProperties?.AgentPoolProfiles?.FirstOrDefault();

        return new Cluster
        {
            Name = data.Name,
            SubscriptionId = clusterResource.Id.SubscriptionId,
            ResourceGroupName = clusterResource.Id.ResourceGroupName,
            Location = data.Location.ToString(),
            IdentityType = data.Identity?.ManagedServiceIdentityType.ToString(),
            ProvisioningState = clusterProperties?.ProvisioningState,
            SkuTier = data.Sku?.Tier?.ToString(),
            Tags = data.Tags?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
            KubernetesVersion = clusterProperties?.KubernetesVersion,
            PowerState = clusterProperties?.PowerState?.Code,
            DnsPrefix = clusterProperties?.DnsPrefix,
            Fqdn = clusterProperties?.Fqdn,
            NodeCount = agentPool?.Count,
            NodeVmSize = agentPool?.VmSize,
            EnableRbac = clusterProperties?.EnableRbac,
            NetworkPlugin = clusterProperties?.NetworkProfile?.NetworkPlugin?.ToString(),
            NetworkPolicy = clusterProperties?.NetworkProfile?.NetworkPolicy?.ToString(),
            ServiceCidr = clusterProperties?.NetworkProfile?.ServiceCidr,
            DnsServiceIP = clusterProperties?.NetworkProfile?.DnsServiceIP?.ToString()
        };
    }
}
