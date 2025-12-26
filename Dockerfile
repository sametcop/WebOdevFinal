# .NET 9.0 Çalışma Zamanı (Burası 8.0 kalmış, 9.0 olmalı)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# .NET 9.0 SDK (Derleme Aracı)
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
