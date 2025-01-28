# CMEProject

Projeto de uma API RESTful desenvolvida em C# com ASP.NET Core para autenticação e gerenciamento de usuários. O sistema utiliza JWT para autenticação segura e PostgreSQL para armazenar os dados.

##Funcionalidades

✅ Registro e login de usuários com criptografia de senha (BCrypt)

✅ Autenticação via JWT

✅ Gerenciamento de grupos de usuários

✅ Integração com PostgreSQL usando Entity Framework Core

✅ Docker para facilitar a execução da aplicação

✅ Atribuição do Swagger

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


## Telas do Frontend

HomePage: Tela principal onde mostrará a tabela de historico.

LoginPage: Tela onde efetuamos o Login.

MaterialsPage: Tela onde mostrará a tabela e onde efetuamos o CRUD de Materiais.

ProcessPage: Tela onde efetuaremos os processos(Receiving,Washing e Distribution).

UsersPage: Tela onde mostrará a tabela e onde efetuamos o CRUD de Usuarios.

## Contato

Para mais informações, entre em contato pelo email: adrianoeusebio2006c1@gmai.com








