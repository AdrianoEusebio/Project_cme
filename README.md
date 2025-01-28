Este projeto é uma API desenvolvida em C# com ASP.NET Core, que gerencia a autenticação e administração de usuários e grupos de usuários. Ele fornece funcionalidades como:

Autenticação JWT (JSON Web Token)
Registro de Usuários
Login Seguro com Hash de Senha
Gerenciamento de Grupos de Usuários
Banco de Dados PostgreSQL
A aplicação segue o padrão RESTful API, utilizando o Entity Framework Core como ORM para interagir com o banco de dados.

📂 Estrutura do Projeto
bash
Copiar
Editar
📦 Projeto
 ┣ 📂 Controllers
 ┃ ┣ 📜 AuthController.cs    # Lida com autenticação (login e registro)
 ┃ ┣ 📜 UserController.cs    # Gerencia os usuários
 ┃ 
 ┣ 📂 Models
 ┃ ┣ 📜 User.cs              # Modelo de usuário
 ┃ ┣ 📜 UserGroup.cs         # Modelo de grupo de usuários
 ┃
 ┣ 📂 Data
 ┃ ┣ 📜 MyDbContext.cs       # Configuração do banco de dados (Entity Framework)
 ┃ ┣ 📜 MyDbContextFactory.cs # Configuração para migrações
 ┃
 ┣ 📜 Dockerfile             # Configuração do Docker
 ┣ 📜 docker-compose.yml      # Configuração para subir os containers
 ┣ 📜 appsettings.json        # Configuração da aplicação (Banco de Dados, JWT, etc.)
 ┣ 📜 README.md               # Documentação
🛠 Tecnologias Utilizadas
C# com ASP.NET Core
Entity Framework Core
PostgreSQL
JWT (Json Web Token)
BCrypt para Hash de Senha
Docker e Docker Compose
🔧 Configuração do Docker
📌 1. Pré-requisitos
Antes de rodar o projeto, certifique-se de ter instalado:

Docker
Docker Compose
📌 2. Configurar o Banco de Dados no Docker
O banco de dados PostgreSQL é gerenciado dentro de um container Docker. Para iniciar o banco, siga os passos abaixo:

1️⃣ Criar um arquivo .env com as variáveis do banco de dados (Opcional)

ini
Copiar
Editar
POSTGRES_USER=admin
POSTGRES_PASSWORD=admin
POSTGRES_DB=mydatabase
2️⃣ Rodar o docker-compose para iniciar o banco de dados

bash
Copiar
Editar
docker-compose up -d
📌 Isso criará um container PostgreSQL rodando na porta 5432.

🚀 Como Rodar o Projeto
📌 1. Criar e rodar os containers da aplicação
Com os containers configurados no docker-compose.yml, rode o comando:

bash
Copiar
Editar
docker-compose up --build
Isso criará e iniciará os serviços do banco de dados e da API.

📌 2. Acessar a API
Após rodar o comando acima, a API estará disponível em:

bash
Copiar
Editar
http://localhost:5000/api/
Endpoints disponíveis:
Registro de Usuário

bash
Copiar
Editar
POST http://localhost:5000/api/auth/register
Login

bash
Copiar
Editar
POST http://localhost:5000/api/auth/login
Buscar Usuários

bash
Copiar
Editar
GET http://localhost:5000/api/user/{id}
🐳 Docker Compose (Resumo do Arquivo)
O docker-compose.yml usado no projeto:

yaml
Copiar
Editar
version: "3.8"

services:
  db:
    image: postgres
    restart: always
    environment:
      POSTGRES_USER: admin
      POSTGRES_PASSWORD: admin
      POSTGRES_DB: mydatabase
    ports:
      - "5432:5432"
    volumes:
      - pgdata:/var/lib/postgresql/data

  api:
    build: .
    ports:
      - "5000:5000"
    environment:
      - ConnectionStrings__DefaultConnection=Host=db;Database=mydatabase;Username=admin;Password=admin
    depends_on:
      - db

volumes:
  pgdata:
🛠 Executando as Migrações do Banco de Dados
Caso seja necessário rodar as migrações do Entity Framework manualmente dentro do container da API:

1️⃣ Acesse o container:

bash
Copiar
Editar
docker exec -it <nome-do-container> bash
2️⃣ Execute o comando para aplicar as migrações:

bash
Copiar
Editar
dotnet ef database update
📌 Conclusão
Agora sua API de gerenciamento de usuários e autenticação JWT está pronta para uso! 🚀

Caso tenha dúvidas, sinta-se à vontade para abrir uma issue ou contribuir para o projeto. 😊
