// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.CloudArchitect.Options.Design;

public class DesignOptions : BaseCloudArchitectOptions
{
    /// <summary>
    /// Architecture design tool parameters for guided design flow.
    /// </summary>
    [JsonPropertyName("architectureDesignTool")]
    public ArchitectureDesignToolOptions? ArchitectureDesignTool { get; set; }
}
