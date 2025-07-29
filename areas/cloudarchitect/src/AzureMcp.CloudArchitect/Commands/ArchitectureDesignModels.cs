// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.CloudArchitect.Commands;

/// <summary>
/// The set of parameters that the tool takes as input.
/// </summary>
public sealed class ArchitectureDesignToolParameters
{
    [JsonPropertyName("question")]
    public required string Question { get; set; }

    [JsonPropertyName("questionNumber")]
    public required int QuestionNumber { get; set; }

    [JsonPropertyName("totalQuestions")]
    public required int TotalQuestions { get; set; }

    [JsonPropertyName("answer")]
    public string? Answer { get; set; }

    [JsonPropertyName("nextQuestionNeeded")]
    public required bool NextQuestionNeeded { get; set; }

    [JsonPropertyName("confidenceScore")]
    public double? ConfidenceScore { get; set; }

    [JsonPropertyName("architectureComponent")]
    public string? ArchitectureComponent { get; set; }

    [JsonPropertyName("architectureTier")]
    public ArchitectureTier? ArchitectureTier { get; set; }

    [JsonPropertyName("state")]
    public required ArchitectureDesignToolState State { get; set; }
}

public sealed class ArchitectureDesignToolState
{
    [JsonPropertyName("architectureComponents")]
    public List<string> ArchitectureComponents { get; set; } = [];

    [JsonPropertyName("architectureTiers")]
    public required ArchitectureDesignTiers ArchitectureTiers { get; set; }

    [JsonPropertyName("thought")]
    public required string Thought { get; set; }

    [JsonPropertyName("suggestedHint")]
    public required string SuggestedHint { get; set; }

    [JsonPropertyName("requirements")]
    public required ArchitectureDesignRequirements Requirements { get; set; }

    [JsonPropertyName("confidenceFactors")]
    public required ArchitectureDesignConfidenceFactors ConfidenceFactors { get; set; }
}

public sealed class ArchitectureDesignTiers
{
    [JsonPropertyName("infrastructure")]
    public List<string> Infrastructure { get; set; } = [];

    [JsonPropertyName("platform")]
    public List<string> Platform { get; set; } = [];

    [JsonPropertyName("application")]
    public List<string> Application { get; set; } = [];

    [JsonPropertyName("data")]
    public List<string> Data { get; set; } = [];

    [JsonPropertyName("security")]
    public List<string> Security { get; set; } = [];

    [JsonPropertyName("operations")]
    public List<string> Operations { get; set; } = [];
}

public sealed class ArchitectureDesignRequirements
{
    [JsonPropertyName("explicit")]
    public List<ArchitectureDesignRequirement> Explicit { get; set; } = [];

    [JsonPropertyName("implicit")]
    public List<ArchitectureDesignRequirement> Implicit { get; set; } = [];

    [JsonPropertyName("assumed")]
    public List<ArchitectureDesignRequirement> Assumed { get; set; } = [];
}

public sealed class ArchitectureDesignRequirement
{
    [JsonPropertyName("category")]
    public required string Category { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("source")]
    public required string Source { get; set; }

    [JsonPropertyName("importance")]
    public required ImportanceLevel Importance { get; set; }

    [JsonPropertyName("confidence")]
    public required double Confidence { get; set; }
}

public sealed class ArchitectureDesignConfidenceFactors
{
    [JsonPropertyName("explicitRequirementsCoverage")]
    public required double ExplicitRequirementsCoverage { get; set; }

    [JsonPropertyName("implicitRequirementsCertainty")]
    public required double ImplicitRequirementsCertainty { get; set; }

    [JsonPropertyName("assumptionRisk")]
    public required double AssumptionRisk { get; set; }
}

public sealed class ArchitectureDesignToolResponse
{
    [JsonPropertyName("display_text")]
    public required string DisplayText { get; set; }

    [JsonPropertyName("display_thought")]
    public required string DisplayThought { get; set; }

    [JsonPropertyName("display_hint")]
    public required string DisplayHint { get; set; }

    [JsonPropertyName("questionNumber")]
    public required int QuestionNumber { get; set; }

    [JsonPropertyName("totalQuestions")]
    public required int TotalQuestions { get; set; }

    [JsonPropertyName("nextQuestionNeeded")]
    public required bool NextQuestionNeeded { get; set; }

    [JsonPropertyName("state")]
    public required ArchitectureDesignToolState State { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ArchitectureTier
{
    [JsonPropertyName("infrastructure")]
    Infrastructure,

    [JsonPropertyName("platform")]
    Platform,

    [JsonPropertyName("application")]
    Application,

    [JsonPropertyName("data")]
    Data,

    [JsonPropertyName("security")]
    Security,

    [JsonPropertyName("operations")]
    Operations
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ImportanceLevel
{
    [JsonPropertyName("high")]
    High,

    [JsonPropertyName("medium")]
    Medium,

    [JsonPropertyName("low")]
    Low
}
