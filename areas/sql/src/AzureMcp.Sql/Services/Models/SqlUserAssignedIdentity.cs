// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Sql.Services.Models
{
    /// <summary> User assigned identity properties. </summary>
    internal sealed class SqlUserAssignedIdentity
    {
        /// <summary> The principal ID of the assigned identity. </summary>
        public Guid? PrincipalId { get; }
        /// <summary> The client ID of the assigned identity. </summary>
        public Guid? ClientId { get; }
    }
}
