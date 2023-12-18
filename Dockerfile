
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY WhereTo/*.csproj ./
RUN dotnet restore

# Copy the rest of the source code and build the application
COPY . ./

FROM build-env AS test
RUN dotnet test --logger "html;LogFileName=/app/test_results.html"

FROM scratch as export-test-results
COPY --from=test /app/*.html .

FROM build-env as publish
RUN dotnet publish -c Debug -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published output from the build stage
COPY --from=publish /app/out .

ENV ASPNETCORE_ENVIRONMENT=Development

# Set the entry point for the container
ENTRYPOINT ["dotnet", "WhereTo.dll"]








# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# WORKDIR /app

# # Copy csproj and restore as distinct layers
# COPY WhereTo/*.csproj ./
# RUN dotnet restore

# # Copy the rest of the source code and build the application
# COPY . ./

# # Install ReportGenerator
# RUN dotnet tool install -g dotnet-reportgenerator-globaltool 
# RUN   dotnet tool install dotnet-reportgenerator-globaltool --tool-path tools  
# RUN  dotnet new tool-manifest 
# RUN   dotnet tool install dotnet-reportgenerator-globaltool

# # Run tests and generate coverage report
# FROM build-env AS test
# RUN dotnet test --logger "html;LogFileName=/app/test_results.html" \
#     && dotnet reportgenerator -reports:/app/test_results.html -targetdir:/app/TestResults

# FROM scratch as export-test-results
# COPY --from=test /app/TestResults /app/TestResults

# FROM build-env as publish
# RUN dotnet publish -c Release -o out

# # Build the runtime image
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app

# # Copy the published output from the build stage
# COPY --from=publish /app/out .

# # Set the entry point for the container
# ENTRYPOINT ["dotnet", "WhereTo.dll"]