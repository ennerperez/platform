# UpdateVersion
Include in `PreBuildEvent`:

    if $(ConfigurationName) == Debug    "$(SolutionDir).tools\UpdateVersion.exe" "$(SolutionDir)Files\AssemblyInfo.Platform.cs" /l:"$(SolutionDir)CHANGELOG.md" /c:4 /i:$(ConfigurationName)
    if $(ConfigurationName) == Release  "$(SolutionDir).tools\UpdateVersion.exe" "$(SolutionDir)Files\AssemblyInfo.Platform.cs" /l:"$(SolutionDir)CHANGELOG.md" /i:RTM