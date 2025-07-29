// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using System.Text.Json;
using AzureMcp.CloudArchitect.Options;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Helpers;
using Microsoft.Extensions.Logging;

namespace AzureMcp.CloudArchitect.Commands;

public sealed class ArchitectureDesignCommand(ILogger<ArchitectureDesignCommand> logger) : BaseCommand
{
    private const string CommandTitle = "Design Azure Architecture";
    private readonly ILogger<ArchitectureDesignCommand> _logger = logger;

    private readonly Option<string> _questionOption = CloudArchitectOptionDefinitions.Question;
    private readonly Option<int> _questionNumberOption = CloudArchitectOptionDefinitions.QuestionNumber;
    private readonly Option<int> _totalQuestionsOption = CloudArchitectOptionDefinitions.TotalQuestions;
    private readonly Option<string> _answerOption = CloudArchitectOptionDefinitions.Answer;
    private readonly Option<bool> _nextQuestionNeededOption = CloudArchitectOptionDefinitions.NextQuestionNeeded;
    private readonly Option<double> _confidenceScoreOption = CloudArchitectOptionDefinitions.ConfidenceScore;
    private readonly Option<string> _architectureComponentOption = CloudArchitectOptionDefinitions.ArchitectureComponent;
    private readonly Option<string> _architectureTierOption = CloudArchitectOptionDefinitions.ArchitectureTier;
    private readonly Option<string> _stateOption = CloudArchitectOptionDefinitions.State;

    public override string Name => "design-architecture";

    public override string Description =>
        "A tool for designing Azure cloud architectures through guided questions. " +
        "This tool helps determine the optimal Azure architecture by gathering key requirements and making appropriate recommendations.";

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        command.AddOption(_questionOption);
        command.AddOption(_questionNumberOption);
        command.AddOption(_totalQuestionsOption);
        command.AddOption(_answerOption);
        command.AddOption(_nextQuestionNeededOption);
        command.AddOption(_confidenceScoreOption);
        command.AddOption(_architectureComponentOption);
        command.AddOption(_architectureTierOption);
        command.AddOption(_stateOption);
    }

    [McpServerTool(Destructive = false, ReadOnly = false, Title = CommandTitle)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var validationResult = Validate(parseResult.CommandResult, context.Response);
            if (!validationResult.IsValid)
            {
                return Task.FromResult(context.Response);
            }

            // Extract parameters from the parse result
            var parameters = ExtractParameters(parseResult);

            // Process the architecture design request
            var response = ProcessArchitectureDesign(parameters);

            context.Response.Status = 200;
            context.Response.Results = ResponseResult.Create(response, CloudArchitectJsonContext.Default.ArchitectureDesignToolResponse);
            context.Response.Message = string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing architecture design request");
            HandleException(context, ex);
        }

        return Task.FromResult(context.Response);
    }

    public override ValidationResult Validate(CommandResult commandResult, CommandResponse? commandResponse = null)
    {
        var validationResult = new ValidationResult { IsValid = true };

        var question = commandResult.GetValueForOption(_questionOption);
        var questionNumber = commandResult.GetValueForOption(_questionNumberOption);
        var totalQuestions = commandResult.GetValueForOption(_totalQuestionsOption);
        var nextQuestionNeeded = commandResult.GetValueForOption(_nextQuestionNeededOption);
        var stateJson = commandResult.GetValueForOption(_stateOption);

        if (string.IsNullOrEmpty(question))
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Question parameter is required.";
        }
        else if (questionNumber <= 0)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Question number must be greater than 0.";
        }
        else if (totalQuestions <= 0)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Total questions must be greater than 0.";
        }
        else if (string.IsNullOrEmpty(stateJson))
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "State parameter is required.";
        }

        if (!validationResult.IsValid && commandResponse != null)
        {
            commandResponse.Status = 400;
            commandResponse.Message = validationResult.ErrorMessage!;
        }

        return validationResult;
    }

    private ArchitectureDesignToolParameters ExtractParameters(ParseResult parseResult)
    {
        var question = parseResult.GetValueForOption(_questionOption)!;
        var questionNumber = parseResult.GetValueForOption(_questionNumberOption);
        var totalQuestions = parseResult.GetValueForOption(_totalQuestionsOption);
        var answer = parseResult.GetValueForOption(_answerOption);
        var nextQuestionNeeded = parseResult.GetValueForOption(_nextQuestionNeededOption);
        var confidenceScore = parseResult.GetValueForOption(_confidenceScoreOption);
        var architectureComponent = parseResult.GetValueForOption(_architectureComponentOption);
        var architectureTierString = parseResult.GetValueForOption(_architectureTierOption);
        var stateJson = parseResult.GetValueForOption(_stateOption)!;

        // Parse the architecture tier enum
        ArchitectureTier? architectureTier = null;
        if (!string.IsNullOrEmpty(architectureTierString))
        {
            if (Enum.TryParse<ArchitectureTier>(architectureTierString, true, out var tier))
            {
                architectureTier = tier;
            }
        }

        // Deserialize the state from JSON
        var state = JsonSerializer.Deserialize<ArchitectureDesignToolState>(stateJson, CloudArchitectJsonContext.Default.ArchitectureDesignToolState);
        if (state == null)
        {
            throw new ArgumentException("Invalid state JSON provided");
        }

        return new ArchitectureDesignToolParameters
        {
            Question = question,
            QuestionNumber = questionNumber,
            TotalQuestions = totalQuestions,
            Answer = answer,
            NextQuestionNeeded = nextQuestionNeeded,
            ConfidenceScore = confidenceScore,
            ArchitectureComponent = architectureComponent,
            ArchitectureTier = architectureTier,
            State = state
        };
    }

    private ArchitectureDesignToolResponse ProcessArchitectureDesign(ArchitectureDesignToolParameters parameters)
    {
        // The implementation here mirrors the TypeScript version - it's deceptively short
        // because all the "magic" happens in the tool description and the data structures.
        // The tool description in the resource file drives the LM conversation.

        return new ArchitectureDesignToolResponse
        {
            DisplayText = parameters.Question,
            DisplayThought = parameters.State.Thought,
            DisplayHint = parameters.State.SuggestedHint,
            QuestionNumber = parameters.QuestionNumber,
            TotalQuestions = parameters.TotalQuestions,
            NextQuestionNeeded = parameters.NextQuestionNeeded,
            State = parameters.State // Pass the state back unchanged
        };
    }
}
