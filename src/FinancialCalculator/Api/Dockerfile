FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY Api/Api.csproj ./Api/
COPY Contracts/Contracts.csproj ./Contracts/
COPY Infrastucture/Infrastucture.csproj ./Infrastucture/

RUN dotnet restore ./Api/Api.csproj

COPY . ./

WORKDIR /src/Api/
#Publish in /app/build
RUN dotnet publish --no-restore -c Release -o /app/build

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/build .
EXPOSE 80
ENTRYPOINT ["dotnet", "Api.dll"]