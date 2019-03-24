#addin "nuget:?package=Cake.PowerShell&version=0.4.7"
#addin "nuget:?package=Cake.Figlet&version=1.2.0"
#addin "nuget:?package=Cake.Docker&version=0.9.9"

var hostPath = @"C:\inetpub\wwwroot\sc910.identityserver";

var target = Argument("target", "Default");

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
	.DoesForEach(GetFiles("./src/**/*.csproj"), project =>
	{
		DotNetCorePack(project.GetDirectory().FullPath, new DotNetCorePackSettings {
			IncludeSymbols = true
		});
	});

Task("Deploy Plugins")
	.IsDependentOn("Pack")
	.DoesForEach(GetFiles("./src/**/*.nupkg"), package => 
	{
		StartPowershellScript("Install-ScHostPackage", args => 
		{
			args.Append("Path", package.FullPath)
				.Append("HostPath", hostPath)
				.Append("Runtime", "");
		});
	});

Task("DockerBuild")
	.Does(() => {
		DockerBuild(
			new DockerImageBuildSettings {
				Rm = true,
				Isolation = "process",
				Tag = new string[] { "schost:messaging" }
			},
			"."
		);
	});

Task("DockerRun")
	.Does(() => {
		DockerRun(
			new DockerContainerRunSettings {
				Isolation = "process"
			},
			"schost:messaging",
			""
		);
	});

Task("DockerRm")
	.Does(() => {

	});

Task("Default");

RunTarget(target);