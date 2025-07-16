// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

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
}