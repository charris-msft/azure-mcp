// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using Azure.Core;
using AzureMcp.Models.Option;
using Xunit;

namespace AzureMcp.Tests.Options;

[Trait("Area", "Core")]
public class OptionDefinitionsTests
{
    [Fact]
    public void CreateResourceGroupOption_ShouldCreateSeparateInstances()
    {
        // Arrange & Act
        var requiredOption = OptionDefinitions.Common.CreateResourceGroupOption(isRequired: true);
        var optionalOption = OptionDefinitions.Common.CreateResourceGroupOption(isRequired: false);

        // Assert
        Assert.NotSame(requiredOption, optionalOption);
        Assert.True(requiredOption.IsRequired);
        Assert.False(optionalOption.IsRequired);
        Assert.Equal(requiredOption.Name, optionalOption.Name);
        Assert.Equal(requiredOption.Description, optionalOption.Description);
    }

    [Fact]
    public void CreateResourceGroupOption_ShouldNotShareState()
    {
        // Arrange
        var option1 = OptionDefinitions.Common.CreateResourceGroupOption(isRequired: true);
        var option2 = OptionDefinitions.Common.CreateResourceGroupOption(isRequired: true);

        // Act - Modify one option
        option1.IsRequired = false;

        // Assert - Other option should be unaffected
        Assert.False(option1.IsRequired);
        Assert.True(option2.IsRequired);
    }

    [Fact]
    public void CreateResourceGroupOption_ShouldDefaultToRequired()
    {
        // Act
        var option = OptionDefinitions.Common.CreateResourceGroupOption();

        // Assert
        Assert.True(option.IsRequired);
    }

    [Fact]
    public void CreateResourceGroupOption_ShouldUseCorrectNameAndDescription()
    {
        // Act
        var option = OptionDefinitions.Common.CreateResourceGroupOption();

        // Assert
        Assert.Equal($"--{OptionDefinitions.Common.ResourceGroupName}", option.Name);
        Assert.Equal("The name of the Azure resource group. This is a logical container for Azure resources.", option.Description);
    }

    [Fact]
    public void StaticResourceGroupOption_ShouldStillExist()
    {
        // Act & Assert - The static option should still exist for backward compatibility
        Assert.NotNull(OptionDefinitions.Common.ResourceGroup);
        Assert.True(OptionDefinitions.Common.ResourceGroup.IsRequired);
        Assert.Equal($"--{OptionDefinitions.Common.ResourceGroupName}", OptionDefinitions.Common.ResourceGroup.Name);
    }

    // Generic factory method tests
    [Fact]
    public void CreateOption_ShouldCreateBasicOption()
    {
        // Act
        var option = OptionDefinitions.Common.CreateOption<string>("--test", "Test description");

        // Assert
        Assert.NotNull(option);
        Assert.Equal("--test", option.Name);
        Assert.Equal("Test description", option.Description);
        Assert.False(option.IsRequired); // Default value
        Assert.False(option.IsHidden); // Default value
    }

    [Fact]
    public void CreateOption_ShouldConfigureOptionCorrectly()
    {
        // Act
        var option = OptionDefinitions.Common.CreateOption<string>(
            "--test",
            "Test description",
            opt =>
            {
                opt.IsRequired = true;
                opt.IsHidden = true;
            });

        // Assert
        Assert.True(option.IsRequired);
        Assert.True(option.IsHidden);
    }

    [Fact]
    public void CreateOption_WithDefaultValue_ShouldCreateOptionWithDefault()
    {
        // Act
        var option = OptionDefinitions.Common.CreateOption(
            "--test",
            () => "default-value",
            "Test description");

        // Assert
        Assert.NotNull(option);
        Assert.Equal("--test", option.Name);
        Assert.Equal("Test description", option.Description);
    }

    [Fact]
    public void CreateOption_WithDefaultValueAndConfiguration_ShouldApplyBoth()
    {
        // Act
        var option = OptionDefinitions.Common.CreateOption(
            "--test",
            () => 42,
            "Test description",
            opt => opt.IsRequired = true);

        // Assert
        Assert.Equal("--test", option.Name);
        Assert.Equal("Test description", option.Description);
        Assert.True(option.IsRequired);
    }

    [Fact]
    public void CreateOption_ShouldCreateSeparateInstances()
    {
        // Act
        var option1 = OptionDefinitions.Common.CreateOption<string>("--test", "Test description");
        var option2 = OptionDefinitions.Common.CreateOption<string>("--test", "Test description");

        // Assert
        Assert.NotSame(option1, option2);
        Assert.Equal(option1.Name, option2.Name);
        Assert.Equal(option1.Description, option2.Description);
    }

    [Fact]
    public void CreateOption_ShouldNotShareState()
    {
        // Arrange
        var option1 = OptionDefinitions.Common.CreateOption<string>("--test", "Test description");
        var option2 = OptionDefinitions.Common.CreateOption<string>("--test", "Test description");

        // Act - Modify one option
        option1.IsRequired = true;
        option1.IsHidden = true;

        // Assert - Other option should be unaffected
        Assert.True(option1.IsRequired);
        Assert.True(option1.IsHidden);
        Assert.False(option2.IsRequired);
        Assert.False(option2.IsHidden);
    }

    [Fact]
    public void CreateOption_ShouldSupportDifferentTypes()
    {
        // Act
        var stringOption = OptionDefinitions.Common.CreateOption<string>("--string", "String option");
        var intOption = OptionDefinitions.Common.CreateOption<int>("--int", "Int option");
        var boolOption = OptionDefinitions.Common.CreateOption<bool>("--bool", "Bool option");
        var enumOption = OptionDefinitions.Common.CreateOption<RetryMode>("--enum", "Enum option");

        // Assert
        Assert.IsType<Option<string>>(stringOption);
        Assert.IsType<Option<int>>(intOption);
        Assert.IsType<Option<bool>>(boolOption);
        Assert.IsType<Option<RetryMode>>(enumOption);
    }

    [Fact]
    public void CreateOption_WithNullConfiguration_ShouldSucceed()
    {
        // Act
        var option = OptionDefinitions.Common.CreateOption<string>("--test", "Test description", null);

        // Assert
        Assert.NotNull(option);
        Assert.False(option.IsRequired);
        Assert.False(option.IsHidden);
    }

    [Fact]
    public void CreateResourceGroupOption_ShouldUseGenericFactory()
    {
        // This test ensures the ResourceGroup factory is using the generic factory internally
        // by verifying it behaves the same way as the generic factory

        // Act
        var resourceGroupOption = OptionDefinitions.Common.CreateResourceGroupOption(isRequired: false);
        var genericOption = OptionDefinitions.Common.CreateOption<string>(
            $"--{OptionDefinitions.Common.ResourceGroupName}",
            "The name of the Azure resource group. This is a logical container for Azure resources.",
            opt => opt.IsRequired = false);

        // Assert
        Assert.Equal(resourceGroupOption.Name, genericOption.Name);
        Assert.Equal(resourceGroupOption.Description, genericOption.Description);
        Assert.Equal(resourceGroupOption.IsRequired, genericOption.IsRequired);
        Assert.NotSame(resourceGroupOption, genericOption); // Different instances
    }
}
