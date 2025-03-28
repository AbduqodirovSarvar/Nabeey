# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files separately
COPY src/Nabeey.WebApi/Nabeey.WebApi.csproj Nabeey.WebApi/
COPY src/Nabeey.Domain/Nabeey.Domain.csproj Nabeey.Domain/
COPY src/Nabeey.DataAccess/Nabeey.DataAccess.csproj Nabeey.DataAccess/
COPY src/Nabeey.Service/Nabeey.Service.csproj Nabeey.Service/

# Restore dependencies
WORKDIR /src/Nabeey.WebApi
RUN dotnet restore

# Copy all source files
COPY . .

# Ensure the correct directory before building
WORKDIR ./src/Nabeey.WebApi
RUN dotnet build "Nabeey.WebApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Nabeey.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nabeey.WebApi.dll"]
