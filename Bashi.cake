#tool "nuget:?package=NUnit.ConsoleRunner"

var target = Argument("target", "Default");
var solution = "./Bashi.sln";

Task("Default")
    .IsDependentOn("Test");

Task("NuGet:Restore")
    .Does(() =>
    {
        NuGetRestore(solution);
    });
    
Task("Build")
    .IsDependentOn("NuGet:Restore")
    .Does(() =>
    {
        var settings = new MSBuildSettings { Verbosity = Verbosity.Minimal };
        MSBuild(solution, settings);
    });

Task("TestOnly")
    .Does(() => 
    {
        var assemblies = GetFiles("./test/*/bin/Debug/*.Test.dll");
        var settings = new NUnit3Settings { NoResults = true };

        NUnit3(assemblies, settings);
    });

Task("Test")
    .IsDependentOn("Build")
    .IsDependentOn("TestOnly");

Task("Clean")
    .Does(() =>
    {
        Action<string> CleanAndNotify = path =>
                                        {
                                            var files = GetDirectories(path);

                                            CleanDirectories(files);
                                            Information("Cleaned {0} directories.", files.Count);
                                        };

        CleanAndNotify("./src/**/bin/Debug");
        CleanAndNotify("./src/**/obj/Debug");
        CleanAndNotify("./test/**/bin/Debug");
        CleanAndNotify("./test/**/obj/Debug");
    });

Task("Rebuild")
    .IsDependentOn("Clean")
    .IsDependentOn("Build");

RunTarget(target);
