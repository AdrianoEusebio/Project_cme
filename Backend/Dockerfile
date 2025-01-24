# Usa a imagem do SDK do .NET 8 para desenvolvimento
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

# Copia apenas o csproj e faz restore
COPY ["CME_api/CME_api.csproj", "/app/"]
RUN dotnet restore "/app/CME_api.csproj"

# Copia todo o código do projeto
COPY . .

# Define o ambiente de desenvolvimento e ativa o Hot Reload
ENV DOTNET_USE_POLLING_FILE_WATCHER=1

# Comando para rodar a aplicação com Hot Reload
CMD ["dotnet", "watch", "run", "--project", "/app/CME_api.csproj", "--no-launch-profile", "--no-hot-restart"]

