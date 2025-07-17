$commonProjPath = Resolve-Path "$PSScriptRoot\core\tests\AzureMcp.Tests\AzureMcp.Tests.csproj"
$cliProjPath = Resolve-Path "$PSScriptRoot\core\src\AzureMcp.Cli\AzureMcp.Cli.csproj"

$projFiles = Get-ChildItem . -Filter "*UnitTests.csproj" -Recurse
foreach ($projFile in $projFiles) {
    Write-Host "Processing $projFile"
    $srcProjPath = $projFile.FullName.Replace('\tests\', '\src\').Replace('.UnitTests', '')
    $relativeSrcProjPath = Resolve-Path -Path $srcProjPath -Relative -RelativeBasePath $projFile.Directory
    $relativeCommonProjPath = Resolve-Path -Path $commonProjPath -Relative -RelativeBasePath $projFile.Directory

    [xml]$proj = Get-Content $projFile.FullName -Raw
    $itemGroup = $proj.Project.ItemGroup.ProjectReference[0].ParentNode
    $itemGroup.RemoveAll()
    $element = $itemGroup.AppendChild($proj.CreateElement('ProjectReference'))
    $element.SetAttribute('Include', $relativeCommonProjPath)
    $element = $itemGroup.AppendChild($proj.CreateElement('ProjectReference'))
    $element.SetAttribute('Include', $relativeSrcProjPath)

    $proj.Save($projFile.FullName)
}

$projFiles = Get-ChildItem . -Filter "*LiveTests.csproj" -Recurse
foreach ($projFile in $projFiles) {
    Write-Host "Processing $projFile"
    $srcProjPath = $projFile.FullName.Replace('\tests\', '\src\').Replace('.LiveTests', '')
    $relativeCliProjPath = Resolve-Path -Path $cliProjPath -Relative -RelativeBasePath $projFile.Directory
    $relativeCommonProjPath = Resolve-Path -Path $commonProjPath -Relative -RelativeBasePath $projFile.Directory

    [xml]$proj = Get-Content $projFile.FullName -Raw
    $itemGroup = $proj.Project.ItemGroup.ProjectReference[0].ParentNode
    $itemGroup.RemoveAll()
    $element = $itemGroup.AppendChild($proj.CreateElement('ProjectReference'))
    $element.SetAttribute('Include', $relativeCliProjPath)
    $element = $itemGroup.AppendChild($proj.CreateElement('ProjectReference'))
    $element.SetAttribute('Include', $relativeCommonProjPath)

    $proj.Save($projFile.FullName)
}
