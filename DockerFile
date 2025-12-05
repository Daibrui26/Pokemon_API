FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo .csproj
COPY *Pokemon_API.csproj ./

# Restaurar dependencias
RUN dotnet restore "Pokemon_API.csproj"

# Copiar todo el código fuente
COPY . .

# Compilar el proyecto
RUN dotnet build "Pokemon_API.csproj" -c Release -o /app/build

# ===== STAGE 2: Publish =====
FROM build AS publish
RUN dotnet publish "Pokemon_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===== STAGE 3: Runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar los archivos publicados
COPY --from=publish /app/publish .

# Exponer el puerto
EXPOSE 5000

# Variables de entorno
ENV ASPNETCORE_URLS=http://+:5000

# Punto de entrada
ENTRYPOINT ["dotnet", "Pokemon_API.dll"]