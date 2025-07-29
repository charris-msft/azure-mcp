// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.CloudArchitect.Options;

namespace AzureMcp.CloudArchitect.Commands;

public abstract class BaseCloudArchitectCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : GlobalCommand<T>
    where T : BaseCloudArchitectOptions, new()
{
}
