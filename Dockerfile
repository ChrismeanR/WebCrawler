#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

# Pull this docker image
FROM mcr.microsoft.com/dotnet/core/runtime:3.1-nanoserver-1809 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809 AS build
WORKDIR /src
COPY ["WebCrawler.CR.Core.csproj", ""]
RUN dotnet restore "./WebCrawler.CR.Core.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebCrawler.CR.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebCrawler.CR.Core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebCrawler.CR.Core.dll"]