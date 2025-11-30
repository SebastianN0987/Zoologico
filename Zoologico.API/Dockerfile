# --------------------------------------------------------
# STAGE 1: base (IMAGEN DE EJECUCIÓN)
# Usamos la imagen de ASP.NET para el entorno de ejecución.
# --------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# --------------------------------------------------------
# STAGE 2: build (IMAGEN DE CONSTRUCCIÓN)
# Usamos el SDK de .NET 8.0 para compilar la aplicación.
# --------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo .csproj (ajustar nombre del archivo según corresponda)
COPY ["Zoologico.Modelos-master/Zoologico.API/Zoologico.API.csproj", "Zoologico.API/"]


RUN dotnet restore "Zoologico.API.csproj"

# Copiar el resto del código
COPY . .
RUN dotnet build "Zoologico.API.csproj" -c Release -o /app/build

# --------------------------------------------------------
# STAGE 3: publish (IMAGEN PARA PUBLICAR)
# Publicamos la aplicación para preparación del contenedor final.
# --------------------------------------------------------
FROM build AS publish
RUN dotnet publish "Zoologico.API.csproj" -c Release -o /app/publish

# --------------------------------------------------------
# STAGE 4: final (IMAGEN DE EJECUCIÓN)
# Usamos una imagen más ligera para el entorno de producción.
# --------------------------------------------------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define el punto de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Zoologico.API.dll"]

#actualizacion docker