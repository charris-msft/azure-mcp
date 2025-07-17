$RepositoryRoot = $PSScriptRoot
$workPath = "$RepositoryRoot\.work"
$slnPath = "$RepositoryRoot\AzureMcp.sln"

$ErrorActionPreference = "Stop"
function CreateTestProject {
    param (
        [string]$Path,
        [string[]]$projectDependencies = @()
    )
    Write-Host "- Creating $Template $Path..." -ForegroundColor Cyan
    New-Item -Path $Path -ItemType Directory -Force | Out-Null
    Push-Location $Path
    try {
        $name = Split-Path -Path $Path -Leaf
        $projFile = "$Path\$name.csproj"
@"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <IsTestProject>true</IsTestProject>
    <OutputType>Exe</OutputType>
  </PropertyGroup>

  <ItemGroup>
$($projectDependencies | Where-Object { Test-Path $_ } | ForEach-Object {"    <ProjectReference Include=`"$(Resolve-Path -Path $_ -Relative)`" />`n"})
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="NSubstitute" />
    <PackageReference Include="NSubstitute.Analyzers.CSharp" />
    <PackageReference Include="xunit.v3" />
    <PackageReference Include="xunit.runner.visualstudio" />
    <PackageReference Include="coverlet.collector" />
  </ItemGroup>
</Project>
"@ | Set-Content -Path $projFile

        #dotnet sln $slnPath add $projFile | Out-Host
    }
    finally {
        Pop-Location
    }
    return $projFile
}

function CreateLibraryProject {
    param (
        [string]$Path,
        [string[]]$projectDependencies = @()
    )
    Write-Host "- Creating $Template $Path..." -ForegroundColor Cyan
    New-Item -Path $Path -ItemType Directory -Force | Out-Null
    Push-Location $Path
    try {
        $name = Split-Path -Path $Path -Leaf
        $projFile = "$Path\$name.csproj"
@"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <IsAotCompatible>true</IsAotCompatible>
  </PropertyGroup>

  <ItemGroup>
$($projectDependencies | ForEach-Object {"    <ProjectReference Include=`"$(Resolve-Path -Path $_ -Relative)`" />`n"})
  </ItemGroup>
</Project>
"@ | Set-Content -Path $projFile

        #dotnet sln $slnPath add $projFile | Out-Host
    }
    finally {
        Pop-Location
    }
    return $projFile
}

function CreateCliProject {
    param (
        [string]$Path,
        [string[]]$projectDependencies = @()
    )
    Write-Host "- Creating $Template $Path..." -ForegroundColor Cyan
    New-Item -Path $Path -ItemType Directory -Force | Out-Null
    Push-Location $Path
    try {
        $name = Split-Path -Path $Path -Leaf
        $projFile = "$Path\$name.csproj"
@"
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
$($projectDependencies | ForEach-Object {"    <ProjectReference Include=`"$(Resolve-Path -Path $_ -Relative)`" />`n"})
  </ItemGroup>
</Project>
"@ | Set-Content -Path $projFile

        #dotnet sln $slnPath add $projFile | Out-Host
    }
    finally {
        Pop-Location
    }
    return $projFile
}

function CreateArea{
    param(
        [string]$cliProjectPath,
        [string]$areaName,
        [string]$areaProjectName,
        [string[]]$libDependencies = @(),
        [string[]]$testDependencies = @(),
        [hashtable]$packageVersions
    )

    Write-Host "Creating area $areaName..." -ForegroundColor Yellow
    $areaPath = "$RepositoryRoot\areas\$areaName"

    $projPath = CreateLibraryProject -Path "$areaPath\src\$areaProjectName" -projectDependencies $libDependencies
    CreateTestProject -Path "$areaPath\tests\$areaProjectName.UnitTests" -projectDependencies $projPath, $testDependencies
    CreateTestProject -Path "$areaPath\tests\$areaProjectName.LiveTests" -projectDependencies $cliProjectPath, $testDependencies

    Copy-Item -Path "$workPath\src\GlobalUsings.cs" -Destination "$areaPath\src\$areaProjectName" -Force

    AddPackageReferences -Path "$areaPath\src\$areaProjectName" -PackageVersions $packageVersions
    AddPackageReferences -Path "$areaPath\tests\$areaProjectName.UnitTests" -PackageVersions $packageVersions
    AddPackageReferences -Path "$areaPath\tests\$areaProjectName.LiveTests" -PackageVersions $packageVersions
}

function AddPackageReferences {
    param(
        [string]$Path,
        [string[]]$AdditionalPackages = @(),
        [hashtable]$PackageVersions
    )
    Write-Host "Adding package references to $Path..." -ForegroundColor Cyan
    $projFile = Get-ChildItem -Path $Path -Filter "*.csproj" | Select-Object -ExpandProperty FullName -First 1
    if(!$projFile) {
        Write-Host "No project file found in $Path." -ForegroundColor Yellow
        return;
    }

    $sourceFiles = Get-ChildItem -Path $Path -Recurse -Include *.cs

    $usedNamespaces = $sourceFiles
    | Get-Content
    | Where-Object { $_ -match '(global )?using\s+([a-zA-Z0-9\.]+);' }
    | ForEach-Object { $Matches[2] }
    | Sort-Object -Unique

    $matchingPackages = @{}
    foreach($packageName in $PackageVersions.Keys) {
        if($AdditionalPackages -contains $packageName) {
            $matchingPackages[$packageName] = $PackageVersions[$packageName]
            continue;
        }

        foreach ($namespace in $usedNamespaces) {
            if ($namespace.StartsWith($packageName)) {
                $matchingPackages[$packageName] = $PackageVersions[$packageName]
                break;
            }
        }
    }

    [xml]$proj = Get-Content -Path $projFile
    $itemGroup = $proj.Project.ItemGroup | Where-Object { $_.PackageReference }
    if(!$itemGroup) {
        $itemGroup = $proj.CreateElement("ItemGroup")
        $proj.Project.AppendChild($itemGroup) | Out-Null
    }
    foreach ($packageName in $matchingPackages.Keys | Sort-Object) {
        Write-Host "Adding package $packageName version $($matchingPackages[$packageName]) to $projFile..." -ForegroundColor Cyan
        $packageReference = $proj.CreateElement("PackageReference")
        $packageReference.SetAttribute("Include", $packageName)
        $itemGroup.AppendChild($packageReference) | Out-Null
    }
    $proj.Save($projFile)
}

function MigrateArea {
    param(
        [string]$areaName,
        [string]$azureCliPath,
        [string]$azureCorePath,
        [string]$azureTestsCorePath,
        [hashtable]$packageVersions
    )

    $areaDirectoryName = $areaName.ToLower()
    Write-Host "Migrating area $areaName..." -ForegroundColor Yellow

    $sourcePath = "$workPath\src\areas\$areaName"
    if(!(Test-Path $sourcePath)) {
        Write-Host "Source path $sourcePath does not exist." -ForegroundColor Yellow
        return;
    }

    $areaProjectName = "AzureMcp.$areaName"

    CreateArea -areaName $areaDirectoryName `
        -serverCliProjectPath $azureCliPath `
        -areaProjectName $areaProjectName `
        -libDependencies $azureCorePath `
        -testDependencies $azureTestsCorePath

    $targetPath = "$RepositoryRoot\areas\$areaDirectoryName\src\AzureMcp.$areaName"
    Write-Host "Migrating source files from $sourcePath to $targetPath" -ForegroundColor Cyan
    Move-Item -Path "$sourcePath\*" -Destination $targetPath -Force
    AddPackageReferences -Path $targetPath -PackageVersions $packageVersions

    $sourcePath = "$workPath\tests\Areas\$areaName\UnitTests"
    if(Test-Path $sourcePath) {
        $targetPath = "$RepositoryRoot\areas\$areaDirectoryName\tests\AzureMcp.$areaName.UnitTests"
        Write-Host "Migrating unit test files from $sourcePath to $targetPath" -ForegroundColor Cyan
        Move-Item -Path "$sourcePath\*" -Destination $targetPath -Force
    }

    $sourcePath = "$workPath\tests\Areas\$areaName\LiveTests"
    if(Test-Path $sourcePath) {
        $targetPath = "$RepositoryRoot\areas\$areaDirectoryName\tests\AzureMcp.$areaName.LiveTests"
        Write-Host "Migrating live test from $sourcePath to $targetPath" -ForegroundColor Cyan
        Move-Item -Path "$sourcePath\*" -Destination $targetPath -Force
    }

    $bicepFile = "$workPath\infra\services\$areaDirectoryName.bicep"
    if(Test-Path $bicepFile) {
        $targetPath = "$RepositoryRoot\areas\$areaDirectoryName\tests\test-resources.bicep"
        Write-Host "Migrating Bicep file from $bicepFile to $targetPath" -ForegroundColor Cyan
        Move-Item -Path $bicepFile -Destination $targetPath -Force
    }

    $postScriptFile = "$workPath\infra\services\$areaDirectoryName-post.ps1"
    if(Test-Path $postScriptFile) {
        $targetPath = "$RepositoryRoot\areas\$areaDirectoryName\tests\test-resources-post.ps1"
        Write-Host "Migrating post script file from $postScriptFile to $targetPath" -ForegroundColor Cyan
        Move-Item -Path $postScriptFile -Destination $targetPath -Force
    }
}

function ResetAndInitialize {
    git add .\convert-repo.ps1
    git restore .
    git clean -xdf

    # Move all existing source into a temporary directory ".work"
    if(Test-Path $workPath) {
        Write-Host "Temporary work directory already exists at $workPath. Removing it..." -ForegroundColor Yellow
        Remove-Item $workPath -Recurse -Force
    }

    New-Item -ItemType Directory -Path $workPath | Out-Null

    Write-Host "Moving existing files to temporary work directory..." -ForegroundColor Cyan
    Move-Item -Path "$RepositoryRoot\src" -Destination $workPath -Force
    Move-Item -Path "$RepositoryRoot\tests" -Destination $workPath -Force
    Move-Item -Path "$RepositoryRoot\infra" -Destination $workPath -Force
    Move-Item -Path "$RepositoryRoot\*.sln" -Destination $workPath -Force
}

function UpdateDirectoryBuildProps {
    $dirPropsPath = "$RepositoryRoot/Directory.Build.props"
    [xml]$proj = Get-Content "$workPath/src/AzureMcp.csproj"
    $version = $proj.Project.PropertyGroup.Version | Select-Object -First 1

    [xml]$dirsProps = Get-Content -Path $dirPropsPath -Raw
    $propertyGroup = $dirsProps.Project.PropertyGroup | Select-Object -First 1
    $versionNode = $propertyGroup.PrependChild($dirsProps.CreateElement("Version"))
    $versionNode.InnerText = $version

    $commentNode = $propertyGroup.AppendChild($dirsProps.CreateComment(''))
    $commentNode.InnerText = ' Suppress SYSLIB0020 for generated System.Text.Json code that uses obsolete IgnoreNullValues '
    $noWarnNode = $propertyGroup.AppendChild($dirsProps.CreateElement("NoWarn"))
    $noWarnNode.InnerText = '$(NoWarn);SYSLIB0020'

    $dirsProps.Save($dirPropsPath)
}

function AddProjectsToSolution {
    $projects = Get-ChildItem -Path $RepositoryRoot -Filter "*.csproj" -Recurse
    foreach ($project in $projects) {
        dotnet sln add $project.FullName | Out-Host
    }
}

function UpdateCodeFiles {
    $internalClasses = @('TelemetryActivityExtensions.cs', 'GlobalCommand.cs', 'TelemetryConstants.cs')
    $excludeNamespaces = @("AzureMcp", "Microsoft.Extensions.DependencyInjection")

    $namespaceChanges = @{}

    $projects = Get-ChildItem -Path $RepositoryRoot -Filter "*.csproj" -Recurse
        | Where-Object { $_.FullName -like '*\core\*' -or $_.FullName -like '*\areas\*' }

    foreach ($project in $projects) {
        $projectDirectory = Split-Path -Path $project.FullName -Parent
        $codeFiles = Get-ChildItem -Path $projectDirectory -Recurse -Include '*.cs'
        $rootNamespace = $project.Name.Replace('.csproj', '')

        foreach ($file in $codeFiles) {
            if ($file.DirectoryName -eq $projectDirectory) {
                # If the file is in the root of the project, use the root namespace directly
                $newNamespace = $rootNamespace
            } else {
                # Otherwise, calculate the relative namespace based on the project directory
                $relativeNamespace = $file.DirectoryName.Substring($projectDirectory.Length + 1).Replace('\', '.')
                $newNamespace = "$rootNamespace.$relativeNamespace"
            }

            $content = Get-Content -Path $file.FullName -Raw
            # Replace core namespaces (AzureMcp.Commands -> AzureMcp.Core.Commands, etc.)
            if ($content -match '\n(\s*)namespace\s+([\w\.]+)') {
                $currentNamespace = $Matches[2]
                $namespaceStatement = $Matches[0]

                if ($currentNamespace -ne $newNamespace -and $currentNamespace -notin $excludeNamespaces) {
                    $newNamespaceStatement = $namespaceStatement.Replace($currentNamespace, $newNamespace)
                    Write-Host "Updating namespace in $($file.FullName) from '$currentNamespace' to '$newNamespace'" -ForegroundColor Cyan
                    $content = $content.Replace($namespaceStatement, $newNamespaceStatement)
                    $namespaceChanges[$currentNamespace] = $newNamespace
                }
            }

            # replace internal classes with public classes
            if ($internalClasses -contains $file.Name) {
                $content = $content -replace '^(\s*)internal (.* )?class ', '$1public $2class '
            }

            $content = $content.TrimEnd(@("`n", "`r"))
            Set-Content -Path $file.FullName -Value $content
        }
    }

    $sortedKeys = $namespaceChanges.Keys | Sort-Object { $_.Length } -Descending

    # apply namespace changes to all code files
    foreach ($project in $projects) {
        $projectDirectory = Split-Path -Path $project.FullName -Parent
        $codeFiles = Get-ChildItem -Path $projectDirectory -Recurse -Include '*.cs'

        foreach ($file in $codeFiles) {
            $content = Get-Content -Path $file.FullName -Raw
            $newContent = $content
            foreach ($oldNamespace in $sortedKeys) {
                $newContent = $newContent.Replace($oldNamespace, $namespaceChanges[$oldNamespace])
            }
            # if the file references IAreaSetup, it should have a using statement for AzureMcp.Core.Areas
            if ($file.Name -ne 'IAreaSetup.cs' -and $newContent -match 'IAreaSetup') {
                $usingStatement = "using AzureMcp.Core.Areas;"

                if (-not $newContent.Contains($usingStatement)) {
                    #Add a using statement before the namespace declaration
                    $newContent = $newContent -replace '\n( *namespace )', "$usingStatement`n`n`$1"
                }
            }

            $newContent = $newContent.TrimEnd(@("`n", "`r"))
            if ($newContent -ne $content) {
                Write-Host "Updating namespace usage in $($file.FullName)" -ForegroundColor Cyan
                Set-Content -Path $file.FullName -Value $newContent
            }
        }
    }
}

# Setup script for an Area based repository structure
# Creates directory structure and C# projects as defined in README.md

Write-Host "Setting up area based repository structure..." -ForegroundColor Green
# Set repository root to current directory

Push-Location $RepositoryRoot
try {
    $packagesPropsPath = "$RepositoryRoot\Directory.Packages.props"
    $cliDirectory = "$RepositoryRoot\core\src\AzureMcp.Cli"
    $coreDirectory = "$RepositoryRoot\core\src\AzureMcp.Core"

    ResetAndInitialize
    UpdateDirectoryBuildProps

    Write-Host "Creating solution file at $slnPath..." -ForegroundColor Cyan
    dotnet new sln -n "AzureMcp" -o $RepositoryRoot

    $packagesToAdd = @{
        'Azure.ResourceManager' = '1.13.1'
        'xunit.v3.assert' = '2.0.2'
        'xunit.v3.extensibility.core' = '2.0.2'
    }

    [xml]$packagesProps = Get-Content -Path $packagesPropsPath -Raw
    foreach ($package in $packagesToAdd.GetEnumerator()) {
        $element = $packagesProps.CreateElement("PackageVersion")
        $element.SetAttribute("Include", $package.Key)
        $element.SetAttribute("Version", $package.Value)
        $packagesProps.Project.ItemGroup.AppendChild($element)
    }
    $packagesProps.Save($packagesPropsPath)

    $packageVersions = @{}
    $packagesProps.Project.ItemGroup.PackageVersion | ForEach-Object { $packageVersions[$_.Include] = $_.Version }

    $areas = Get-ChildItem -Path "$workPath/src/areas" -Directory | Select-Object -ExpandProperty Name

    # Create directory structure
    Write-Host "Creating Core..." -ForegroundColor Yellow

    $azureCorePath = CreateLibraryProject -Path $coreDirectory
    $azureCliPath = CreateCliProject -Path $cliDirectory -projectDependencies $azureCorePath
    Copy-Item -Path "$workPath\src\GlobalUsings.cs" -Destination $cliDirectory -Force

    # add the wildcard areas reference to the cli project
    $cliProjFile = Get-ChildItem -Path $azureCliPath -Filter "*.csproj" | Select-Object -ExpandProperty FullName -First 1
    [xml]$proj = Get-Content -Path $cliProjFile
    $reference = $proj.CreateElement("ProjectReference")
    $reference.SetAttribute("Include", "..\..\..\areas\*\src\**\AzureMcp.*.csproj")
    $proj.Project.ItemGroup.AppendChild($reference) | Out-Null
    $proj.Save($cliProjFile)

    $azureTestsCorePath = CreateLibraryProject -Path "$RepositoryRoot\core\tests\AzureMcp.Tests" -projectDependencies $azureCorePath
    CreateTestProject -Path "$RepositoryRoot\core\tests\AzureMcp.Core.UnitTests" -projectDependencies $azureTestsCorePath, $azureCorePath
    CreateTestProject -Path "$RepositoryRoot\core\tests\AzureMcp.Cli.UnitTests" -projectDependencies $azureCliPath, $azureTestsCorePath

    Write-Host "Core created successfully!" -ForegroundColor Green

    Write-Host "Creating areas..." -ForegroundColor Yellow

    foreach ($area in $areas) {
        MigrateArea `
            -AreaName $area `
            -azureCliPath $azureCliPath `
            -azureCorePath $azureCorePath `
            -azureTestsCorePath $azureTestsCorePath `
            -PackageVersions $packageVersions
    }

    while($true)
    {
        $emptyDirectories = Get-ChildItem -Path $RepositoryRoot -Directory -Recurse | Where-Object { !(Get-ChildItem $_) }
        $emptyDirectories | Remove-Item
        if($emptyDirectories.Count -eq 0) {
            break;
        }
    }

    # All remaining files are "Core" files
    $sourcePath = "$workPath\src"
    $targetPath = "$RepositoryRoot\core\src\AzureMcp.Core"
    Write-Host "Moving core source files from $sourcePath to $targetPath" -ForegroundColor Cyan
    Remove-Item -Path "$sourcePath\*.*proj" -Force -ErrorAction SilentlyContinue
    Move-Item -Path "$sourcePath\*" -Destination $targetPath -Force

    $sourcePath = "$workPath\tests"
    $targetPath = "$RepositoryRoot\core\tests\AzureMcp.Core.UnitTests"
    Write-Host "Moving core test files from $sourcePath to $targetPath" -ForegroundColor Cyan
    # move only files that end with "Tests.cs" to the target folder, keeping the directory structure
    Get-ChildItem -Path $sourcePath -Recurse -Filter "*Tests.cs" | ForEach-Object {
        $relativePath = $_.FullName.Substring($sourcePath.Length + 1)
        $targetFilePath = Join-Path -Path $targetPath -ChildPath $relativePath
        $targetDir = Split-Path -Path $targetFilePath -Parent
        if(!(Test-Path -Path $targetDir)) {
            New-Item -ItemType Directory -Path $targetDir | Out-Null
        }
        Move-Item -Path $_.FullName -Destination $targetFilePath -Force
    }

    $sourcePath = "$workPath\tests"
    $targetPath = "$RepositoryRoot\core\tests\AzureMcp.Tests"
    Write-Host "Moving remaining test helper files from $sourcePath to $targetPath" -ForegroundColor Cyan
    Remove-Item -Path "$sourcePath\*.*proj" -Force -ErrorAction SilentlyContinue
    Move-Item -Path "$sourcePath\*" -Destination $targetPath -Force

    ### Manual adjustments
    Move-Item -Path "$workPath\infra\samples" -Destination "$RepositoryRoot\areas\search\tests" -Force
    Move-Item -Path "$coreDirectory\Commands\Subscription\SubscriptionJsonContext.cs" -Destination "$RepositoryRoot\areas\subscription\src\AzureMcp.Subscription\Commands" -Force
    Move-Item -Path "$coreDirectory\Program.cs" -Destination $cliDirectory -Force

    ## Remove references to AzureMcp.Areas.Group.Commands from JsonSourceGenerationContext.cs
    $path = "$coreDirectory\Commands\JsonSourceGenerationContext.cs"
    $content = Get-Content -Path $path -Raw
    $content = $content.Replace('using AzureMcp.Areas.Group.Commands;`n', '')
    $content = $content.Replace('[JsonSerializable(typeof(GroupListCommand.Result))]`n', '')
    $content | Set-Content -Path $path

    AddPackageReferences -Path $cliDirectory -PackageVersions $packageVersions
    AddPackageReferences -Path $coreDirectory -PackageVersions $packageVersions -AdditionalPackages @('OpenTelemetry.Exporter.OpenTelemetryProtocol', 'Azure.Monitor.OpenTelemetry.AspNetCore', 'System.Linq.AsyncEnumerable', 'Newtonsoft.Json')

    $content = Get-Content -Path $azureCorePath -Raw
    $content = $content -replace '</IsAotCompatible>', "</IsAotCompatible>`n    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>"
    Set-Content -Path $azureCorePath -Value $content

    UpdateCodeFiles

    AddProjectsToSolution

    Write-Host "Azure areas created successfully!" -ForegroundColor Green
}
finally {
    Pop-Location
}
