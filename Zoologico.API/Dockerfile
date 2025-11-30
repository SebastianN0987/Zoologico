# --------------------------------------------------------
# STAGE 1: build (IMAGEN DE CONSTRUCCIÓN Y PUBLICACIÓN)
# Usamos el SDK de .NET 8.0 para compilar la aplicación.
# --------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Configuramos el contexto de la solución anidada (Zoologico.Modelos-master)
# 1. Copiar el archivo de solución (.sln) - CRUCIAL
COPY ["Zoologico.Modelos-master/Zoologico.Modelos.sln", "."]

# 2. Copiar los archivos de proyecto (.csproj) antes de restaurar.
#    Esto incluye el proyecto ejecutable y los proyectos de librería (Modelos, Test).
COPY ["Zoologico.Modelos-master/Zoologico.API/Zoologico.API.csproj", "Zoologico.API/"]
COPY ["Zoologico.Modelos-master/Zoologico.Modelos/Zoologico.Modelos.csproj", "Zoologico.Modelos/"]
COPY ["Zoologico.Modelos-master/Zoologico.APITest/Zoologico.APITest.csproj", "Zoologico.APITest/"]

# 3. Restaurar TODA la solución usando el archivo .sln
RUN dotnet restore "Zoologico.Modelos.sln"

# 4. Copiar el resto del código (archivos .cs, controllers, etc.)
COPY . .

# 5. Publicar la aplicación
#    Nos movemos al directorio del proyecto ejecutable para la publicación.
WORKDIR /src/Zoologico.API
RUN dotnet publish "Zoologico.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --------------------------------------------------------
# STAGE 2: final (IMAGEN DE EJECUCIÓN LIGERA)
# Usamos una imagen de ASP.NET mucho más ligera para el servidor.
# --------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080 
# Configura la variable de entorno para que ASP.NET escuche en el puerto 8080 del contenedor
ENV ASPNETCORE_URLS=http://+:8080

# Copia los archivos publicados desde la etapa de construcción
COPY --from=build /app/publish .

# Define el punto de entrada de la aplicación
ENTRYPOINT ["dotnet", "Zoologico.API.dll"] 

#cambio de rutas