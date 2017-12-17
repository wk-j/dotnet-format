var name = "DotNetFormat";

var project = $"src/{name}/{name}.csproj";

Task("Zip")
    .IsDependentOn("Build")
    .Does(() => {
        Zip($"src/{name}/bin/Debug/net47", "publish/dotnet-format.0.3.0.zip");
    });


Task("Build").Does(() => {
    MSBuild(project, settings => {
        settings.WithTarget("Build");
    });
});

Task("Run")
    .IsDependentOn("Build")
    .Does(() => {
        MSBuild(project, settings => {
            settings.WithTarget("Run");
        });
});

var target = Argument("target", "default");
RunTarget(target);