// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Sql.Services.Models
{
    /// <summary> User assigned identity properties. </summary>
    public partial class SqlUserAssignedIdentity
    {
        /// <summary> The principal ID of the assigned identity. </summary>
        [JsonPropertyName("principalId")]
        public Guid? PrincipalId { get; }
        /// <summary> The client ID of the assigned identity. </summary>
        [JsonPropertyName("clientId")]
        public Guid? ClientId { get; }
    }
}
