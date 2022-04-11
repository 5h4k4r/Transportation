FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["src/Transportation.Api/Transportation.Api.csproj", "."]
RUN dotnet restore "Transportation.Api.csproj"
COPY src/Transportation.Api .
RUN dotnet build "Transportation.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transportation.Api.csproj" -c Release -o /app/publish /p:GenerateDocumentationFile=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Transportation.Api.dll"]