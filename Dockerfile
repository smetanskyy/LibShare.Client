FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy everything, restore and build
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "LibShare.Client.dll"]