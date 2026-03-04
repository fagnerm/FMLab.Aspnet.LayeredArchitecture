# API - Layered architecture boilerplate
# Copyright (c) 2026 Fagner Marinho
# Licensed under the MIT License. See LICENSE file in the project root for details.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files first to leverage Docker layer caching for restore
COPY ["FMLab.Aspnet.LayeredArchitecture.Api/FMLab.Aspnet.LayeredArchitecture.Api.csproj", "FMLab.Aspnet.LayeredArchitecture.Api/"]
COPY ["FMLab.Aspnet.LayeredArchitecture.Business/FMLab.Aspnet.LayeredArchitecture.Business.csproj", "FMLab.Aspnet.LayeredArchitecture.Business/"]
COPY ["FMLab.Aspnet.LayeredArchitecture.Infrastructure/FMLab.Aspnet.LayeredArchitecture.Infrastructure.csproj", "FMLab.Aspnet.LayeredArchitecture.Infrastructure/"]
RUN dotnet restore "FMLab.Aspnet.LayeredArchitecture.Api/FMLab.Aspnet.LayeredArchitecture.Api.csproj"

COPY . .
WORKDIR "/src/FMLab.Aspnet.LayeredArchitecture.Api"
RUN dotnet build "FMLab.Aspnet.LayeredArchitecture.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FMLab.Aspnet.LayeredArchitecture.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FMLab.Aspnet.LayeredArchitecture.Api.dll"]
