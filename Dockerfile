# Çalışma Zamanı (Runtime) - 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Derleme (Build) - 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["UrunYonetimSistemi.csproj", "."]
RUN dotnet restore "./UrunYonetimSistemi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "UrunYonetimSistemi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrunYonetimSistemi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrunYonetimSistemi.dll"]
