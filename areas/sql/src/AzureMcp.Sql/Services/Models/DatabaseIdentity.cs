// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Sql.Services.Models
{
    /// <summary> Azure Active Directory identity configuration for a resource. </summary>
    public partial class DatabaseIdentity
    {
        /// <summary> The identity type. </summary>
        [JsonPropertyName("type")]
        public string IdentityType { get; set; }
        /// <summary> The Azure Active Directory tenant id. </summary>
        [JsonPropertyName("tenantId")]
        public Guid? TenantId { get; set; }
        /// <summary> The resource ids of the user assigned identities to use. </summary>
        [JsonPropertyName("userAssignedIdentities")]
        public IDictionary<string, SqlUserAssignedIdentity> UserAssignedIdentities { get; set; }
    }
}
