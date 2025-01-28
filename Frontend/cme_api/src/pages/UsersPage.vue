<template>
    <div class="home-container">
        <aside class="sidebar">
            <h2>Menu</h2>
            <button @click="handleNavigation('home')" :class="{ active: isActive('home') }">Histórico</button>
            <button @click="handleNavigation('process')" :class="{ active: isActive('process') }">Processos</button>
            <button @click="handleNavigation('materials')" :class="{ active: isActive('materials') }">Materiais</button>
            <button class="active">Usuários</button>
            <button @click="generatePDF">Gerar PDF</button>
        </aside>
        <main class="content">
            <header>
                <h1 class="title">CMEBringel - Usuários</h1>
                <button class="account-button" @click="showUserInfo">👤 Conta</button>
            </header>

            <div class="table-actions">
                <button class="add-button" @click="openAddUserModal">➕ Adicionar Usuário</button>
            </div>
            <br />
            <section class="process-history">
                <h2>Usuários</h2>
                <div class="table-wrapper">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Nome</th>
                                <th>Email</th>
                                <th>Data de Criação</th>
                                <th>Nível de Acesso</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in usersData" :key="item.idUser">
                                <td>{{ item.idUser }}</td>
                                <td>{{ item.username }}</td>
                                <td>{{ item.email }}</td>
                                <td>{{ formatDate(item.criadoEm) }}</td>
                                <td>{{ item.idGroup === 1 ? "Administrador" : "Enfermeiro" }}</td>
                                <td>
                                    <button class="delete-button" @click="deleteUser(item.idUser)">Excluir</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </section>

            <div v-if="showModal" class="modal">
                <div class="modal-content">
                    <h3>Adicionar Usuário</h3>

                    <div class="form-group">
                        <label>Nome do Usuário:</label>
                        <input v-model="newUser.username" type="text" placeholder="Digite o nome do usuário" required />
                    </div>

                    <div class="form-group">
                        <label>Email:</label>
                        <input v-model="newUser.email" type="email" placeholder="Digite o email" required />
                    </div>

                    <div class="form-group">
                        <label>Senha:</label>
                        <input v-model="newUser.password" type="password" placeholder="Digite a senha" required />
                    </div>

                    <div class="form-group">
                        <label>Nível de Acesso:</label>
                        <select v-model="newUser.idGroup">
                            <option disabled value="">Selecione um nível</option>
                            <option value="1">Administrador</option>
                            <option value="2">Usuário Padrão</option>
                        </select>
                    </div>

                    <div class="modal-buttons">
                        <button class="save-button" @click="addUser">💾 Salvar</button>
                        <button class="cancel-button" @click="closeModal">❌ Cancelar</button>
                    </div>
                </div>
            </div>
        </main>
    </div>
</template>

<script>
import "@/assets/css/homeStyles.css";
import authService from "@/services/authService.js";
import axios from "axios";

export default {
    data() {
        return {
            usersData: [],
            showModal: false,
            newUser: {
                username: "",
                email: "",
                password: "",
                idGroup: ""
            }
        };
    },
    methods: {
        isActive(route) {
            return this.$route.path === `/${route}`;
        },
        handleNavigation(route) {
            if (this.isActive(route)) {
                location.reload();
            } else {
                this.$router.push(`/${route}`);
            }
        },
        async fetchUserData() {
            try {
                const response = await axios.get("http://localhost:8000/api/user");
                this.usersData = response.data;
                console.log("Usuários carregados:", this.users); 
            } catch (error) {
                console.error("Erro ao buscar usuários:", error);
            }
        },
        async showUserInfo() {
            await authService.getUserCredentials();
        },
        formatDate(date) {
            if (!date) return "N/A";

            const formattedDate = new Date(date).toLocaleString("pt-BR", {
                year: "numeric",
                month: "2-digit",
                day: "2-digit",
                hour: "2-digit",
                minute: "2-digit",
                second: "2-digit",
                timeZone: "UTC"
            });

            return formattedDate;
        },
        openAddUserModal() {
            this.newUser = { username: "", email: "", password: "", idGroup: "" };
            this.showModal = true;
        },
        closeModal() {
            this.showModal = false;
        },
        async addUser() {
            if (!this.newUser.username || !this.newUser.email || !this.newUser.password || !this.newUser.idGroup) {
                return alert("Todos os campos são obrigatórios!");
            }

            try {
                const response = await axios.post("http://localhost:8000/api/user/register", {
                    username: this.newUser.username,
                    email: this.newUser.email,
                    hashPassword: this.newUser.password,
                    idGroup: this.newUser.idGroup
                });

                this.usersData.push(response.data);
                alert("Usuário adicionado com sucesso!");
                this.closeModal();
                location.reload();
            } catch (error) {
                console.error("Erro ao adicionar usuário:", error.response?.data || error.message);
                alert("Erro ao adicionar usuário.");
            }
        },
        async deleteUser(id) {
            const confirmDelete = confirm("Tem certeza que deseja excluir este usuário?");
            if (!confirmDelete) return;

            try {
                await axios.delete(`http://localhost:8000/api/user/${id}`);
                alert("Usuário excluído com sucesso!");
                this.fetchUserData();
            } catch (error) {
                console.error("Erro ao excluir usuário:", error.response?.data || error.message);
                alert(error.response?.data?.message || "Erro ao excluir usuário.");
            }
        }
    },
    mounted() {
        this.fetchUserData();
    }
};
</script>

<style scoped>
.sidebar button.active {
    background: #1abc9c;
    color: white;
    font-weight: bold;
}

.modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
}

.modal-content {
    background: white;
    padding: 20px;
    border-radius: 8px;
    width: 400px;
    text-align: center;
    box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.2);
}

.form-group {
    display: flex;
    flex-direction: column;
    align-items: center;
    text-align: left;
    width: 100%;
    margin-bottom: 12px;
}

label {
    width: 100%;
    font-weight: bold;
    margin-bottom: 5px;
    color: #333;
}

input,
select {
    width: 90%;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    font-size: 14px;
    transition: 0.3s;
    text-align: left;
}

.modal-buttons {
    display: flex;
    justify-content: space-between;
    width: 90%;
    margin: auto;
}
</style>
