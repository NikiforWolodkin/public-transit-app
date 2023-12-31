#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RoutesService/Web/Web.csproj", "Web/"]
COPY ["RoutesService/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["RoutesService/Domain.Services/Domain.Services.csproj", "Domain.Services/"]
COPY ["RoutesService/Domain/Domain.csproj", "Domain/"]
COPY ["RoutesService/Services/Services.csproj", "Services/"]
RUN dotnet restore "Web/Web.csproj"
COPY RoutesService/. .
WORKDIR "/src/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]