🎯 Sistema de Autenticação e Gerenciamento de Usuários

🚀 Projeto de uma API RESTful desenvolvida em C# com ASP.NET Core para autenticação e gerenciamento de usuários. O sistema utiliza JWT para autenticação segura e PostgreSQL para armazenar os dados.

📜 Funcionalidades
✅ Registro e login de usuários com criptografia de senha (BCrypt)
✅ Autenticação via JWT
✅ Gerenciamento de grupos de usuários
✅ Integração com PostgreSQL usando Entity Framework Core
✅ Docker para facilitar a execução da aplicação

🛠️ Tecnologias Utilizadas
Tecnologia	Descrição
C#	Linguagem principal do projeto
ASP.NET Core	Framework para a API REST
Entity Framework	ORM para banco de dados
PostgreSQL	Banco de dados relacional
JWT	Autenticação segura
BCrypt	Hash de senha para segurança
Docker	Contêinerização da aplicação
📁 Estrutura do Projeto
bash
Copiar
Editar
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
🚀 Configuração do Docker
🔹 Pré-requisitos
Antes de rodar o projeto, instale:

🔹 Docker
🔹 Docker Compose

🔹 Configurar e rodar o Banco de Dados
Para rodar o PostgreSQL no Docker:

bash
Copiar
Editar
docker-compose up -d
Isso criará um container com PostgreSQL rodando na porta 5432.

🚀 Como Rodar a API
1️⃣ Criar e rodar os containers
bash
Copiar
Editar
docker-compose up --build
Isso criará e iniciará os serviços do banco de dados e da API.

2️⃣ Acessar a API
A API estará disponível em:

bash
Copiar
Editar
http://localhost:5000/api/
3️⃣ Endpoints Principais
Método	Endpoint	Descrição
POST	/api/auth/register	Registro de usuário
POST	/api/auth/login	Login e retorno do token JWT
GET	/api/user/{id}	Buscar informações de um usuário
🐳 Configuração do Docker Compo
