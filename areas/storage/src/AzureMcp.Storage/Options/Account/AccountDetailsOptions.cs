// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;

namespace AzureMcp.Storage.Options.Account;

public class AccountDetailsOptions : SubscriptionOptions
{
    public string? Account { get; set; }
}
