FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY Services/Credit/Credit.csproj ./Services/Credit/
COPY Contracts/Contracts.csproj ./Contracts/
COPY Infrastucture/Infrastucture.csproj ./Infrastucture/

RUN dotnet restore ./Services/Credit/Credit.csproj

COPY . ./

WORKDIR /src/Services/Credit/
#Publish in /app/build
RUN dotnet publish --no-restore -c Release -o /app/build

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/build .
EXPOSE 80
ENTRYPOINT ["dotnet", "Credit.dll"]