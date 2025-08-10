// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> The call rate limit Cognitive Services account. </summary>
    public sealed class ServiceAccountCallRateLimit
    {
        /// <summary> The count value of Call Rate Limit. </summary>
        [JsonPropertyName("count")]
        public float? Count { get; }
        /// <summary> The renewal period in seconds of Call Rate Limit. </summary>
        [JsonPropertyName("renewalPeriod")]
        public float? RenewalPeriod { get; }
        /// <summary> Gets the rules. </summary>
        [JsonPropertyName("rules")]
        public IReadOnlyList<ServiceAccountThrottlingRule> Rules { get; }
    }
}
