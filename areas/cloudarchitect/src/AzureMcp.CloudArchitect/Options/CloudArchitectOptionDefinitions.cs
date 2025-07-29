// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.CloudArchitect.Options;

public static class CloudArchitectOptionDefinitions
{
    // Original options for the CloudArchitectCommand
    public const string ResourceName = "resource";
    public const string ActionName = "action";

    public static readonly Option<string> Resource = new(
        $"--{ResourceName}",
        "The Azure resource type for which to get cloud architecture designs. Options: 'general' (general Azure), 'azurefunctions' (Azure Functions), 'static-web-app' (Azure Static Web Apps)."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Action = new(
        $"--{ActionName}",
        "The action type for the cloud architecture designs. Options: 'all', 'code-generation', 'deployment'. Note: 'static-web-app' resource only supports 'all'."
    )
    {
        IsRequired = true
    };

    // New options for the ArchitectureDesignCommand
    public const string QuestionName = "question";
    public const string QuestionNumberName = "questionNumber";
    public const string TotalQuestionsName = "totalQuestions";
    public const string AnswerName = "answer";
    public const string NextQuestionNeededName = "nextQuestionNeeded";
    public const string ConfidenceScoreName = "confidenceScore";
    public const string ArchitectureComponentName = "architectureComponent";
    public const string ArchitectureTierName = "architectureTier";
    public const string StateName = "state";

    public static readonly Option<string> Question = new(
        $"--{QuestionName}",
        "The current question being asked"
    )
    {
        IsRequired = true
    };

    public static readonly Option<int> QuestionNumber = new(
        $"--{QuestionNumberName}",
        "Current question number in sequence"
    )
    {
        IsRequired = true
    };

    public static readonly Option<int> TotalQuestions = new(
        $"--{TotalQuestionsName}",
        "Estimated total questions needed"
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Answer = new(
        $"--{AnswerName}",
        "The user's response to the question (if available)"
    );

    public static readonly Option<bool> NextQuestionNeeded = new(
        $"--{NextQuestionNeededName}",
        "Whether another question is needed"
    )
    {
        IsRequired = true
    };

    public static readonly Option<double> ConfidenceScore = new(
        $"--{ConfidenceScoreName}",
        "A value between 0.0 and 1.0 representing confidence in understanding requirements"
    );

    public static readonly Option<string> ArchitectureComponent = new(
        $"--{ArchitectureComponentName}",
        "The specific Azure component being suggested"
    );

    public static readonly Option<string> ArchitectureTier = new(
        $"--{ArchitectureTierName}",
        "Which tier this component belongs to (infrastructure, platform, application, data, security, operations)"
    );

    public static readonly Option<string> State = new(
        $"--{StateName}",
        "The complete architecture state from the previous request (JSON format)"
    )
    {
        IsRequired = true
    };
}
