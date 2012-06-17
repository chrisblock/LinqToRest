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

$build = "$msbuild ""$solution"" $options /t:Build"

Invoke-Expression $build

$nuget = Join-Path $baseDir "tools\NuGet\nuget.exe"

$linqToRestProject = Join-Path $baseDir "source\LinqToRest\LinqToRest.csproj"

$package = "$nuget pack $linqToRestProject -Prop Configuration=Release"

Invoke-Expression $package

$linqToRestClientProject = Join-Path $baseDir "source\LinqToRest.Client\LinqToRest.Client.csproj"

$package = "$nuget pack $linqToRestClientProject -Prop Configuration=Release"

Invoke-Expression $package

$linqToRestServerProject = Join-Path $baseDir "source\LinqToRest.Server\LinqToRest.Server.csproj"

$package = "$nuget pack $linqToRestServerProject -Prop Configuration=Release"

Invoke-Expression $package
