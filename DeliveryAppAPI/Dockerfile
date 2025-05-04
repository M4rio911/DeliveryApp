# # Etap budowania
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src

# # Skopiuj pliki rozwiązania i przywróć zależności
# COPY *.sln .
# COPY DeliveryAppAPI/*.csproj ./DeliveryAppAPI/
# COPY DeliveryApp.Application/*.csproj ./DeliveryApp.Application/
# COPY DeliveryApp.Infrastructure/*.csproj ./DeliveryApp.Infrastructure/
# COPY DeliveryAppAPI.Domain/*.csproj ./DeliveryAppAPI.Domain/
# COPY DeliveryAppAPI.Persistance/*.csproj ./DeliveryAppAPI.Persistance/
# RUN dotnet restore

# # Skopiuj resztę plików i zbuduj aplikację
# COPY . .
# WORKDIR /src/DeliveryAppAPI
# RUN dotnet publish -c Release -o /app/publish

# # Etap uruchomienia
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app
# COPY --from=build /app/publish .

# ENTRYPOINT ["dotnet", "DeliveryAppAPI.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./DeliveryAppAPI/publish . 
ENTRYPOINT ["dotnet", "DeliveryApp.API.dll"]