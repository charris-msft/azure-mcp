// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


#nullable disable

using System.Text.Json.Serialization;
using Azure.Core;

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> Properties of Cognitive Services account deployment model. </summary>
    public sealed class CognitiveServicesAccountDeploymentModel
    {
        /// <summary> Deployment model publisher. </summary>
        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }
        /// <summary> Deployment model format. </summary>
        [JsonPropertyName("format")]
        public string Format { get; set; }
        /// <summary> Deployment model name. </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary> Optional. Deployment model version. If version is not specified, a default version will be assigned. The default version is different for different models and might change when there is new version available for a model. Default version for a model could be found from list models API. </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }
        /// <summary> Optional. Deployment model source ARM resource ID. </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; }
        /// <summary> Optional. Source of the model, another Microsoft.CognitiveServices accounts ARM resource ID. </summary>
        [JsonPropertyName("sourceAccount")]
        public ResourceIdentifier SourceAccount { get; set; }
        /// <summary> The call rate limit Cognitive Services account. </summary>
        [JsonPropertyName("callRateLimit")]
        public ServiceAccountCallRateLimit CallRateLimit { get; set; }
    }
}
