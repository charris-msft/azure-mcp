// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.KeyVault.Commands.Key;
using AzureMcp.KeyVault.Commands.Secret;

namespace AzureMcp.KeyVault.Commands;

[JsonSerializable(typeof(KeyListCommand.KeyListCommandResult))]
[JsonSerializable(typeof(KeyGetCommand.KeyGetCommandResult))]
[JsonSerializable(typeof(KeyCreateCommand.KeyCreateCommandResult))]
[JsonSerializable(typeof(SecretGetCommand.SecretGetCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class KeyVaultJsonContext : JsonSerializerContext
{
}
