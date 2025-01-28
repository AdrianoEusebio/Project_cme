# CMEProject

Projeto de uma API RESTful desenvolvida em C# com ASP.NET Core para autenticação e gerenciamento de usuários. O sistema utiliza JWT para autenticação segura e PostgreSQL para armazenar os dados.

## 🏗️ Definição das Regras e Funcionalidades

✅ Registro e login de usuários com criptografia de senha (BCrypt)


✅ Autenticação via JWT


✅ Gerenciamento de grupos de usuários


✅ Integração com PostgreSQL usando Entity Framework Core


✅ Docker para facilitar a execução da aplicação

🔹 Autenticação

```bash
Método	Endpoint	Descrição
POST	/api/auth/login	Recebe Username e Password no body. Retorna 200 (success) com nome do usuário e nível de acesso, 404 se o usuário não for encontrado ou 400 se a senha estiver incorreta.
POST	/api/auth/jwt	Recebe Username e Password no body. Retorna token ASC e token refresh em caso de sucesso.
```

# 🔹 Views e Permissões

🔑 View 1 - Administração de Usuários (Apenas para Admin)

📌 CRUD completo de usuários, exceto a exclusão de usuários que já foram utilizados em processos.

📦 View 2 - Cadastro de Materiais (Apenas para Admin)

📌 CRUD completo de materiais, com as seguintes regras:

Regra: Não permitir deletar ou editar materiais que possuem registros em outras tabelas.

Serial gerado automaticamente no formato:
```bash
ID = 1 | Nome = produtoteste | Serial = PRO001
```
## OBS: O campo "status" será preenchido automaticamente com NO_PROCESS ao criar um novo material.


### Ao finalizar a lavagem:

O campo IS_WASHED será alterado para TRUE.

O status do material mudará para LAVAGEM FINALIZADA.

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

## Configuração do Swagger

Para visuzalizar o swagger, adicione a URL:
```bash
http://localhost:8000/swagger/index.html
```
# 🎨 Views do Sistema

## 🔹 Login

📌 Tela simples para autenticação do usuário.
✔ Apenas username e senha são utilizados.

## 🔹 View Principal (Main)

📌 Tela principal do sistema, contendo as opções disponíveis conforme o nível de acesso.

## 🔹 View de Processos

📌 Tela que exibe e gerencia os processos do sistema.

## 🔹 View de Materiais

📌 Exibe a tabela de materiais e suas respectivas funcionalidades.
✔ Apenas o Admin tem acesso.

## 🔹 View de Usuários

📌 Exibe a tabela de usuários e suas respectivas funcionalidades.
✔ Apenas o Admin tem acesso.

## Contato

Para mais informações, entre em contato pelo email: adrianoeusebio2006c1@gmai.com








