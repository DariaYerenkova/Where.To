##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER app
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["WhereTo/WhereTo.csproj", "WhereTo/"]
#RUN dotnet restore "./WhereTo/./WhereTo.csproj"
#COPY . .
#WORKDIR "/src/WhereTo"
#RUN dotnet build "./WhereTo.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./WhereTo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "WhereTo.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY WhereTo/*.csproj ./
RUN dotnet restore

# Clear NuGet cache
RUN dotnet nuget locals all --clear

# Copy the rest of the source code and build the application
COPY . ./

# Run xUnit tests
RUN dotnet test --logger "trx;LogFileName=/app/test_results.trx"

RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build-env /app/out .
COPY --from=build-env /app/test_results.trx /app

# Set the entry point for the container
ENTRYPOINT ["dotnet", "WhereTo.dll"]