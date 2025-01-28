<template>
    <section class="process-table">
        <h2>Distribution</h2>
        <div class="table-actions">
            <button class="add-button" @click="openAddDistributionModal">➕ Adicionar Material</button>
            <br />
        </div>
        <br />
        <div class="table-wrapper">
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Material</th>
                        <th>Setor</th>
                        <th>Usuário Responsável</th>
                        <th>Data</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in distributionData" :key="item.idDistribution">
                        <td>{{ item.idDistribution }}</td>
                        <td>{{ item.serialMaterial }}</td>
                        <td>{{ item.sector }}</td>
                        <td>{{ getUserById(item.idUser) }}</td>
                        <td>{{ formatDate(item.entryDate) }}</td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div v-if="showModal" class="modal">
            <div class="modal-content">
                <h3>Iniciar Distribuição</h3>
                <label>Escolha o Material:</label>
                <br />
                <select v-model="newDistribution.serialMaterial" required>
                    <option disabled value="">Selecione um Material</option>
                    <option v-for="material in availableMaterials" :key="material.serial" :value="material.serial">
                        {{ material.name }} - {{ material.serial }}
                    </option>
                </select>
                <br /><br />

                <label>Escolha o Setor:</label>
                <br />
                <select v-model="newDistribution.sector" required>
                    <option disabled value="">Selecione um Setor</option>
                    <option v-for="sector in sectors" :key="sector" :value="sector">{{ sector }}</option>
                </select>
                <br /><br />

                <div class="modal-buttons">
                    <button class="add-button" @click="addDistribution">Salvar</button>
                    <button class="cancel-button" @click="closeModal">Cancelar</button>
                </div>
            </div>
        </div>
    </section>
</template>

<script>
import axios from "axios";
import "@/assets/css/homeStyles.css";

export default {
    data() {
        return {
            distributionData: [],
            availableMaterials: [],
            users: [],
            showModal: false,
            newDistribution: {
                serialMaterial: "",
                sector: ""
            },
            sectors: [
                "Centro Cirúrgico",
                "UTI",
                "Pediatria",
                "Emergência",
                "Enfermaria Geral"
            ]
        };
    },
    methods: {
        async fetchDistributionData() {
            try {
                const response = await axios.get("http://localhost:8000/api/distribution");
                this.distributionData = response.data;
            } catch (error) {
                console.error("Erro ao buscar dados de Distribution:", error);
            }
        },
        async fetchAvailableMaterials() {
            try {
                const response = await axios.get("http://localhost:8000/api/material");
                this.availableMaterials = response.data.filter(
                    (material) => material.status === "LAVAGEM_FINALIZADA"
                );
            } catch (error) {
                console.error("Erro ao buscar materiais disponíveis:", error);
            }
        },
        async fetchUsers() {
            try {
                const response = await axios.get("http://localhost:8000/api/user");
                this.users = response.data;
            } catch (error) {
                console.error("Erro ao buscar usuários:", error);
            }
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

        getUserById(id) {
            if (!this.users || this.users.length === 0) return "Carregando...";
            const user = this.users.find((user) => Number(user.idUser) === Number(id));
            return user ? user.username : "Usuário Desconhecido";
        },
        openAddDistributionModal() {
            this.newDistribution = { serialMaterial: "", sector: "" };
            this.showModal = true;
            this.fetchAvailableMaterials();
        },
        closeModal() {
            this.showModal = false;
        },
        async addDistribution() {
            if (!this.newDistribution.serialMaterial || !this.newDistribution.sector) {
                return alert("Todos os campos são obrigatórios!");
            }

            const idUser = localStorage.getItem("id");
            if (!idUser) {
                return alert("Erro: Usuário não identificado. Faça login novamente.");
            }

            try {
                const response = await axios.post("http://localhost:8000/api/distribution", {
                    serialMaterial: this.newDistribution.serialMaterial,
                    sector: this.newDistribution.sector,
                    idUser: Number(idUser)
                });
                console.log("Enviando para API:", {
                    serialMaterial: this.newDistribution.serialMaterial,
                    sector: this.newDistribution.sector,
                    idUser: Number(idUser)
                });
                this.distributionData.push(response.data);
                alert("Distribuição registrada com sucesso!");
                this.fetchDistributionData();
                this.closeModal();

            } catch (error) {
                console.error("Erro ao adicionar distribuição:", error.response?.data || error.message);
                alert(`Erro ao adicionar distribuição: ${JSON.stringify(error.response?.data) || error.message}`);
            }
        }
    },
    mounted() {
        this.fetchDistributionData();
        this.fetchUsers();
    }
}
</script>

<style scoped>
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

.modal-buttons {
    display: flex;
    justify-content: space-between;
    width: 90%;
    margin: auto;
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
</style>
