FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /

COPY . .

RUN dotnet restore

COPY . .

RUN dotnet build "src/Api/Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Api/Api.csproj" -c Release -o /app/publish /p:GenerateDocumentationFile=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "src/Api/Api.dll"]
