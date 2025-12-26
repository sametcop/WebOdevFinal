# .NET 9.0 Çalışma Zamanı
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# .NET 9.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["UrunYonetimSistemi.csproj", "."]
RUN dotnet restore "./UrunYonetimSistemi.csproj"
COPY . .
WORKDIR "/src/."

# --- DÜZELTME BURADA YAPILDI ---
# /p:UseAppHost=false komutunu ekledik.
# Bu komut, klasör isminle çakışan başlatma dosyasını oluşturmaz, hatayı engeller.
RUN dotnet build "UrunYonetimSistemi.csproj" -c Release -o /app/build /p:UseAppHost=false

FROM build AS publish
RUN dotnet publish "UrunYonetimSistemi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrunYonetimSistemi.dll"]
