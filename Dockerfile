# ─ Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# ─ Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos solo el csproj y restauramos dependencias
COPY ["PrestamoApp.Api/PrestamoApp.Api.csproj", "PrestamoApp.Api/"]
RUN dotnet restore "PrestamoApp.Api/PrestamoApp.Api.csproj"

# Ahora copiamos todo el repo, para incluir el resto de proyectos compartidos (Core, Models, etc.)
COPY . .

# Nos movemos al folder de la API y construimos en Release
WORKDIR "/src/PrestamoApp.Api"
RUN dotnet publish "PrestamoApp.Api.csproj" -c Release -o /app/publish

# ─ Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PrestamoApp.Api.dll"]


