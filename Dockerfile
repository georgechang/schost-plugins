FROM mcr.microsoft.com/dotnet/core/sdk AS build
WORKDIR /app

# Copy source files
COPY src/GC.Plugin.Messaging.Services ./GC.Plugin.Messaging.Services

# Restore NuGet dependencies
RUN dotnet restore ./GC.Plugin.Messaging.Services/GC.Plugin.Messaging.Services.csproj

# Build NuGet packages
RUN dotnet pack -c Release -o out ./GC.Plugin.Messaging.Services/GC.Plugin.Messaging.Services.csproj

# Copy license file to staging folder
COPY license.xml /temp/sitecoreruntime/

# Copy utilities over to build container
COPY utilities /utilities

# Extract DLLs from built NuGet package
#RUN /utilities/7za.exe e -o\temp\sitecoreruntime\production -y -bsp0 -bso0 GC.Plugin.Messaging.Services/out/GC.Plugin.Messaging.Services.1.0.0.nupkg -ir!lib\*.dll
RUN /utilities/7za.exe e -o\temp\sitecoreruntime\production -y -bsp0 -bso0 -an -air!*.nupkg -ir!lib\*.dll
RUN /utilities/7za.exe x -o\temp\sitecoreruntime\production\sitecore\* -y -bsp0 -bso0 -an -air!*.nupkg -ir!sitecore\*

# FROM george.azurecr.io/schost:latest AS runtime
FROM mcr.microsoft.com/dotnet/core/runtime:latest AS runtime
WORKDIR /app

COPY --from=build ./stage ./

#ENTRYPOINT ["dotnet", "ScHost.dll", "web"]