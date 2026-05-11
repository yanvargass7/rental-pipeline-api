FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["RentalPipeline.API/RentalPipeline.API.csproj", "RentalPipeline.API/"]
COPY ["RentalPipeline.Application/RentalPipeline.Application.csproj", "RentalPipeline.Application/"]
COPY ["RentalPipeline.Domain/RentalPipeline.Domain.csproj", "RentalPipeline.Domain/"]
COPY ["RentalPipeline.Infrastructure/RentalPipeline.Infrastructure.csproj", "RentalPipeline.Infrastructure/"]

RUN dotnet restore "RentalPipeline.API/RentalPipeline.API.csproj"

COPY . .

WORKDIR "/src/RentalPipeline.API"

RUN dotnet publish "RentalPipeline.API.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "RentalPipeline.API.dll"]