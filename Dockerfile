FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FinanceSystem.Api/FinanceSystem.Api.csproj", "FinanceSystem.Api/"]
COPY ["FinanceSystem.Authorization/FinanceSystem.Authorization.csproj", "FinanceSystem.Authorization/"]
COPY ["FinanceSystem.Abstractions/FinanceSystem.Abstractions.csproj", "FinanceSystem.Abstractions/"]
COPY ["FinanceSystem.Common/FinanceSystem.Common.csproj", "FinanceSystem.Common/"]
COPY ["FinanceSystem.Data/FinanceSystem.Data.csproj", "FinanceSystem.Data/"]
COPY ["FinanceSystem.Services/FinanceSystem.Services.csproj", "FinanceSystem.Services/"]
RUN dotnet restore "FinanceSystem.Api/FinanceSystem.Api.csproj"
COPY . .
WORKDIR "/src/FinanceSystem.Api"
RUN dotnet build "FinanceSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FinanceSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceSystem.Api.dll"]
