// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> Properties of Cognitive Services account deployment. </summary>
    public sealed class CognitiveServicesAccountDeploymentProperties
    {
        /// <summary> Gets the status of the resource at the time the operation was called. </summary>
        [JsonPropertyName("provisioningState")]
        public string ProvisioningState { get; }
        /// <summary> Properties of Cognitive Services account deployment model. </summary>
        [JsonPropertyName("model")]
        public CognitiveServicesAccountDeploymentModel Model { get; set; }
        /// <summary> Properties of Cognitive Services account deployment model. (Deprecated, please use Deployment.sku instead.). </summary>
        [JsonPropertyName("scaleSettings")]
        public CognitiveServicesAccountDeploymentScaleSettings ScaleSettings { get; set; }
        /// <summary> The capabilities. </summary>
        [JsonPropertyName("capabilities")]
        public IReadOnlyDictionary<string, string> Capabilities { get; }
        /// <summary> The name of RAI policy. </summary>
        [JsonPropertyName("raiPolicyName")]
        public string RaiPolicyName { get; set; }
        /// <summary> The call rate limit Cognitive Services account. </summary>
        [JsonPropertyName("callRateLimit")]
        public ServiceAccountCallRateLimit CallRateLimit { get; }
        /// <summary> Gets the rate limits. </summary>
        [JsonPropertyName("rateLimits")]
        public IReadOnlyList<ServiceAccountThrottlingRule> RateLimits { get; }
        /// <summary> Deployment model version upgrade option. </summary>
        [JsonPropertyName("versionUpgradeOption")]
        public string VersionUpgradeOption { get; set; }
        /// <summary> If the dynamic throttling is enabled. </summary>
        [JsonPropertyName("dynamicThrottlingEnabled")]
        public bool? IsDynamicThrottlingEnabled { get; }
        /// <summary> The current capacity. </summary>
        [JsonPropertyName("currentCapacity")]
        public int? CurrentCapacity { get; set; }
        /// <summary> Internal use only. </summary>
        [JsonPropertyName("capacitySettings")]
        public DeploymentCapacitySettings CapacitySettings { get; set; }
        /// <summary> The name of parent deployment. </summary>
        [JsonPropertyName("parentDeploymentName")]
        public string ParentDeploymentName { get; set; }
        /// <summary> Specifies the deployment name that should serve requests when the request would have otherwise been throttled due to reaching current deployment throughput limit. </summary>
        [JsonPropertyName("spilloverDeploymentName")]
        public string SpilloverDeploymentName { get; set; }
    }
}
