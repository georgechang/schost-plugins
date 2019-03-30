#addin "nuget:?package=Cake.Figlet&version=1.2.0"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var output = Argument("output", "");

Setup(ctx => {
    Information("");
    Information(Figlet("sugcon.eu"));
	Information("Sitecore Host Messaging Plugins");
});

Task("Clean")
	.DoesForEach(GetFiles("./src/**/*.csproj"), project =>
	{
		DotNetCoreClean(project.GetDirectory().FullPath);
	}
);

Task("Pack")
	.IsDependentOn("Clean")
	.DoesForEach(GetFiles("./src/**/*.csproj"), project =>
	{
		var settings = new DotNetCorePackSettings {
			Configuration = configuration
		};

		if (!string.IsNullOrEmpty(output))
		{
			settings.OutputDirectory = output;
		}

		DotNetCorePack(project.GetDirectory().FullPath, settings);
	});

Task("Default")
	.IsDependentOn("Pack");

RunTarget(target);