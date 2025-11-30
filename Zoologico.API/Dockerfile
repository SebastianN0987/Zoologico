# --------------------------------------------------------
# STAGE 1: build (IMAGEN DE CONSTRUCCIÓN)
# Usamos el SDK de .NET 8.0 para compilar la aplicación.
# --------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# La carpeta de GitHub es Zoologico.Modelos-master/, la usamos como base para copiar.

# 1. Copiar el archivo de solución (.sln) para que 'dotnet restore' funcione en soluciones multi-proyecto.
COPY ["Zoologico.Modelos-master/Zoologico.Modelos.sln", "."]

# 2. Copiar los archivos de proyecto (.csproj) antes de restaurar.
COPY ["Zoologico.Modelos-master/Zoologico.API/Zoologico.API.csproj", "Zoologico.API/"]
COPY ["Zoologico.Modelos-master/Zoologico.Modelos/Zoologico.Modelos.csproj", "Zoologico.Modelos/"]
COPY ["Zoologico.Modelos-master/Zoologico.APITest/Zoologico.APITest.csproj", "Zoologico.APITest/"]

# 3. Restaurar TODA la solución usando el archivo .sln
RUN dotnet restore "Zoologico.Modelos.sln"

# 4. Copiar el resto del código (los archivos .cs, controllers, etc.)
COPY . .

# 5. Publicar la aplicación
#    Nos movemos al directorio del proyecto ejecutable para la publicación.
WORKDIR /src/Zoologico.Modelos-master/Zoologico.API
RUN dotnet publish "Zoologico.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --------------------------------------------------------
# STAGE 2: final (IMAGEN DE EJECUCIÓN)
# Usamos una imagen de ASP.NET mucho más ligera para el servidor.
# --------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080 
# Configura la variable de entorno para que ASP.NET escuche en el puerto 8080 del contenedor (necesario para Render)
ENV ASPNETCORE_URLS=http://+:8080

# Copia los archivos publicados desde la etapa de construcción
COPY --from=build /app/publish .

# Define el punto de entrada de la aplicación
ENTRYPOINT ["dotnet", "Zoologico.API.dll"]
