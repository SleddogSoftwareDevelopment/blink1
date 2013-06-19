.\src\.nuget\Nuget.exe pack -Symbols -OutputDirectory .\Build .\src\Sleddog.Blink1\Sleddog.Blink1.csproj

.\src\.nuget\Nuget.exe push .\Build\*.nupkg