// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> Internal use only. </summary>
    public sealed class DeploymentCapacitySettings
    {
        /// <summary> The designated capacity. </summary>
        [JsonPropertyName("designatedCapacity")]
        public int? DesignatedCapacity { get; set; }
        /// <summary> The priority of this capacity setting. </summary>
        [JsonPropertyName("priority")]
        public int? Priority { get; set; }
    }
}
