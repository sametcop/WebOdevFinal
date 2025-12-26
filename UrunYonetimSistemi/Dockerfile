# .NET çalışma zamanı imajını kullan
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

# Derleme imajını kullan
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
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
