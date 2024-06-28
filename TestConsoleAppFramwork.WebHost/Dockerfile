# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copy source and dependencies.
COPY TestConsoleAppFramwork.WebHost/ TestConsoleAppFramwork.WebHost/

# Publish the application.
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish TestConsoleAppFramwork.WebHost/TestConsoleAppFramwork.WebHost.csproj --use-current-runtime -o /app /p:UseAppHost=false

# Create a new stage for running the application.
FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine
WORKDIR /app

RUN mkdir -p /Logs
RUN chown $APP_UID /Logs

# Copy everything needed to run the app from the "build" stage.
COPY --from=build /app .

USER $APP_UID

ENTRYPOINT ["dotnet", "TestConsoleAppFramwork.WebHost.dll"]
