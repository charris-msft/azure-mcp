// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager.ResourceGraph;
using Azure.ResourceManager.ResourceGraph.Models;
using Azure.ResourceManager.Resources;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Sql.Models;
using AzureMcp.Sql.Services.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureMcp.Sql.Services;

public class SqlService(ISubscriptionService subscriptionService, ITenantService tenantService, ILogger<SqlService> logger) : BaseAzureService(tenantService), ISqlService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly ILogger<SqlService> _logger = logger;
    private readonly ITenantService _tenantService = tenantService ?? throw new ArgumentNullException(nameof(tenantService));

    public async Task<SqlDatabase?> GetDatabaseAsync(
        string serverName,
        string databaseName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);
            var tenantResource = (await _tenantService.GetTenants()).FirstOrDefault(t => t.Data.TenantId == subscriptionResource.Data.TenantId);

            var queryContent = new ResourceQueryContent($"Resources | where type =~ 'Microsoft.Sql/servers/databases' and resourceGroup =~ '{resourceGroup}' and name =~ '{databaseName}' | project id, name, type, location, sku, kind, managedBy, identity, tags, properties")
            {
                Subscriptions = { subscriptionResource.Data.SubscriptionId }
            };
            ResourceQueryResult result = await tenantResource.GetResourcesAsync(queryContent);
            if (result != null && result.Count > 0)
            {
                using var jsonDocument = JsonDocument.Parse(result.Data);
                var dataArray = jsonDocument.RootElement;
                var item = dataArray.ValueKind == JsonValueKind.Array && dataArray.GetArrayLength() > 0
                    ? dataArray[0]
                    : default;
                if (item.ValueKind == JsonValueKind.Object)
                {
                    return ConvertToSqlDatabaseModel(item);
                }
            }
            throw new Exception($"SQL database '{databaseName}' not found in resource group '{resourceGroup}' for subscription '{subscription}'.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting SQL database. Server: {Server}, Database: {Database}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, databaseName, resourceGroup, subscription);
            throw;
        }
    }

    public async Task<List<SqlDatabase>> ListDatabasesAsync(
        string serverName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var databases = new List<SqlDatabase>();

            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);
            var tenantResource = (await _tenantService.GetTenants()).FirstOrDefault(t => t.Data.TenantId == subscriptionResource.Data.TenantId);

            var queryContent = new ResourceQueryContent($"Resources | where type =~ 'Microsoft.Sql/servers/databases' and resourceGroup =~ '{resourceGroup}' | project id, name, type, location, sku, kind, managedBy, identity, tags, properties")
            {
                Subscriptions = { subscriptionResource.Data.SubscriptionId }
            };
            ResourceQueryResult result = await tenantResource.GetResourcesAsync(queryContent);
            if (result != null && result.Count > 0)
            {
                using var jsonDocument = JsonDocument.Parse(result.Data);
                var dataArray = jsonDocument.RootElement;
                if (dataArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in dataArray.EnumerateArray())
                    {
                        databases.Add(ConvertToSqlDatabaseModel(item));
                    }
                }
            }
            return databases;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error listing SQL databases. Server: {Server}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, resourceGroup, subscription);
            throw;
        }
    }

    public async Task<List<SqlServerEntraAdministrator>> GetEntraAdministratorsAsync(
        string serverName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var entraAdministrators = new List<SqlServerEntraAdministrator>();

            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);
            var tenantResource = (await _tenantService.GetTenants()).FirstOrDefault(t => t.Data.TenantId == subscriptionResource.Data.TenantId);

            var queryContent = new ResourceQueryContent($"Resources | where type =~ 'Microsoft.Sql/servers/administrators' and resourceGroup =~ '{resourceGroup}' and id contains '/servers/{serverName}/' | project id, name, type, location, sku, kind, managedBy, identity, tags, properties")
            {
                Subscriptions = { subscriptionResource.Data.SubscriptionId }
            };
            ResourceQueryResult result = await tenantResource.GetResourcesAsync(queryContent);
            if (result != null && result.Count > 0)
            {
                using var jsonDocument = JsonDocument.Parse(result.Data);
                var dataArray = jsonDocument.RootElement;
                if (dataArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in dataArray.EnumerateArray())
                    {
                        entraAdministrators.Add(ConvertToSqlServerEntraAdministratorModel(item));
                    }
                }
            }

            return entraAdministrators;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting SQL server Entra ID administrators. Server: {Server}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, resourceGroup, subscription);
            throw;
        }
    }

    public async Task<List<SqlElasticPool>> GetElasticPoolsAsync(
        string serverName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroup, cancellationToken);

            var sqlServerResource = await resourceGroupResource.Value
                .GetSqlServers()
                .GetAsync(serverName);

            var elasticPools = new List<SqlElasticPool>();

            await foreach (var poolResource in sqlServerResource.Value.GetElasticPools().GetAllAsync())
            {
                var pool = poolResource.Data;
                elasticPools.Add(new SqlElasticPool(
                    Name: pool.Name,
                    Id: pool.Id.ToString(),
                    Type: pool.ResourceType.ToString(),
                    Location: pool.Location.ToString(),
                    Sku: pool.Sku != null ? new ElasticPoolSku(
                        Name: pool.Sku.Name,
                        Tier: pool.Sku.Tier,
                        Capacity: pool.Sku.Capacity,
                        Family: pool.Sku.Family,
                        Size: pool.Sku.Size
                    ) : null,
                    State: pool.State?.ToString(),
                    CreationDate: pool.CreatedOn,
                    MaxSizeBytes: pool.MaxSizeBytes,
                    PerDatabaseSettings: pool.PerDatabaseSettings != null ? new ElasticPoolPerDatabaseSettings(
                        MinCapacity: pool.PerDatabaseSettings.MinCapacity,
                        MaxCapacity: pool.PerDatabaseSettings.MaxCapacity
                    ) : null,
                    ZoneRedundant: pool.IsZoneRedundant,
                    LicenseType: pool.LicenseType?.ToString(),
                    DatabaseDtuMin: null, // DTU properties not available in current SDK
                    DatabaseDtuMax: null,
                    Dtu: null,
                    StorageMB: null
                ));
            }

            return elasticPools;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting SQL elastic pools. Server: {Server}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, resourceGroup, subscription);
            throw;
        }
    }

    public async Task<List<SqlServerFirewallRule>> ListFirewallRulesAsync(
        string serverName,
        string resourceGroup,
        string subscription,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroup, cancellationToken);

            var sqlServerResource = await resourceGroupResource.Value
                .GetSqlServers()
                .GetAsync(serverName);

            var firewallRules = new List<SqlServerFirewallRule>();

            await foreach (var firewallRuleResource in sqlServerResource.Value.GetSqlFirewallRules().GetAllAsync(cancellationToken))
            {
                var rule = firewallRuleResource.Data;
                firewallRules.Add(new SqlServerFirewallRule(
                    Name: rule.Name,
                    Id: rule.Id.ToString(),
                    Type: rule.ResourceType.ToString() ?? "Unknown",
                    StartIpAddress: rule.StartIPAddress,
                    EndIpAddress: rule.EndIPAddress
                ));
            }

            return firewallRules;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting SQL server firewall rules. Server: {Server}, ResourceGroup: {ResourceGroup}, Subscription: {Subscription}",
                serverName, resourceGroup, subscription);
            throw;
        }
    }

    private static SqlDatabase ConvertToSqlDatabaseModel(JsonElement item)
    {
        SqlDatabaseData sqlDatabase = SqlDatabaseData.FromJson(item);

        return new SqlDatabase(
                Name: sqlDatabase.ResourceName,
                Id: sqlDatabase.ResourceId,
                Type: sqlDatabase.ResourceType,
                Location: sqlDatabase.Location,
                Sku: sqlDatabase.Sku != null ? new DatabaseSku(
                    Name: sqlDatabase.Sku.Name,
                    Tier: sqlDatabase.Sku.Tier,
                    Capacity: sqlDatabase.Sku.Capacity,
                    Family: sqlDatabase.Sku.Family,
                    Size: sqlDatabase.Sku.Size
                ) : null,
                Status: sqlDatabase.Properties.Status,
                Collation: sqlDatabase.Properties.Collation,
                CreationDate: sqlDatabase.Properties.CreatedOn,
                MaxSizeBytes: sqlDatabase.Properties.MaxSizeBytes,
                ServiceLevelObjective: sqlDatabase.Properties.CurrentServiceObjectiveName,
                Edition: sqlDatabase.Properties.CurrentSku?.Name,
                ElasticPoolName: sqlDatabase.Properties.ElasticPoolId?.ToString().Split('/').LastOrDefault(),
                EarliestRestoreDate: sqlDatabase.Properties.EarliestRestoreOn,
                ReadScale: sqlDatabase.Properties.ReadScale,
                ZoneRedundant: sqlDatabase.Properties.IsZoneRedundant
            );
    }
    
    private static SqlServerEntraAdministrator ConvertToSqlServerEntraAdministratorModel(JsonElement item)
    {
        SqlServerAadAdministratorData admin = SqlServerAadAdministratorData.FromJson(item);

        return new SqlServerEntraAdministrator(
                    Name: admin.ResourceName,
                    Id: admin.ResourceId,
                    Type: admin.ResourceType,
                    AdministratorType: admin.Properties.AdministratorType,
                    Login: admin.Properties.Login,
                    Sid: admin.Properties.Sid?.ToString(),
                    TenantId: admin.Properties.TenantId?.ToString(),
                    AzureADOnlyAuthentication: admin.Properties.IsAzureADOnlyAuthenticationEnabled
                );
    }
}
