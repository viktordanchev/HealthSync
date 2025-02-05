FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app/RestAPI
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Web/RestAPI/RestAPI.csproj", "Web/RestAPI/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "./Web/RestAPI/RestAPI.csproj"
WORKDIR "/src/Web/RestAPI"
COPY . .
RUN dotnet build "./RestAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RestAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/RestAPI
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestAPI.dll"]