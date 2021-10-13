FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

COPY . ./

RUN dotnet restore
RUN dotnet build

RUN chmod +x ./wait-for-it.sh