# Etapa base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MeuSiteMVC/MeuSiteMVC.csproj", "MeuSiteMVC/"]
RUN dotnet restore "MeuSiteMVC/MeuSiteMVC.csproj"
WORKDIR "/src/MeuSiteMVC"
COPY . .
RUN dotnet build "MeuSiteMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeuSiteMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Volume para persistência (opcional)
VOLUME /var/data_protection_keys

# Ambiente para HTTPS (certificado será montado via volume)
ENV ASPNETCORE_ENVIRONMENT="Docker"
ENV ASPNETCORE_URLS="https://+:443;http://+:80"
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="Teste@123"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetapp.pfx"

ENTRYPOINT ["dotnet", "MeuSiteMVC.dll"]
