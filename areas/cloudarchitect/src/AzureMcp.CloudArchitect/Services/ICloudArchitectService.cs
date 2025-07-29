// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.CloudArchitect.Models;
using AzureMcp.Core.Options;

namespace AzureMcp.CloudArchitect.Services;

public interface ICloudArchitectService
{
    Task<ArchitectureDesign> GenerateArchitectureDesign(
        string requirements,
        string? workloadType = null,
        string? scaleRequirements = null,
        string? complianceRequirements = null,
        RetryPolicyOptions? retryPolicy = null);
}
