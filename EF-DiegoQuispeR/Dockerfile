FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 44374

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EF-DiegoQuispeR.csproj", "./"]
RUN dotnet restore "./EF-DiegoQuispeR.csproj"
COPY . .
RUN dotnet build "EF-DiegoQuispeR.csproj" -c Release -o /app/build
RUN dotnet publish "EF-DiegoQuispeR.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar certificado ra�z y actualizar el almac�n de certificados
COPY certs/SQLServerDevCert.cer /usr/local/share/ca-certificates/SQLServerDevCert.crt
RUN update-ca-certificates

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "EF-DiegoQuispeR.dll"]



