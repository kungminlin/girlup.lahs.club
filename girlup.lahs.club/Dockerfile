#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["girlup.lahs.club/girlup.lahs.club.csproj", "girlup.lahs.club/"]
RUN dotnet restore "girlup.lahs.club/girlup.lahs.club.csproj"
COPY . .
WORKDIR "/src/girlup.lahs.club"
RUN dotnet build "girlup.lahs.club.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "girlup.lahs.club.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "girlup.lahs.club.dll"]