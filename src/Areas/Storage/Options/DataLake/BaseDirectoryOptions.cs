// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Storage.Options.DataLake;

public class BaseDirectoryOptions : BaseStorageOptions
{
    [JsonPropertyName(StorageOptionDefinitions.DirectoryName)]
    public string? Directory { get; set; }
}