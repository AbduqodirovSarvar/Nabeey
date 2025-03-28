# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0-jammy AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0-jammy AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR ../
COPY ["./Nabeey.WebApi/Nabeey.WebApi.csproj", "src/Nabeey.WebApi/"]
COPY ["./Nabeey.DataAccess/Nabeey.DataAccess.csproj", "src/Nabeey.DataAccess/"]
COPY ["./Nabeey.Domain/Nabeey.Domain.csproj", "src/Nabeey.Domain/"]
COPY ["./Nabeey.Service/Nabeey.Service.csproj", "src/Nabeey.Service/"]
RUN dotnet restore "./Nabeey.WebApi/Nabeey.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Nabeey.WebApi"
RUN dotnet build "./Nabeey.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Nabeey.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nabeey.WebApi.dll"]