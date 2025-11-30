# --------------------------------------------------------
# STAGE 1: build (IMAGEN DE CONSTRUCCIÓN)
# Usamos el SDK de .NET 8.0 para compilar la aplicación.
# --------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

<<<<<<< HEAD:Zoologico.API/DockerFile
# La carpeta de GitHub es Zoologico.Modelos-master/, la usamos como base para copiar.

# 1. Copiar el archivo de solución (.sln) para que 'dotnet restore' funcione en soluciones multi-proyecto.
COPY ["Zoologico.Modelos-master/Zoologico.Modelos.sln", "."]

# 2. Copiar los archivos de proyecto (.csproj) antes de restaurar.
=======
# La siguiente sección es CRUCIAL para soluciones multi-proyecto.
# El contexto de construcción es la carpeta Zoologico.Modelos-master/

# 1. Copiar el archivo de solución (.sln). Este archivo está en la raíz del contexto (Zoologico.Modelos-master).
#    Asegúrate de que este nombre sea exacto: Zoologico.Modelos.sln
COPY ["Zoologico.Modelos-master/Zoologico.Modelos.sln", "."]

# 2. Copiar los archivos de proyecto (.csproj) ANTES de restaurar.
#    Las rutas son relativas a la raíz del repositorio.
>>>>>>> aedad6a1b0a3ef68f131d904516207871b8abb10:Zoologico.API/Dockerfile
COPY ["Zoologico.Modelos-master/Zoologico.API/Zoologico.API.csproj", "Zoologico.API/"]
COPY ["Zoologico.Modelos-master/Zoologico.Modelos/Zoologico.Modelos.csproj", "Zoologico.Modelos/"]
COPY ["Zoologico.Modelos-master/Zoologico.APITest/Zoologico.APITest.csproj", "Zoologico.APITest/"]

# 3. Restaurar TODA la solución usando el archivo .sln
RUN dotnet restore "Zoologico.Modelos.sln"

<<<<<<< HEAD:Zoologico.API/DockerFile
# 4. Copiar el resto del código (los archivos .cs, controllers, etc.)
=======
# 4. Copiar el resto del código (los archivos .cs, etc.)
>>>>>>> aedad6a1b0a3ef68f131d904516207871b8abb10:Zoologico.API/Dockerfile
COPY . .

# 5. Publicar la aplicación
#    Nos movemos al directorio del proyecto ejecutable para la publicación.
WORKDIR /src/Zoologico.Models-master/Zoologico.API
RUN dotnet publish "Zoologico.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --------------------------------------------------------
<<<<<<< HEAD:Zoologico.API/DockerFile
# STAGE 2: final (IMAGEN DE EJECUCIÓN LIGERA)
=======
# STAGE 2: final (IMAGEN DE EJECUCIÓN)
>>>>>>> aedad6a1b0a3ef68f131d904516207871b8abb10:Zoologico.API/Dockerfile
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
<<<<<<< HEAD:Zoologico.API/DockerFile
ENTRYPOINT ["dotnet", "Zoologico.API.dll"]
=======
ENTRYPOINT ["dotnet", "Zoologico.API.dll"]
>>>>>>> aedad6a1b0a3ef68f131d904516207871b8abb10:Zoologico.API/Dockerfile
