// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> The ServiceAccountThrottlingRule. </summary>
    public sealed class ServiceAccountThrottlingRule
    {
        /// <summary> Gets the key. </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
        /// <summary> Gets the renewal period. </summary>
        [JsonPropertyName("renewalPeriod")]
        public float? RenewalPeriod { get; set; }
        /// <summary> Gets the count. </summary>
        [JsonPropertyName("count")]
        public float? Count { get; set; }
        /// <summary> Gets the min count. </summary>
        [JsonPropertyName("minCount")]
        public float? MinCount { get; set; }
        /// <summary> Gets the is dynamic throttling enabled. </summary>
        [JsonPropertyName("dynamicThrottlingEnabled")]
        public bool? IsDynamicThrottlingEnabled { get; set; }
        /// <summary> Gets the match patterns. </summary>
        [JsonPropertyName("matchPatterns")]
        public IReadOnlyList<ServiceAccountThrottlingMatchPattern> MatchPatterns { get; set; }
    }
}
