// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary>
    /// A class representing the CognitiveServicesAccountDeployment data model.
    /// Cognitive Services account deployment.
    /// </summary>
    public sealed class CognitiveServicesAccountDeploymentData
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
        /// <summary> The resource model definition representing SKU. </summary>
        [JsonPropertyName("sku")]
        public CognitiveServicesSku Sku { get; set; }
        /// <summary> Resource Etag. </summary>
        [JsonPropertyName("etag")]
        public ETag ETag { get; }
        /// <summary> Resource tags. </summary>
        [JsonPropertyName("tags")]
        public IDictionary<string, string> Tags { get; }
        /// <summary> Properties of Cognitive Services account deployment. </summary>
        [JsonPropertyName("properties")]
        public CognitiveServicesAccountDeploymentProperties Properties { get; set; }
    }
}
