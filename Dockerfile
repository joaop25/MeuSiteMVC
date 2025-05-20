FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MeuSiteMVC.csproj", "MeuSiteMVC/"]
RUN dotnet restore "MeuSiteMVC/MeuSiteMVC.csproj"
WORKDIR "MeuSiteMVC"
COPY . .

# Criação do diretório antes de exportar o certificado
RUN mkdir -p /root/.aspnet/https

# Geração e exportação do certificado HTTPS
RUN dotnet dev-certs https -v -ep /root/.aspnet/https/aspnetapp.pfx -p teste@123
RUN dotnet dev-certs https -v --trust

RUN dotnet build "MeuSiteMVC.csproj" -c Release -o /app/build

FROM build AS publish
COPY --from=build /root/.aspnet/https/aspnetapp.pfx /root/.aspnet/https/
RUN dotnet publish "MeuSiteMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /root/.aspnet/https/aspnetapp.pfx /root/.aspnet/https/
VOLUME /var/data_protection_keys
ENV ASPNETCORE_ENVIRONMENT="Docker"
ENV ASPNETCORE_URLS="http://+:80;https://+:443;"
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="teste@123"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/root/.aspnet/https/aspnetapp.pfx
ENTRYPOINT ["dotnet", "MeuSiteMVC.dll"]