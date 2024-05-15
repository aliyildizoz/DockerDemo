#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WorkerServiceConsumer/WorkerServiceConsumer.csproj", "WorkerServiceConsumer/"]
COPY ["Share/Share.csproj", "Share/"]
RUN dotnet restore "./WorkerServiceConsumer/WorkerServiceConsumer.csproj"
COPY . .
WORKDIR "/src/WorkerServiceConsumer"
RUN dotnet build "./WorkerServiceConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WorkerServiceConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerServiceConsumer.dll"]