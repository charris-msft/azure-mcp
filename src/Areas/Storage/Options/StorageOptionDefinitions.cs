// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Storage.Options;

public static class StorageOptionDefinitions
{
    public const string AccountName = "account-name";
    public const string ContainerName = "container-name";
    public const string TableName = "table-name";
    public const string FileSystemName = "file-system-name";
    public const string DirectoryPath = "directory";

    public static readonly Option<string> Account = new(
        $"--{AccountName}",
        "The name of the Azure Storage account. This is the unique name you chose for your storage account (e.g., 'mystorageaccount')."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Container = new(
        $"--{ContainerName}",
        "The name of the container to access within the storage account."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Table = new(
        $"--{TableName}",
        "The name of the table to access within the storage account."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> FileSystem = new(
        $"--{FileSystemName}",
        "The name of the Data Lake file system to access within the storage account."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Directory = new(
        $"--{DirectoryPath}",
        "The directory path within the Data Lake file system to list paths from. If not specified, lists paths from the root directory."
    )
    {
        IsRequired = false
    };
}
