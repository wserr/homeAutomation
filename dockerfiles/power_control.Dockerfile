FROM mcr.microsoft.com/dotnet/aspnet:10.0.1-noble-arm64v8 AS base
USER $APP_UID

EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0.101-noble-arm64v8 AS build
COPY ./microservices/power_control/*.csproj /microservices/power_control/

WORKDIR /microservices/power_control
RUN dotnet restore

WORKDIR /
COPY ./microservices/power_control /microservices/power_control

WORKDIR /microservices/power_control
# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION
RUN dotnet publish "./power_control.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "power_control.dll"]
