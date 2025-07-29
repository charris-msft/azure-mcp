// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.CloudArchitect.Commands.Design;
using AzureMcp.Core.Options;

namespace AzureMcp.CloudArchitect.UnitTests.Design;

public class DesignCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICloudArchitectService _service;
    private readonly ILogger<DesignCommand> _logger;
    private readonly DesignCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public DesignCommandTests()
    {
        _service = Substitute.For<ICloudArchitectService>();
        _logger = Substitute.For<ILogger<DesignCommand>>();

        var collection = new ServiceCollection().AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("design", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("Generate a comprehensive Azure cloud architecture design", command.Description);
    }

    [Theory]
    [InlineData("--requirements \"E-commerce platform with user authentication\"", true)]
    [InlineData("--requirements \"Data processing pipeline\" --workload-type \"data-processing\"", true)]
    [InlineData("--requirements \"Healthcare portal\" --compliance-requirements \"HIPAA\"", true)]
    [InlineData("", false)]
    [InlineData("--workload-type \"web-application\"", false)] // Missing required requirements
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var mockDesign = new ArchitectureDesign(
                "Test architecture description",
                new List<ArchitectureRecommendation>
                {
                    new("Test Component", "Azure App Service", "Test reasoning", "Test config", new List<string> { "Alternative" })
                },
                new List<SecurityConsideration>
                {
                    new("Test Area", "Test recommendation", "Test implementation")
                },
                new List<CostOptimization>
                {
                    new("Test Service", "Test optimization", "Test savings")
                },
                "Test deployment considerations",
                "Test monitoring strategy",
                new List<string> { "Test next step" }
            );

            _service.GenerateArchitectureDesign(
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<RetryPolicyOptions?>())
                .Returns(mockDesign);
        }

        var parseResult = _parser.Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (shouldSucceed)
        {
            Assert.NotNull(response.Results);
            Assert.Equal("Success", response.Message);
            
            // Verify service was called
            await _service.Received(1).GenerateArchitectureDesign(
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<RetryPolicyOptions?>());
        }
        else
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _service.GenerateArchitectureDesign(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(Task.FromException<ArchitectureDesign>(new InvalidOperationException("Test error")));

        var parseResult = _parser.Parse(["--requirements", "test requirements"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(422, response.Status);
        Assert.Contains("Unable to generate architecture design", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesArgumentExceptions()
    {
        // Arrange
        _service.GenerateArchitectureDesign(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(Task.FromException<ArchitectureDesign>(new ArgumentException("Invalid requirements")));

        var parseResult = _parser.Parse(["--requirements", "invalid requirements"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.Contains("Invalid requirements provided", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Theory]
    [InlineData("--requirements \"E-commerce platform\" --workload-type \"web-application\"")]
    [InlineData("--requirements \"Data pipeline\" --scale-requirements \"1M records/day\"")]
    [InlineData("--requirements \"Healthcare app\" --compliance-requirements \"HIPAA\"")]
    public async Task ExecuteAsync_PassesCorrectParametersToService(string args)
    {
        // Arrange
        var mockDesign = new ArchitectureDesign(
            "Test architecture",
            new List<ArchitectureRecommendation>(),
            new List<SecurityConsideration>(),
            new List<CostOptimization>(),
            "Test deployment",
            "Test monitoring",
            new List<string>()
        );

        _service.GenerateArchitectureDesign(
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(mockDesign);

        var parseResult = _parser.Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        
        // Verify the service was called with correct parameters
        await _service.Received(1).GenerateArchitectureDesign(
            Arg.Is<string>(r => !string.IsNullOrEmpty(r)),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>());
    }
}
