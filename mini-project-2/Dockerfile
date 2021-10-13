FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

COPY . ./

WORKDIR /app/MiniProject2.ServiceStudent
RUN dotnet restore

WORKDIR /app/MiniProject2.WebApi
RUN dotnet restore

WORKDIR /app/MiniProject2.EF
RUN dotnet restore

WORKDIR /app/MiniProject2.Factory
RUN dotnet restore

WORKDIR /app/MiniProject2.Models
RUN dotnet restore

WORKDIR /app
RUN dotnet build

RUN chmod +x ./wait-for-it.sh