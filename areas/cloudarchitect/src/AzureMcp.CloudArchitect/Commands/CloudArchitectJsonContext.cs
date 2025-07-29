// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.CloudArchitect.Commands;

[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(ArchitectureDesignToolParameters))]
[JsonSerializable(typeof(ArchitectureDesignToolResponse))]
[JsonSerializable(typeof(ArchitectureDesignToolState))]
[JsonSerializable(typeof(ArchitectureDesignTiers))]
[JsonSerializable(typeof(ArchitectureDesignRequirements))]
[JsonSerializable(typeof(ArchitectureDesignRequirement))]
[JsonSerializable(typeof(ArchitectureDesignConfidenceFactors))]
[JsonSerializable(typeof(ArchitectureTier))]
[JsonSerializable(typeof(ImportanceLevel))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class CloudArchitectJsonContext : JsonSerializerContext
{

}
