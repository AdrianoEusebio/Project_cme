<template>
    <div class="home-container">
        <aside class="sidebar">
            <h2>Menu</h2>
            <button @click="handleNavigation('home')" :class="{ active: isActive('home') }">Histórico</button>
            <button @click="handleNavigation('process')" :class="{ active: isActive('process') }">Processos</button>
            <button class="active">Materiais</button>
            <button @click="handleNavigation('users')" :class="{ active: isActive('users') }">Usuários</button>
            <button @click="generatePDF">Gerar PDF</button>
        </aside>

        <main class="content">
            <header>
                <h1 class="title">CMEBringel - Materiais</h1>
                <button class="account-button" @click="showUserInfo">👤 Conta</button>
            </header>
            <div class="table-actions">
                <button class="add-button" @click="openAddMaterialModal">➕ Adicionar Material</button>
            </div>
            <br />
            <section class="process-history">
                <h2>Materiais</h2>
                <div class="table-wrapper">
                    <table>
                        <thead>
                            <tr>
                                <th></th>
                                <th>ID</th>
                                <th>Nome</th>
                                <th>Serie</th>
                                <th>Data de Criação</th>
                                <th>Data de Validade</th>
                                <th>Tipo</th>
                                <th>Status</th>
                                <th>Criado por</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="material in materialsData" :key="material.idMaterial">
                                <td>
                                </td>
                                <td>{{ material.idMaterial }}</td>
                                <td>{{ material.name }}</td>
                                <td>{{ material.serial }}</td>
                                <td>{{ formatDate(material.entryDate) }}</td>
                                <td>{{ formatExpirationDate(material.expirationDate) }}</td>
                                <td>{{ material.type }}</td>
                                <td>{{ material.status }}</td>
                                <td>{{ material.userName }}</td>
                                <td>
                                    <button class="delete-button"
                                        @click="deleteMaterial(material.idMaterial)">Excluir</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </section>

            <div v-if="showModal" class="modal">
                <div class="modal-content">
                    <h3>Adicionar Material</h3>
                    <label>Nome do Material:</label>
                    <br />
                    <input v-model="newMaterial.name" type="text" placeholder="Nome do Material" required />
                    <br /><br />
                    <label>Data de Validade:</label>
                    <br />
                    <input v-model="newMaterial.expirationData" type="date" required />
                    <br /><br />
                    <label>Tipo de Material:</label>
                    <br />
                    <select v-model="newMaterial.type">
                        <option disabled value="">Selecione um tipo</option>
                        <option v-for="type in materialTypes" :key="type" :value="type">{{ type }}</option>
                    </select>
                    <br /><br />
                    <div class="modal-buttons">
                        <button class="add-button" @click="addMaterial">Salvar</button>
                        <button class="cancel-button" @click="closeModal">Cancelar</button>
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
            materialsData: [],
            showModal: false,
            newMaterial: {
                name: "",
                expirationDate: "",
                type: ""
            },
            materialTypes: [
                "Equipamento de Proteção Individual (EPI)",
                "Equipamento Medico",
                "Material Cirurgico",
                "Material de Diagnostico",
                "Instrumeto Cirugico",
            ],
            users: []
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
        async fetchMaterialData() {
            try {
                const response = await axios.get("http://localhost:8000/api/material");
                this.materialsData = response.data;
            } catch (error) {
                console.error("Erro ao buscar materiais:", error);
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

        formatExpirationDate(date) {
            return date ? new Date(date).toLocaleDateString("pt-BR") : "N/A";
        },



        getUserById(id) {
            if (!this.users || this.users.length === 0) return "Carregando...";
            const user = this.users.find(user => Number(user.idUser) === Number(id));
            return user ? user.username : "Usuário Desconhecido";
        },

        openAddMaterialModal() {
            this.newMaterial = { name: "", expirationDate: "", type: "" };
            this.showModal = true;
        },

        closeModal() {
            this.showModal = false;
        },

        async addMaterial() {
            if (!this.newMaterial.name || !this.newMaterial.expirationData || !this.newMaterial.type) {
                return alert("Todos os campos são obrigatórios!");
            }
            const idUser = localStorage.getItem("id");

            if (!idUser) {
                return alert("Erro: Usuário não identificado. Faça login novamente.");
            }

            try {
                const response = await axios.post("http://localhost:8000/api/material/register", {
                    idUser,
                    expirationDate: this.newMaterial.expirationData,
                    type: this.newMaterial.type,
                    name: this.newMaterial.name
                });

                this.materialsData.push(response.data);
                alert("Material adicionado com sucesso!");
                this.closeModal();
                this.fetchMaterialData();
            } catch (error) {
                console.error("Erro ao adicionar material:", error.response?.data || error.message);
                alert("Erro ao adicionar material. Verifique o console para mais detalhes.");
            }
        },

        async deleteMaterial(id) {
            const confirmation = confirm("Tem certeza que deseja excluir este material?");
            if (!confirmation) return;

            try {
                const response = await axios.delete(`http://localhost:8000/api/Material/${id}`);
                alert(response.data.message);
                this.fetchMaterialData();
            } catch (error) {
                alert(error.response?.data.message || "Erro ao excluir material.");
            }
        }
    },
    mounted() {
        this.fetchMaterialData();
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

.save-button {
    background: #27ae60;
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 5px;
    font-weight: bold;
    cursor: pointer;
    transition: 0.3s;
    flex: 1;
    margin-right: 10px;
}

.save-button:hover {
    background: #219150;
}

.cancel-button {
    background: #e74c3c;
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 5px;
    font-weight: bold;
    cursor: pointer;
    transition: 0.3s;
    flex: 1;
    margin-left: 10px;
}

.cancel-button:hover {
    background: #c0392b;
}
</style>