// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.CloudArchitect.Options.Design;

public class DesignOptions : BaseCloudArchitectOptions
{
    [JsonPropertyName(CloudArchitectOptionDefinitions.Requirements)]
    public string? Requirements { get; set; }

    [JsonPropertyName(CloudArchitectOptionDefinitions.WorkloadType)]
    public string? WorkloadType { get; set; }

    [JsonPropertyName(CloudArchitectOptionDefinitions.ScaleRequirements)]
    public string? ScaleRequirements { get; set; }

    [JsonPropertyName(CloudArchitectOptionDefinitions.ComplianceRequirements)]
    public string? ComplianceRequirements { get; set; }
}
