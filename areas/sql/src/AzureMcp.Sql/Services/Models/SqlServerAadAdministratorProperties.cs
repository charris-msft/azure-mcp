// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Sql.Services.Models
{
    /// <summary>
    /// A class representing the SqlServerAadAdministrator properties model.
    /// Azure Active Directory administrator properties.
    /// </summary>
    public partial class SqlServerAadAdministratorProperties
    {
        /// <summary> Type of the sever administrator. </summary>
        [JsonPropertyName("administratorType")]
        public string AdministratorType { get; set; }
        /// <summary> Login name of the server administrator. </summary>
        [JsonPropertyName("login")]
        public string Login { get; set; }
        /// <summary> SID (object ID) of the server administrator. </summary>
        [JsonPropertyName("sid")]
        public Guid? Sid { get; set; }
        /// <summary> Tenant ID of the administrator. </summary>
        [JsonPropertyName("tenantId")]
        public Guid? TenantId { get; set; }
        /// <summary> Azure Active Directory only Authentication enabled. </summary>
        [JsonPropertyName("azureADOnlyAuthentication")]
        public bool? IsAzureADOnlyAuthenticationEnabled { get; }
    }
}
