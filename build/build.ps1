$baseDir = Resolve-Path(".")
$outputFolder = Join-Path $baseDir "_build_output\"
$solution = Join-Path $baseDir "source\LinqToRest.sln"
$windir = $env:windir

if((ls "$windir\Microsoft.NET\Framework\v4.0*") -eq $null ) {
	throw "Building requires .NET 4.0, which doesn't appear to be installed on this machine."
}

$v4_net_version = (ls "$windir\Microsoft.NET\Framework\v4.0*").Name

$msbuild = "$windir\Microsoft.NET\Framework\$v4_net_version\MSBuild.exe"

$options = "/noconsolelogger /p:Configuration=Release"

if ([System.IO.Directory]::Exists($outputFolder)) {
	[System.IO.Directory]::Delete($outputFolder, 1)
}

$clean = "$msbuild ""$solution"" $options /t:Clean"

Invoke-Expression $clean

if ($LastExitCode -ne 0) {
	Write-Output "Error executing '$clean'." -ForegroundColor Red
	Exit 1
}

$build = "$msbuild ""$solution"" $options /t:Build"

Invoke-Expression $build

if ($LastExitCode -ne 0) {
	Write-Output "Error executing '$build'." -ForegroundColor Red
	Exit 1
}

$nuget = Join-Path $baseDir "tools\NuGet\nuget.exe"

$projects = @("LinqToRest", "LinqToRest.Client", "LinqToRest.Server")

foreach ($project in $projects) {
	$projectFilePath = Join-Path $baseDir "source\$project\$project.csproj"

	$package = "$nuget pack $projectFilePath -Prop Configuration=Release"

	Invoke-Expression $package
}
