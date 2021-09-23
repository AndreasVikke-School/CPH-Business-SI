FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

RUN apt-get update && apt-get install -y dos2unix

COPY . ./

WORKDIR /app/MiniProject1.ClassLib
RUN ["dotnet", "restore"]
WORKDIR /app/MiniProject1.Console
RUN ["dotnet", "restore"]
WORKDIR /app/MiniProject1.Grpc
RUN ["dotnet", "restore"]
WORKDIR /app/MiniProject1.WebApi
RUN ["dotnet", "restore"]

WORKDIR /app
RUN ["dotnet", "build"]

RUN dos2unix ./wait-for-it.sh && apt-get --purge remove -y dos2unix && rm -rf /var/lib/apt/lists/*
RUN chmod +x ./wait-for-it.sh