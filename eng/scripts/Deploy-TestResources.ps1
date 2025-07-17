param(
    [string]$SubscriptionId,
    [string]$ResourceGroupName,
    [string]$BaseName,
    [Parameter(Mandatory=$true)]
    [string]$Area,
    [int]$DeleteAfterHours = 12,
    [switch]$Unique
)

. "$PSScriptRoot/../common/scripts/common.ps1"

$context = Get-AzContext
$account = $context.Account

function New-StringHash([string[]]$strings) {
    $string = $strings -join ' '
    $hash = [System.Security.Cryptography.SHA1]::Create()
    $bytes = [System.Text.Encoding]::UTF8.GetBytes($string)
    $hashBytes = $hash.ComputeHash($bytes)
    return [BitConverter]::ToString($hashBytes) -replace '-', ''
}

$suffix = $Unique ? [guid]::NewGuid().ToString() : (New-StringHash $account.Id, $SubscriptionId).ToLower().Substring(0, 8)

if(!$BaseName) {
    $BaseName = "mcp$($suffix)"
}

if(!$ResourceGroupName) {
    $username = $account.Id.Split('@')[0]
    $ResourceGroupName = "$username-mcp$($suffix)"
}

Push-Location $RepoRoot
try {
    $testResourcesDirectory = "$RepoRoot/areas/$($Area.ToLower())/tests/"
    $bicepPath = "$testResourcesDirectory/test-resources.bicep"
    if(!(Test-Path -Path $bicepPath)) {
        Write-Error "Test resources bicep template '$bicepPath' does not exist."
        return
    }

    $staticSuffix = $context.Subscription.Id.SubString(0, 4)
    $staticResourceGroupName = "mcp-static-$staticSuffix"
    $staticBaseName = "mcp$staticSuffix"

    $additionalParameters = @{
        StaticResourceGroupName = $staticResourceGroupName
        StaticBaseName = $staticBaseName
    }

    Write-Host "Deploying:`n  ResourceGroupName: '$ResourceGroupName'`n  BaseName: '$BaseName'`n  DeleteAfterHours: $DeleteAfterHours`n  TestResourcesDirectory: '$testResourcesDirectory'`n  AdditionalParameters: $($additionalParameters | ConvertTo-Json -Compress)"
    if($SubscriptionId) {
        ./eng/common/TestResources/New-TestResources.ps1 `
            -SubscriptionId $SubscriptionId `
            -ResourceGroupName $ResourceGroupName `
            -BaseName $BaseName `
            -TestResourcesDirectory $testResourcesDirectory `
            -DeleteAfterHours $DeleteAfterHours `
            -AdditionalParameters $additionalParameters
    } else {
        ./eng/common/TestResources/New-TestResources.ps1 `
            -ResourceGroupName $ResourceGroupName `
            -BaseName $BaseName `
            -TestResourcesDirectory $testResourcesDirectory `
            -DeleteAfterHours $DeleteAfterHours `
            -AdditionalParameters $additionalParameters
    }
}
finally {
    Pop-Location
}
