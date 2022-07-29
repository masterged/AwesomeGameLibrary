FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AwesomeGameLibrary.Web/AwesomeGameLibrary.Web.csproj", "AwesomeGameLibrary.Web/"]
RUN dotnet restore "AwesomeGameLibrary.Web/AwesomeGameLibrary.Web.csproj"
COPY . .
WORKDIR "/src/AwesomeGameLibrary.Web"
RUN dotnet build "AwesomeGameLibrary.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwesomeGameLibrary.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwesomeGameLibrary.Web.dll"]
