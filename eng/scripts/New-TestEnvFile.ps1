#Requires -Version 7

param(
    [string] $TenantId,
    [string] $TestApplicationId,
    [string] $ResourceGroupName,
    [string] $BaseName,
    [hashtable] $AdditionalParameters,
    [string] $Path
)

$ErrorActionPreference = "Stop"

. "$PSScriptRoot/../common/scripts/common.ps1"
$RepoRoot = $RepoRoot.Path.Replace('\', '/')

if($env:ARM_OIDC_TOKEN -and $TenantId -and $TestApplicationId) {
    Write-Host "Using OIDC token for Azure Powershell authentication"
    Connect-AzAccount -ServicePrincipal `
        -TenantId $TenantId `
        -ApplicationId $TestApplicationId `
        -FederatedToken $env:ARM_OIDC_TOKEN
}

$context = Get-AzContext

$outputFile = "$Path/test-resources.env"

# When using TME in CI, $context.Tenant.Name is empty so we use a map
# $context.Tenant.Name still works for local dev
$tenantName = switch($context.Tenant.Id) {
    '70a036f6-8e4d-4615-bad6-149c02e7720d' { 'TME Organization' }
    '72f988bf-86f1-41af-91ab-2d7cd011db47' { 'Microsoft' }
    default { $context.Tenant.Name }
}

$tenantId = $context.Tenant.Id
$subscriptionId = $context.Subscription.Id
$subscriptionName = $context.Subscription.Name

$staticResourceGroupName = $AdditionalParameters.StaticResourceGroupName
$staticBaseName = $AdditionalParameters.StaticBaseName

$environmentText = @"
    TENANT=`"$tenantName`"
    TENANT_ID=`"$tenantId`"
    SUBSCRIPTION=`"$subscriptionName`"
    SUBSCRIPTION_ID=`"$subscriptionId`"
    STATIC_RESOURCE_GROUP=`"$staticResourceGroupName`"
    STATIC_RESOURCE_BASE_NAME=`"$staticBaseName`"
    RESOURCE_GROUP=`"$ResourceGroupName`"
    RESOURCE_BASE_NAME=`"$BaseName`"
"@

Write-Host "Creating test settings file at $outputFile`:`n$environmentText"

Set-Content $outputFile -Value $environmentText -Force
