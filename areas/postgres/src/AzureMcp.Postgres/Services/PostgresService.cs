// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.Core;
using Azure.ResourceManager.PostgreSql.FlexibleServers;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.ResourceGroup;
using Npgsql;

namespace AzureMcp.Postgres.Services;

public class PostgresService : BaseAzureService, IPostgresService
{
    private readonly IResourceGroupService _resourceGroupService;
    private string? _cachedEntraIdAccessToken;
    private DateTime _tokenExpiryTime;

    public PostgresService(IResourceGroupService resourceGroupService)
    {
        _resourceGroupService = resourceGroupService ?? throw new ArgumentNullException(nameof(resourceGroupService));
    }

    private async Task<string> GetEntraIdAccessTokenAsync()
    {
        if (_cachedEntraIdAccessToken != null && DateTime.UtcNow < _tokenExpiryTime)
        {
            return _cachedEntraIdAccessToken;
        }

        var tokenRequestContext = new TokenRequestContext(new[] { "https://ossrdbms-aad.database.windows.net/.default" });
        var tokenCredential = await GetCredential();
        var accessToken = await tokenCredential
            .GetTokenAsync(tokenRequestContext, CancellationToken.None)
            .ConfigureAwait(false);
        _cachedEntraIdAccessToken = accessToken.Token;
        _tokenExpiryTime = accessToken.ExpiresOn.UtcDateTime.AddSeconds(-60); // Subtract 60 seconds as a buffer.

        return _cachedEntraIdAccessToken;
    }

    private static string NormalizeServerName(string server)
    {
        if (!server.Contains('.'))
        {
            return server + ".postgres.database.azure.com";
        }
        return server;
    }

    public async Task<List<string>> ListDatabasesAsync(string subscriptionId, string resourceGroup, string user, string server)
    {
        var entraIdAccessToken = await GetEntraIdAccessTokenAsync();
        var host = NormalizeServerName(server);
        var connectionString = $"Host={host};Database=postgres;Username={user};Password={entraIdAccessToken}";

        await using var resource = await PostgresResource.CreateAsync(connectionString);
        var query = "SELECT datname FROM pg_database WHERE datistemplate = false;";
        await using var command = new NpgsqlCommand(query, resource.Connection);
        await using var reader = await command.ExecuteReaderAsync();
        var dbs = new List<string>();
        while (await reader.ReadAsync())
        {
            dbs.Add(reader.GetString(0));
        }
        return dbs;
    }

    public async Task<List<string>> ExecuteQueryAsync(string subscriptionId, string resourceGroup, string user, string server, string database, string query)
    {
        var entraIdAccessToken = await GetEntraIdAccessTokenAsync();
        var host = NormalizeServerName(server);
        var connectionString = $"Host={host};Database={database};Username={user};Password={entraIdAccessToken}";

        await using var resource = await PostgresResource.CreateAsync(connectionString);
        await using var command = new NpgsqlCommand(query, resource.Connection);
        await using var reader = await command.ExecuteReaderAsync();

        var rows = new List<string>();

        var columnNames = Enumerable.Range(0, reader.FieldCount)
                               .Select(reader.GetName)
                               .ToArray();
        rows.Add(string.Join(", ", columnNames));
        while (await reader.ReadAsync())
        {
            var row = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                row.Add(reader[i]?.ToString() ?? "NULL");
            }
            rows.Add(string.Join(", ", row));
        }
        return rows;
    }

    public async Task<List<string>> ListTablesAsync(string subscriptionId, string resourceGroup, string user, string server, string database)
    {
        var entraIdAccessToken = await GetEntraIdAccessTokenAsync();
        var host = NormalizeServerName(server);
        var connectionString = $"Host={host};Database={database};Username={user};Password={entraIdAccessToken}";

        await using var resource = await PostgresResource.CreateAsync(connectionString);
        var query = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';";
        await using var command = new NpgsqlCommand(query, resource.Connection);
        await using var reader = await command.ExecuteReaderAsync();
        var tables = new List<string>();
        while (await reader.ReadAsync())
        {
            tables.Add(reader.GetString(0));
        }
        return tables;
    }

    public async Task<List<string>> GetTableSchemaAsync(string subscriptionId, string resourceGroup, string user, string server, string database, string table)
    {
        var entraIdAccessToken = await GetEntraIdAccessTokenAsync();
        var host = NormalizeServerName(server);
        var connectionString = $"Host={host};Database={database};Username={user};Password={entraIdAccessToken}";

        await using var resource = await PostgresResource.CreateAsync(connectionString);
        var query = $"SELECT column_name, data_type FROM information_schema.columns WHERE table_name = '{table}';";
        await using var command = new NpgsqlCommand(query, resource.Connection);
        await using var reader = await command.ExecuteReaderAsync();
        var schema = new List<string>();
        while (await reader.ReadAsync())
        {
            schema.Add($"{reader.GetString(0)}: {reader.GetString(1)}");
        }
        return schema;
    }

    public async Task<List<string>> ListServersAsync(string subscriptionId, string resourceGroup, string user)
    {
        var rg = await _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup);
        if (rg == null)
        {
            throw new Exception($"Resource group '{resourceGroup}' not found.");
        }
        var serverList = new List<string>();
        await foreach (PostgreSqlFlexibleServerResource server in rg.GetPostgreSqlFlexibleServers().GetAllAsync())
        {
            serverList.Add(server.Data.Name);
        }
        return serverList;
    }

    public async Task<string> GetServerConfigAsync(string subscriptionId, string resourceGroup, string user, string server)
    {
        var rg = await _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup);
        if (rg == null)
        {
            throw new Exception($"Resource group '{resourceGroup}' not found.");
        }
        var pgServer = await rg.GetPostgreSqlFlexibleServerAsync(server);
        var pgServerData = pgServer.Value.Data;
        var result = $"Server Name: {pgServerData.Name}\n" +
                 $"Location: {pgServerData.Location}\n" +
                 $"Version: {pgServerData.Version}\n" +
                 $"SKU: {pgServerData.Sku?.Name}\n" +
                 $"Storage Size (GB): {pgServerData.Storage?.StorageSizeInGB}\n" +
                 $"Backup Retention Days: {pgServerData.Backup?.BackupRetentionDays}\n" +
                 $"Geo-Redundant Backup: {pgServerData.Backup?.GeoRedundantBackup}";
        return result;
    }

    public async Task<string> GetServerParameterAsync(string subscriptionId, string resourceGroup, string user, string server, string param)
    {
        var rg = await _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup);
        if (rg == null)
        {
            throw new Exception($"Resource group '{resourceGroup}' not found.");
        }
        var pgServer = await rg.GetPostgreSqlFlexibleServerAsync(server);

        var configResponse = await pgServer.Value.GetPostgreSqlFlexibleServerConfigurationAsync(param);
        if (configResponse?.Value?.Data == null)
        {
            throw new Exception($"Parameter '{param}' not found.");
        }
        return configResponse.Value.Data.Value;
    }

    public async Task<string> SetServerParameterAsync(string subscriptionId, string resourceGroup, string user, string server, string param, string value)
    {
        var rg = await _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup);
        if (rg == null)
        {
            throw new Exception($"Resource group '{resourceGroup}' not found.");
        }
        var pgServer = await rg.GetPostgreSqlFlexibleServerAsync(server);

        var configResponse = await pgServer.Value.GetPostgreSqlFlexibleServerConfigurationAsync(param);
        if (configResponse?.Value?.Data == null)
        {
            throw new Exception($"Parameter '{param}' not found.");
        }

        var configData = new PostgreSqlFlexibleServerConfigurationData
        {
            Value = value,
            Source = "user-override"
        };

        var updateOperation = await configResponse.Value.UpdateAsync(WaitUntil.Completed, configData);
        if (updateOperation.HasCompleted && updateOperation.HasValue)
        {
            return $"Parameter '{param}' updated successfully to '{value}'.";
        }
        else
        {
            throw new Exception($"Failed to update parameter '{param}' to value '{value}'.");
        }
    }

    public async Task<string> GetReplicationStatusAsync(string subscriptionId, string resourceGroup, string user, string server)
    {
        var rg = await _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup);
        if (rg == null)
        {
            throw new Exception($"Resource group '{resourceGroup}' not found.");
        }
        var pgServer = await rg.GetPostgreSqlFlexibleServerAsync(server);

        // Get replication-related parameters
        var walLevelTask = GetServerParameterValueAsync(pgServer.Value, "wal_level");
        var maxWalSendersTask = GetServerParameterValueAsync(pgServer.Value, "max_wal_senders");
        var maxReplicationSlotsTask = GetServerParameterValueAsync(pgServer.Value, "max_replication_slots");

        await Task.WhenAll(walLevelTask, maxWalSendersTask, maxReplicationSlotsTask);

        var walLevel = walLevelTask.Result;
        var maxWalSenders = maxWalSendersTask.Result;
        var maxReplicationSlots = maxReplicationSlotsTask.Result;

        // Analyze replication status
        var isReplicationEnabled = IsReplicationEnabled(walLevel);
        var replicationStatus = isReplicationEnabled ? "ENABLED" : "NOT ENABLED";

        var result = $"PostgreSQL Server {server} Replication Status\n\n";
        result += $"Based on the parameter checks for your PostgreSQL server {server} in resource group {resourceGroup}, here's the replication configuration:\n\n";
        
        if (isReplicationEnabled)
        {
            result += "✅ Replication is ENABLED\n\n";
            result += "Key Replication Parameters:\n\n";
            result += $"1. wal_level: {walLevel}\n";
            result += "   ○ This setting enables replication functionality\n";
            
            if (walLevel.Equals("replica", StringComparison.OrdinalIgnoreCase))
            {
                result += "   ○ 'replica' level allows for streaming replication and point-in-time recovery\n\n";
            }
            else if (walLevel.Equals("logical", StringComparison.OrdinalIgnoreCase))
            {
                result += "   ○ 'logical' level allows for logical replication in addition to streaming replication\n\n";
            }

            if (int.TryParse(maxWalSenders, out var walSenders) && walSenders > 0)
            {
                result += $"2. max_wal_senders: {maxWalSenders}\n";
                result += $"   ○ Allows up to {maxWalSenders} concurrent WAL sender processes\n";
                result += $"   ○ This means the server can support up to {maxWalSenders} replica connections\n\n";
            }

            if (int.TryParse(maxReplicationSlots, out var replicationSlots) && replicationSlots > 0)
            {
                result += $"3. max_replication_slots: {maxReplicationSlots}\n";
                result += $"   ○ Supports up to {maxReplicationSlots} replication slots\n";
                result += "   ○ Replication slots ensure WAL files are retained for replicas\n\n";
            }

            result += "Summary\n\n";
            result += $"Your PostgreSQL server {server} has replication enabled and properly configured. The server is set up to:\n\n";
            result += "• Support streaming replication\n";
            if (walSenders > 0)
            {
                result += $"• Handle up to {walSenders} concurrent replica connections\n";
            }
            if (replicationSlots > 0)
            {
                result += $"• Maintain replication slots for reliable replica synchronization\n";
            }

            if (walLevel.Equals("replica", StringComparison.OrdinalIgnoreCase))
            {
                result += $"\nThe wal_level = {walLevel} setting is the key indicator that replication is enabled on your PostgreSQL server.";
            }
        }
        else
        {
            result += "❌ Replication is NOT ENABLED\n\n";
            result += "Current Configuration:\n\n";
            result += $"• wal_level: {walLevel}\n";
            result += "  ○ Current level does not support replication\n";
            result += "  ○ For replication, wal_level should be set to 'replica' or 'logical'\n\n";
            
            result += "To enable replication:\n\n";
            result += "1. Set wal_level to 'replica' for streaming replication\n";
            result += "2. Configure max_wal_senders (typically 3-10)\n";
            result += "3. Set max_replication_slots if needed\n";
            result += "4. Restart the PostgreSQL server\n\n";
            
            result += $"Use the parameter update tools to modify wal_level from '{walLevel}' to 'replica' to enable replication.";
        }

        return result;
    }

    private async Task<string> GetServerParameterValueAsync(PostgreSqlFlexibleServerResource pgServer, string paramName)
    {
        try
        {
            var configResponse = await pgServer.GetPostgreSqlFlexibleServerConfigurationAsync(paramName);
            return configResponse?.Value?.Data?.Value ?? "unknown";
        }
        catch
        {
            return "unknown";
        }
    }

    private static bool IsReplicationEnabled(string walLevel)
    {
        return walLevel.Equals("replica", StringComparison.OrdinalIgnoreCase) ||
               walLevel.Equals("logical", StringComparison.OrdinalIgnoreCase);
    }

    private sealed class PostgresResource : IAsyncDisposable
    {
        public NpgsqlConnection Connection { get; }
        private readonly NpgsqlDataSource _dataSource;

        public static async Task<PostgresResource> CreateAsync(string connectionString)
        {
            var dataSource = new NpgsqlSlimDataSourceBuilder(connectionString)
                .EnableTransportSecurity()
                .Build();
            var connection = await dataSource.OpenConnectionAsync();
            return new PostgresResource(dataSource, connection);
        }

        public async ValueTask DisposeAsync()
        {
            await Connection.DisposeAsync();
            await _dataSource.DisposeAsync();
        }

        private PostgresResource(NpgsqlDataSource dataSource, NpgsqlConnection connection)
        {
            _dataSource = dataSource;
            Connection = connection;
        }
    }
}
