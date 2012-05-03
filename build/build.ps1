$baseDir = Resolve-Path(".")
$outputFolder = Join-Path $baseDir "_build_output\"
$solution = Join-Path $baseDir "source\LinqToRest.sln"
$windir = $env:windir

if((ls "$windir\Microsoft.NET\Framework\v4.0*") -eq $null ) {
	throw "Building requires .NET 4.0, which doesn't appear to be installed on this machine."
}

$v4_net_version = (ls "$windir\Microsoft.NET\Framework\v4.0*").Name

$msbuild = "$windir\Microsoft.NET\Framework\$v4_net_version\MSBuild.exe"

$options = "/noconsolelogger /p:Configuration=Release /p:OutDir=""$outputFolder"""

if ([System.IO.Directory]::Exists($outputFolder)) {
	[System.IO.Directory]::Delete($outputFolder, 1)
}

$build = $msbuild + " ""$solution"" " + $options + " /t:Build"

Invoke-Expression $build
