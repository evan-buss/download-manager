#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
# COPY ["src/DownloadManager/DownloadManager.csproj", "src/DownloadManager/"]
COPY . .
RUN dotnet restore "src/DownloadManager/DownloadManager.csproj"
# WORKDIR "/src/DownloadManager"
RUN dotnet build "src/DownloadManager/DownloadManager.csproj" -c Release -o /app/build

FROM build AS publish
# Install latest LTS
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs
RUN dotnet publish "src/DownloadManager/DownloadManager.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DownloadManager.dll"]