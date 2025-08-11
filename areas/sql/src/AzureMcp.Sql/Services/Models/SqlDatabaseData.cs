// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;
using AzureMcp.Sql.Commands;

namespace AzureMcp.Sql.Services.Models
{
    /// <summary>
    /// A class representing the SqlDatabase data model.
    /// A database resource.
    /// </summary>
    public partial class SqlDatabaseData
    {
        /// <summary> The resource ID for the resource. </summary>
        [JsonPropertyName("id")]
        public string ResourceId { get; set; }
        /// <summary> The type of the resource. </summary>
        [JsonPropertyName("type")]
        public string ResourceType { get; set; }
        /// <summary> The name of the resource. </summary>
        [JsonPropertyName("name")]
        public string ResourceName { get; set; }
        /// <summary> The location of the resource. </summary>
        [JsonPropertyName("location")]
        public string Location { get; set; }
        /// <summary> The database SKU. </summary>
        [JsonPropertyName("sku")]
        public SqlSku Sku { get; set; }
        /// <summary> Kind of database. This is metadata used for the Azure portal experience. </summary>
        [JsonPropertyName("kind")]
        public string Kind { get; }
        /// <summary> Resource that manages the database. </summary>
        [JsonPropertyName("managedBy")]
        public string ManagedBy { get; }
        /// <summary> The Azure Active Directory identity of the database. </summary>
        [JsonPropertyName("identity")]
        public DatabaseIdentity Identity { get; set; }
        /// <summary> The tags of the resource. </summary>
        [JsonPropertyName("tags")]
        public IDictionary<string, string> Tags { get; set; }
        /// <summary> Properties of the Sql database. </summary>
        [JsonPropertyName("properties")]
        public SqlDatabaseProperties Properties { get; set; }

        // Read the JSON response content and create a model instance from it.
        public static SqlDatabaseData FromJson(JsonElement source)
        {
            return JsonSerializer.Deserialize<SqlDatabaseData>(source, SqlJsonContext.Default.SqlDatabaseData);
        }
    }
}
