// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.Sql.Models;
using AzureMcp.Sql.Services.Models;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Sql.Services;

public class SqlService(ISubscriptionService subscriptionService, ITenantService tenantService, ILogger<SqlService> logger) : BaseAzureResourceService(subscriptionService, tenantService), ISqlService
{
    private readonly ILogger<SqlService> _logger = logger;

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
            var result = await ExecuteSingleResourceQueryAsync(
                "Microsoft.Sql/servers/databases",
                resourceGroup,
                subscription,
                retryPolicy,
                ConvertToSqlDatabaseModel,
                $"name =~ '{databaseName}'",
                cancellationToken);

            if (result == null)
            {
                throw new Exception($"SQL database '{databaseName}' not found in resource group '{resourceGroup}' for subscription '{subscription}'.");
            }

            return result;
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
            return await ExecuteResourceQueryAsync(
                "Microsoft.Sql/servers/databases",
                resourceGroup,
                subscription,
                retryPolicy,
                ConvertToSqlDatabaseModel,
                cancellationToken: cancellationToken);
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
            return await ExecuteResourceQueryAsync(
                "Microsoft.Sql/servers/administrators",
                resourceGroup,
                subscription,
                retryPolicy,
                ConvertToSqlServerEntraAdministratorModel,
                $"id contains '/servers/{serverName}/'",
                cancellationToken);
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
            return await ExecuteResourceQueryAsync(
                "Microsoft.Sql/servers/elasticPools",
                resourceGroup,
                subscription,
                retryPolicy,
                ConvertToSqlElasticPoolModel,
                cancellationToken: cancellationToken);
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
            return await ExecuteResourceQueryAsync(
                "Microsoft.Sql/servers/firewallRules",
                resourceGroup,
                subscription,
                retryPolicy,
                ConvertToSqlFirewallRuleModel,
                cancellationToken: cancellationToken);
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
