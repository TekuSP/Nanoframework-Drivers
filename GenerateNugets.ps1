$nugetsToProcess = (Get-ChildItem -Path "." -Filter *.nuspec -r | % { echo $_.FullName });
Foreach ($nuget in $nugetsToProcess)
{
    echo "-----------------------------------";
    echo "Packing nuget file: $nuget";
    &"nuget.exe" "pack" "$nuget" "-properties" "Configuration=Release" "-IncludeReferencedProjects" | Write-Host;
    echo "Finished packing nuget file: $nuget";
}
