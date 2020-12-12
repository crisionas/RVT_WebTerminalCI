#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["RVT_WebTerminal/RVT_WebTerminal.csproj", "RVT_WebTerminal/"]
RUN dotnet restore "RVT_WebTerminal/RVT_WebTerminal.csproj"
COPY . .
WORKDIR "/src/RVT_WebTerminal"
RUN dotnet build "RVT_WebTerminal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RVT_WebTerminal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RVT_WebTerminal.dll"]