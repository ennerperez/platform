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

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    foreach (var solution in solutions)
    {
        foreach (var folder in new System.IO.FileInfo(solution.Key).Directory.GetDirectories())
        {
            if (!folder.Name.EndsWith("NetCore"))
            {
                foreach (var file in folder.GetFiles("*.csproj"))
                {
                    var outputPath = System.IO.Path.Combine("./", file.Directory.FullName, "bin", configuration);
				    if (IsRunningOnWindows())
                    {
                        var settings = new MSBuildSettings()
                        .UseToolVersion(MSBuildToolVersion.VS2019)
			            .WithProperty("VisualStudioVersion", new string[]{"16.0"})
                        .WithProperty("PackageVersion", version)
                        .WithProperty("BuildSymbolsPackage", "false")
                        .WithProperty("OutputPath", outputPath);
                        settings.SetConfiguration(configuration);
                        // Use MSBuild
                        MSBuild(file.FullName, settings);
                    }
                    else
                    {
                        var settings = new XBuildSettings()
                        .WithProperty("PackageVersion", version)
                        .WithProperty("BuildSymbolsPackage", "false")
                        .WithProperty("OutputPath", outputPath);
                        settings.SetConfiguration(configuration);
                        // Use XBuild
                        XBuild(file.FullName, settings);
                    }
                }
            }
            else
            {
                foreach (var file in folder.GetFiles("*.csproj"))
                {
                    var outputPath = System.IO.Path.Combine("./", file.Directory.FullName, "bin", configuration);
                    var netcoreBuildSettings = new DotNetCoreBuildSettings
                    {
                        Framework = "netcoreapp3.1",
                        Configuration = configuration,
                        OutputDirectory = outputPath
                    };
                    DotNetCoreBuild(file.FullName, netcoreBuildSettings);
                }
            }
        }
    }
   
});

Task("Build-NuGet-Packages")
    .IsDependentOn("Build")
    .Does(() =>
    {
        foreach (var solution in solutions)
        {
            foreach (var folder in new System.IO.FileInfo(solution.Key).Directory.GetDirectories())
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