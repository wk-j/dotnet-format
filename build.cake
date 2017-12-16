var name = "DotNetFormat";

var project = $"src/{name}/{name}.csproj";

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