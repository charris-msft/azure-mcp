// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> Properties of Cognitive Services account deployment model. (Deprecated, please use Deployment.sku instead.). </summary>
    public sealed class CognitiveServicesAccountDeploymentScaleSettings
    {
        /// <summary> Deployment scale type. </summary>
        [JsonPropertyName("scaleType")]
        public string ScaleType { get; set; }
        /// <summary> Deployment capacity. </summary>
        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }
        /// <summary> Deployment active capacity. This value might be different from `capacity` if customer recently updated `capacity`. </summary>
        [JsonPropertyName("activeCapacity")]
        public int? ActiveCapacity { get; set; }
    }
}
