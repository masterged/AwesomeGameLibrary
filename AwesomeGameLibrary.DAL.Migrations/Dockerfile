FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AwesomeGameLibrary.DAL.Migrations/AwesomeGameLibrary.DAL.Migrations.csproj", "AwesomeGameLibrary.DAL.Migrations/"]
RUN dotnet restore "AwesomeGameLibrary.DAL.Migrations/AwesomeGameLibrary.DAL.Migrations.csproj"
COPY . .
WORKDIR "/src/AwesomeGameLibrary.DAL.Migrations"
RUN dotnet build "AwesomeGameLibrary.DAL.Migrations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwesomeGameLibrary.DAL.Migrations.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwesomeGameLibrary.DAL.Migrations.dll"]
