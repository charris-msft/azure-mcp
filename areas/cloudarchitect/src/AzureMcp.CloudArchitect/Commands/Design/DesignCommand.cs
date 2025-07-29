// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.CloudArchitect.Options;
using AzureMcp.CloudArchitect.Options.Design;
using AzureMcp.CloudArchitect.Services;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Models;
using Microsoft.Extensions.Logging;

namespace AzureMcp.CloudArchitect.Commands.Design;

public sealed class DesignCommand(ILogger<DesignCommand> logger) : BaseCloudArchitectCommand<DesignOptions>
{
    private const string CommandTitle = "Generate Cloud Architecture Design";
    private readonly ILogger<DesignCommand> _logger = logger;

    // Define options from OptionDefinitions
    private readonly Option<string> _requirementsOption = CloudArchitectOptionDefinitions.RequirementsOption;
    private readonly Option<string> _workloadTypeOption = CloudArchitectOptionDefinitions.WorkloadTypeOption;
    private readonly Option<string> _scaleRequirementsOption = CloudArchitectOptionDefinitions.ScaleRequirementsOption;
    private readonly Option<string> _complianceRequirementsOption = CloudArchitectOptionDefinitions.ComplianceRequirementsOption;

    public override string Name => "design";

    public override string Description =>
        """
        Generate a comprehensive Azure cloud architecture design based on your requirements.
        This command analyzes your application requirements and provides detailed recommendations for:
        - Azure service selection and configuration
        - Security and compliance considerations  
        - Cost optimization strategies
        - Deployment and monitoring approaches
        - Implementation next steps
        
        Returns a structured architecture design with components, security considerations, and cost optimizations.
        
        Required options:
        - --requirements: Detailed description of your application or system requirements
        
        Optional options:
        - --workload-type: Type of workload (web-application, api-backend, data-processing, microservices, iot, machine-learning)
        - --scale-requirements: Expected scale and performance requirements
        - --compliance-requirements: Compliance and security requirements (GDPR, HIPAA, SOC2, etc.)
        
        Examples:
          azmcp cloudarchitect design --requirements "E-commerce platform with user authentication, product catalog, and payment processing"
          azmcp cloudarchitect design --requirements "Real-time data processing pipeline for IoT sensors" --workload-type "data-processing" --scale-requirements "1M events per day, global distribution"
          azmcp cloudarchitect design --requirements "Healthcare patient portal" --compliance-requirements "HIPAA" --scale-requirements "10K concurrent users"
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new()
    {
        Destructive = false,
        ReadOnly = true
    };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_requirementsOption);
        command.AddOption(_workloadTypeOption);
        command.AddOption(_scaleRequirementsOption);
        command.AddOption(_complianceRequirementsOption);
    }

    protected override DesignOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Requirements = parseResult.GetValueForOption(_requirementsOption);
        options.WorkloadType = parseResult.GetValueForOption(_workloadTypeOption);
        options.ScaleRequirements = parseResult.GetValueForOption(_scaleRequirementsOption);
        options.ComplianceRequirements = parseResult.GetValueForOption(_complianceRequirementsOption);
        return options;
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            // Required validation step
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            // Get the cloud architect service from DI
            var service = context.GetService<ICloudArchitectService>();

            // Generate architecture design
            var design = await service.GenerateArchitectureDesign(
                options.Requirements!,
                options.WorkloadType,
                options.ScaleRequirements,
                options.ComplianceRequirements,
                options.RetryPolicy);

            // Set results
            context.Response.Results = ResponseResult.Create(
                new DesignCommandResult(design),
                CloudArchitectJsonContext.Default.DesignCommandResult);
        }
        catch (Exception ex)
        {
            // Log error with all relevant context
            _logger.LogError(ex,
                "Error generating architecture design. Requirements: {Requirements}, WorkloadType: {WorkloadType}, Options: {@Options}",
                options.Requirements, options.WorkloadType, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    // Implementation-specific error handling
    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        ArgumentException argEx => $"Invalid requirements provided. {argEx.Message}",
        InvalidOperationException opEx => $"Unable to generate architecture design. {opEx.Message}",
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        ArgumentException => 400,
        InvalidOperationException => 422,
        _ => base.GetStatusCode(ex)
    };
}

// Strongly-typed result record
public record DesignCommandResult(AzureMcp.CloudArchitect.Models.ArchitectureDesign Design);
