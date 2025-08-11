// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Sql.Services.Models
{
    /// <summary> An ARM Resource SKU. </summary>
    public partial class SqlSku
    {
        /// <summary> The name of the SKU, typically, a letter + Number code, e.g. P3. </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary> The tier or edition of the particular SKU, e.g. Basic, Premium. </summary>
        [JsonPropertyName("tier")]
        public string Tier { get; set; }
        /// <summary> Size of the particular SKU. </summary>
        [JsonPropertyName("size")]
        public string Size { get; set; }
        /// <summary> If the service has different generations of hardware, for the same SKU, then that can be captured here. </summary>
        [JsonPropertyName("family")]
        public string Family { get; set; }
        /// <summary> Capacity of the particular SKU. </summary>
        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }
    }
}
