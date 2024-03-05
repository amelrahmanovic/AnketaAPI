#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AnketaAPI/AnketaAPI.csproj", "AnketaAPI/"]
RUN dotnet restore "AnketaAPI/AnketaAPI.csproj"
COPY . .
WORKDIR "/src/AnketaAPI"
RUN dotnet build "AnketaAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnketaAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnketaAPI.dll"]