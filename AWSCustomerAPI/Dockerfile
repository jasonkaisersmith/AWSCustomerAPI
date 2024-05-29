#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AWSCustomerAPI/AWSCustomerAPI.csproj", "AWSCustomerAPI/"]
RUN dotnet restore "./AWSCustomerAPI/./AWSCustomerAPI.csproj"
COPY . .
WORKDIR "/src/AWSCustomerAPI"
RUN dotnet build "./AWSCustomerAPI.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AWSCustomerAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AWSCustomerAPI.dll"]