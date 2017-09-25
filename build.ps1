function exec {
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd
    )

    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $lastexitcode)
    }
}

$artifactsDir="$PSScriptRoot\artifacts"

if(Test-Path $artifactsDir) { Remove-Item $artifactsDir -Force -Recurse }

$build_number = @{ $true = $env:APPVEYOR_BUILD_NUMBER; $false = 1 }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];

exec { dotnet restore --verbosity normal /property:BuildNumber=$build_number }

exec { dotnet build --configuration Release --verbosity normal /property:BuildNumber=$build_number }

exec { dotnet pack --configuration Release --output "$artifactsDir" --no-build --verbosity normal /property:BuildNumber=$build_number }
