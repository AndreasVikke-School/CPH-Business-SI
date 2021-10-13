FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

RUN apt-get update && apt-get install -y dos2unix

COPY . ./

RUN dotnet restore
RUN dotnet build

RUN dos2unix ./wait-for-it.sh && apt-get --purge remove -y dos2unix && rm -rf /var/lib/apt/lists/*
RUN chmod +x ./wait-for-it.sh