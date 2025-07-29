// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.CloudArchitect.Options;

public static class CloudArchitectOptionDefinitions
{
    public const string Requirements = "requirements";
    public const string WorkloadType = "workload-type";
    public const string ScaleRequirements = "scale-requirements";
    public const string ComplianceRequirements = "compliance-requirements";

    public static readonly Option<string> RequirementsOption = new(
        $"--{Requirements}",
        "Detailed description of the application or system requirements, including functionality, expected users, and key features."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> WorkloadTypeOption = new(
        $"--{WorkloadType}",
        "The type of workload (e.g., web-application, data-processing, microservices, iot, machine-learning, api-backend)."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> ScaleRequirementsOption = new(
        $"--{ScaleRequirements}",
        "Expected scale and performance requirements (e.g., number of users, data volume, requests per second, geographic distribution)."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> ComplianceRequirementsOption = new(
        $"--{ComplianceRequirements}",
        "Compliance and security requirements (e.g., GDPR, HIPAA, SOC2, data residency requirements)."
    )
    {
        IsRequired = false
    };
}
