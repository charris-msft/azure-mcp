// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> The ServiceAccountThrottlingMatchPattern. </summary>
    public sealed class ServiceAccountThrottlingMatchPattern
    {
        /// <summary> Gets the path. </summary>
        [JsonPropertyName("path")]
        public string Path { get; }
        /// <summary> Gets the method. </summary>
        [JsonPropertyName("method")]
        public string Method { get; }
    }
}
