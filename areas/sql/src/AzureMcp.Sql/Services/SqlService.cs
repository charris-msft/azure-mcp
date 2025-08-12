// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.ResourceGraph;
using Azure.ResourceManager.ResourceGraph.Models;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Sql.Models;
using AzureMcp.Sql.Services.Models;
using Microsoft.Extensions.Logging;

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

            var queryContent = new ResourceQueryContent($"Resources | where type =~ 'Microsoft.Sql/servers/databases' and resourceGroup =~ '{resourceGroup}' and name =~ '{databaseName}'")
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

            var queryContent = new ResourceQueryContent($"Resources | where type =~ 'Microsoft.Sql/servers/databases' and resourceGroup =~ '{resourceGroup}'")
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

            var queryContent = new ResourceQueryContent($"Resources | where type =~ 'Microsoft.Sql/servers/administrators' and resourceGroup =~ '{resourceGroup}' and id contains '/servers/{serverName}/'")
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
            var elasticPools = new List<SqlElasticPool>();

            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);
            var tenantResource = (await _tenantService.GetTenants()).FirstOrDefault(t => t.Data.TenantId == subscriptionResource.Data.TenantId);

            var queryContent = new ResourceQueryContent($"Resources | where type =~ 'Microsoft.Sql/servers/elasticPools' and resourceGroup =~ '{resourceGroup}'")
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
                        elasticPools.Add(ConvertToSqlElasticPoolModel(item));
                    }
                }
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
            var firewallRules = new List<SqlServerFirewallRule>();

            var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);
            var tenantResource = (await _tenantService.GetTenants()).FirstOrDefault(t => t.Data.TenantId == subscriptionResource.Data.TenantId);

            var queryContent = new ResourceQueryContent($"Resources | where type =~ 'Microsoft.Sql/servers/firewallRules' and resourceGroup =~ '{resourceGroup}'")
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
                        firewallRules.Add(ConvertToSqlFirewallRuleModel(item));
                    }
                }
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

    private static SqlElasticPool ConvertToSqlElasticPoolModel(JsonElement item)
    {
        SqlElasticPoolData elasticPool = SqlElasticPoolData.FromJson(item);
        return new SqlElasticPool(
                    Name: elasticPool.ResourceName,
                    Id: elasticPool.ResourceId,
                    Type: elasticPool.ResourceType,
                    Location: elasticPool.Location,
                    Sku: elasticPool.Sku != null ? new ElasticPoolSku(
                        Name: elasticPool.Sku.Name,
                        Tier: elasticPool.Sku.Tier,
                        Capacity: elasticPool.Sku.Capacity,
                        Family: elasticPool.Sku.Family,
                        Size: elasticPool.Sku.Size
                    ) : null,
                    State: elasticPool.Properties.State,
                    CreationDate: elasticPool.Properties.CreatedOn,
                    MaxSizeBytes: elasticPool.Properties.MaxSizeBytes,
                    PerDatabaseSettings: elasticPool.Properties.PerDatabaseSettings != null ? new ElasticPoolPerDatabaseSettings(
                        MinCapacity: elasticPool.Properties.PerDatabaseSettings.MinCapacity,
                        MaxCapacity: elasticPool.Properties.PerDatabaseSettings.MaxCapacity
                    ) : null,
                    ZoneRedundant: elasticPool.Properties.IsZoneRedundant,
                    LicenseType: elasticPool.Properties.LicenseType,
                    DatabaseDtuMin: null, // DTU properties not available in current SDK
                    DatabaseDtuMax: null,
                    Dtu: null,
                    StorageMB: null
                );
    }

    private static SqlServerFirewallRule ConvertToSqlFirewallRuleModel(JsonElement item)
    {
        SqlFirewallRuleData firewallRule = SqlFirewallRuleData.FromJson(item);
        return new SqlServerFirewallRule(
            Name: firewallRule.ResourceName,
            Id: firewallRule.ResourceId,
            Type: firewallRule.ResourceType,
            StartIpAddress: firewallRule.Properties.StartIPAddress,
            EndIpAddress: firewallRule.Properties.EndIPAddress
        );
    }
}
