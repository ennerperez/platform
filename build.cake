#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./build") + Directory(configuration);

// Define solutions.
var solutions = new Dictionary<string, string> {
     { "./src/platform.netcore.sln", "Any" },
     { "./src/platform.sln", "Any" },
};

// Define AssemblyInfo source.
var assemblyInfoVersion = ParseAssemblyInfo("./.files/AssemblyInfo.Version.cs");
var assemblyInfoCommon = ParseAssemblyInfo("./.files/AssemblyInfo.Common.cs");

// Define version.
var ticks = DateTime.Now.ToString("ddHHmmss");
var assemblyVersion = assemblyInfoVersion.AssemblyVersion.Replace(".*", "." + ticks.Substring(ticks.Length-8,8));
var version = EnvironmentVariable("APPVEYOR_BUILD_VERSION") ?? Argument("version", assemblyVersion);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
	CleanDirectories("./**/samples/packages");
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    foreach (var solution in solutions)
    {
        NuGetRestore(solution.Key);
    }
});

Task("BuildCore")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    var netcoreBuildSettings = new DotNetCoreBuildSettings
    {
        Framework = "netcoreapp3.1",
        Configuration = configuration
    };
    DotNetCoreBuild(solutions.ElementAt(0).Key, netcoreBuildSettings);
});

Task("Build")
    .IsDependentOn("BuildCore")
    .Does(() =>
{

    
    if (IsRunningOnWindows())
    {
        var settings = new MSBuildSettings()
            .WithProperty("PackageVersion", version)
            .WithProperty("BuildSymbolsPackage", "false");
            settings.SetConfiguration(configuration);
        // Use MSBuild
        MSBuild(solutions.ElementAt(1).Key, settings);
    }
    else
    {
        var settings = new XBuildSettings()
            .WithProperty("PackageVersion", version)
            .WithProperty("BuildSymbolsPackage", "false");
            settings.SetConfiguration(configuration);
        // Use XBuild
        XBuild(solutions.ElementAt(1).Key, settings);
    }
   
});

Task("Build-NuGet-Packages")
    .Does(() =>
    {
        foreach (var folder in new System.IO.FileInfo(solutions.ElementAt(1).Key).Directory.GetDirectories())
        {
            foreach (var file in folder.GetFiles("*.nuspec"))
            {
        		var path = file.Directory;
                var assemblyInfo = ParseAssemblyInfo(path + "/Properties/AssemblyInfo.cs");
                var nuGetPackSettings = new NuGetPackSettings()
                {
                    OutputDirectory = buildDir,
                    IncludeReferencedProjects = false,
                    Id = assemblyInfo.Title.Replace(" ", "."),
                    Title = assemblyInfo.Title,
                    Version = version,
                    Authors = new[] { assemblyInfoCommon.Company },
                    Summary = assemblyInfo.Description,
                    Copyright = assemblyInfoCommon.Copyright,
                    Properties = new Dictionary<string, string>()
                    {{ "Configuration", configuration }}
                };
                NuGetPack(file.FullName, nuGetPackSettings);
            }
        }
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit3("./src/**/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {
        NoResults = true
        });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests")
	.IsDependentOn("Build-NuGet-Packages");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);