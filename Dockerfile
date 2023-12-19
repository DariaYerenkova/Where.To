
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# WORKDIR /app

# # Copy csproj and restore as distinct layers
# COPY WhereTo/*.csproj ./
# RUN dotnet restore

# # Copy the rest of the source code and build the application
# COPY . ./

# FROM build-env AS test
# RUN dotnet test --logger "html;LogFileName=/app/test_results.html"

# FROM scratch as export-test-results
# COPY --from=test /app/*.html .

# FROM build-env as publish
# RUN dotnet publish -c Debug -o out

# # Build the runtime image
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app

# # Copy the published output from the build stage
# COPY --from=publish /app/out .

# ENV ASPNETCORE_ENVIRONMENT=Development

# # Set the entry point for the container
# ENTRYPOINT ["dotnet", "WhereTo.dll"]



FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY WhereTo/*.csproj ./
RUN dotnet restore

# Copy the rest of the source code and build the application
COPY . ./

# Run tests and generate coverage report
FROM build-env AS test
WORKDIR /app 
RUN dotnet add package Microsoft.CodeCoverage && \
    dotnet tool install dotnet-reportgenerator-globaltool --tool-path /dotnetglobaltools

RUN dotnet test --collect "Code Coverage;Format=cobertura" --results-directory:"/app/coverletReports" && \
    /dotnetglobaltools/reportgenerator "-reports:/app/coverletReports/*/*.xml" "-targetdir:/out/reports" "-reporttypes:HTMLInline;HTMLChart"

FROM scratch as export-test-results
COPY --from=test /out/reports/index.html .

FROM build-env as publish
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published output from the build stage
COPY --from=publish /app/out .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "WhereTo.dll"]




