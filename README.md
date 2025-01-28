# CMEProject

Projeto de uma API RESTful desenvolvida em C# com ASP.NET Core para autenticação e gerenciamento de usuários. O sistema utiliza JWT para autenticação segura e PostgreSQL para armazenar os dados.

##Funcionalidades

✅ Registro e login de usuários com criptografia de senha (BCrypt)
✅ Autenticação via JWT
✅ Gerenciamento de grupos de usuários
✅ Integração com PostgreSQL usando Entity Framework Core
✅ Docker para facilitar a execução da aplicação

##Diretorio

```bash
📦 Projeto
 ┣ 📂 Controllers
 ┃ ┣ 📜 AuthController.cs    # Autenticação (login e registro)
 ┃ ┣ 📜 UserController.cs    # Gerenciamento de usuários
 ┃ 
 ┣ 📂 Models
 ┃ ┣ 📜 User.cs              # Modelo de usuário
 ┃ ┣ 📜 UserGroup.cs         # Modelo de grupo de usuários
 ┃
 ┣ 📂 Data
 ┃ ┣ 📜 MyDbContext.cs       # Configuração do banco de dados
 ┃ ┣ 📜 MyDbContextFactory.cs # Configuração para migrações
 ┃
 ┣ 📜 Dockerfile             # Configuração do Docker
 ┣ 📜 docker-compose.yml      # Configuração dos containers
 ┣ 📜 appsettings.json        # Configuração da aplicação
 ┣ 📜 README.md               # Documentação
```
##Pré-requisitos
Antes de rodar o projeto, instale:

🔹 Docker
🔹 Docker Compose

## Configuração do Ambiente

Siga os passos abaixo para configurar e utilizar o sistema.
1. Clone o repositório:
    ```bash
    https://github.com/AdrianoEusebio/Project_cme.git
    ```
2. Execute o comando abaixo para iniciar os containers Docker:
    ```bash
    docker-compose up --build
    ```
3. Depois de inicializar os conteiners, Adicione a URL abaixo para rodar o programa:
    ```bash
    http://localhost:8080/
    ```
4. Logue com a credencial abaixo:
    - **Administrador**
    - Usuário: `superadmin`
    - Senha: `admin123`

##Configuração do Docker Compose
```bash
services:
  Database:
    image: postgres:latest
    container_name: Database
    restart: always
    environment:
      POSTGRES_DB: cme_db
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: cme12345
    ports:
      - "15432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    command: >
      postgres -c listen_addresses='*' -c password_encryption=md5
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U postgres"]
      interval: 5s
      retries: 5
    networks:
      - cme_network   

  Backend:
    image: backend-cme
    container_name: Backend
    build:
      context: ./Backend
      dockerfile: Dockerfile
      target: dev
    ports:
      - "8000:8000"
    environment:
      - DOTNET_USE_POLLING_FILE_WATCHER=1
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Host=Database;Port=5432;Database=cme_db;Username=postgres;Password=cme12345
      - ASPNETCORE_URLS=http://+:8000
    depends_on:
      Database:
        condition: service_healthy
    command: dotnet watch run --no-launch-profile --no-hot-restart
    volumes:
      - ./Backend/CME_api:/app
      - ~/.nuget/packages:/root/.nuget/packages
    networks:
      - cme_network

  Frontend:
    image: frontend-cme
    container_name: Frontend
    build:
      context: ./Frontend/cme_api
      dockerfile: ../Dockerfile
    ports:
      - "8080:8080"
    depends_on:
      - Backend
    networks:
      - cme_network
    volumes:
      - ./Frontend/cme_api:/app
      - /app/node_modules
    environment:
      - CHOKIDAR_USEPOLLING=true
      - WEBSOCKET_PORT=8080
        
volumes:
  postgres_data:
networks:
  cme_network:
    driver: bridge
```
## Telas do Frontend

HomePage: Tela principal onde mostrará a tabela de historico.
LoginPage: Tela onde efetuamos o Login.
MaterialsPage: Tela onde mostrará a tabela e onde efetuamos o CRUD de Materiais.
ProcessPage: Tela onde efetuaremos os processos(Receiving,Washing e Distribution).
UsersPage: Tela onde mostrará a tabela e onde efetuamos o CRUD de Usuarios.

## Contato

Para mais informações, entre em contato pelo email: adrianoeusebio2006c1@gmai.com








