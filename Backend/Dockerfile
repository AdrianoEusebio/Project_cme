FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

COPY ["CME_api/CME_api.csproj", "/app/"]
RUN dotnet restore "/app/CME_api.csproj"
COPY . .

ENV DOTNET_USE_POLLING_FILE_WATCHER=1
CMD ["dotnet", "watch", "run", "--project", "/app/CME_api.csproj", "--no-launch-profile", "--no-hot-restart"]

