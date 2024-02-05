FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FinanceSystem.Api/FinanceSystem.Api.csproj", "FinanceSystem.Api/"]
COPY ["FinanceSystem.Common/FinanceSystem.Common.csproj", "FinanceSystem.Common/"]
COPY ["FinanceSystem.Abstractions/FinanceSystem.Abstractions.csproj", "FinanceSystem.Abstractions/"]
COPY ["FinanceSystem.Data/FinanceSystem.Data.csproj", "FinanceSystem.Data/"]
COPY ["FinanceSystem.Services/FinanceSystem.Services.csproj", "FinanceSystem.Services/"]
COPY ["FinanceSystem.Authorization/FinanceSystem.Authorization.csproj", "FinanceSystem.Authorization/"]
RUN dotnet restore "FinanceSystem.Api/FinanceSystem.Api.csproj"
COPY . .
WORKDIR "/src/FinanceSystem.Api"
RUN dotnet build "FinanceSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinanceSystem.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceSystem.Api.dll"]
