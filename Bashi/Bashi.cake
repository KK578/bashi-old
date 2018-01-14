#tool "nuget:?package=NUnit.ConsoleRunner"

var target = Argument("target", "Default");

Task("Default")
    .IsDependentOn("Build");
    
Task("Build")
    .Does(() =>
    {
        MSBuild("../Bashi.sln");
    });

Task("TestOnly")
    .Does(() => 
    {
        var assemblies = GetFiles("../*.Test/bin/Debug/*.Test.dll");
        var settings = new NUnit3Settings { NoResults = true };

        NUnit3(assemblies, settings);
    });

Task("Test")
    .IsDependentOn("Build")
    .IsDependentOn("TestOnly");

Task("Clean")
    .Does(() =>
    {
        CleanDirectories("../src/**/obj/Debug/");
        CleanDirectories("../src/**/bin/Debug/");
    });

RunTarget(target);
