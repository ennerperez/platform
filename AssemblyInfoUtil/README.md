# Auto AssemblyInfo Updater
Include in `PreBuildEvent`:


    if $(ConfigurationName) == Release $(SolutionDir).tools\AssemblyInfoUtil.exe "$(SolutionDir)Files\AssemblyInfo.Platform.cs" /l:"$(SolutionDir)CHANGELOG.md" /c:4