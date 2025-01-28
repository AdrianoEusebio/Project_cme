<template>
    <section class="process-table">
        <h2>Materiais a ser lavados</h2>

        <div class="table-actions">
            <button class="add-button" @click="openWashingModal">Iniciar uma Lavagem</button>
            <div class="spacer"></div>
            <button class="delete-button" @click="openFinishWashingModal">Finalizar uma Lavagem</button>
        </div>
        <br />
        <div class="table-wrapper">
            <table>
                <thead>
                    <tr>
                        <th>Material</th>
                        <th>Serial</th>
                        <th>Usuário Responsável</th>
                        <th>Status</th>
                        <th>Data de início</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in washingData" :key="item.idMaterial">
                        <td>{{ item.name }}</td>
                        <td>{{ item.serial }}</td>
                        <td>{{ getUserById(item.idUser) }}</td>
                        <td class="status">{{ item.status }}</td>
                        <td>{{ formatDate(item.date) }}</td>
                    </tr>
                </tbody>
            </table>
        </div>

        <br />

        <h2>Lavagens</h2>

        <div class="table-wrapper">
            <table>
                <thead>
                    <tr>
                        <th>Material Serial</th>
                        <th>Usuário Responsável</th>
                        <th>Já foi lavado</th>
                        <th>Data de entrada</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in finishWashingData" :key="item.idWashing">
                        <td>{{ item.serialMaterial }}</td>
                        <td>{{ getUserById(item.idUser) }}</td>
                        <td>{{ item.isWashed ? 'Sim' : 'Não' }}</td>
                        <td>{{ formatDate(item.entryDate) }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div v-if="showWashingModal" class="modal">
            <div class="modal-content">
                <h3>Iniciar Lavagem</h3>
                <label>Selecione o Material:</label>
                <br />
                <select v-model="newWashing.serialMaterial">
                    <option disabled value="">Selecione um material</option>
                    <option v-for="item in washingData" :key="item.serial" :value="item.serial">{{ item.serial }} - {{
                        item.name }}</option>
                </select>
                <br /><br />
                <div class="modal-buttons">
                    <button class="add-button" @click="addWashing">Salvar</button>
                    <button class="cancel-button" @click="closeWashingModal">Cancelar</button>
                </div>
            </div>
        </div>
        <div v-if="showFinishModal" class="modal">
            <div class="modal-content">
                <h3>Finalizar Lavagem</h3>
                <label>Selecione o Material:</label>
                <br />
                <select v-model="finishWashing.serialMaterial">
                    <option disabled value="">Selecione um material</option>
                    <option v-for="item in filteredFinishWashingData" :key="item.serialMaterial"
                        :value="item.serialMaterial">
                        {{ item.serialMaterial }}
                    </option>
                </select>
                <br /><br />
                <div class="modal-buttons">
                    <button class="add-button" @click="finishWashingProcess">Finalizar</button>
                    <button class="cancel-button" @click="closeFinishWashingModal">Cancelar</button>
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
            washingData: [],
            finishWashingData: [],
            showWashingModal: false,
            showFinishModal: false,
            newWashing: {
                serialMaterial: ""
            },
            finishWashing: {
                serialMaterial: ""
            },
            users: []
        };
    },
    computed: {
        filteredFinishWashingData() {
            return this.finishWashingData.filter(item => !item.isWashed);
        }
    },
    methods: {
        async fetchWashingData() {
            try {
                const response = await axios.get("http://localhost:8000/api/Washing/received");
                this.washingData = response.data;
            } catch (error) {
                console.error("Erro ao buscar dados de Washing:", error);
            }
        },
        async fetchFinishWashingData() {
            try {
                const response = await axios.get("http://localhost:8000/api/Washing/all");
                this.finishWashingData = response.data;
            } catch (error) {
                console.error("Erro ao buscar dados de Washing:", error);
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
        getUserById(id) {
            if (!this.users || this.users.length === 0) return "Carregando...";
            const user = this.users.find(user => Number(user.idUser) === Number(id));
            return user ? user.username : "Usuário Desconhecido";
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

        openWashingModal() {
            this.newWashing = { serialMaterial: "" };
            this.showWashingModal = true;
        },
        closeWashingModal() {
            this.showWashingModal = false;
        },
        async addWashing() {
            if (!this.newWashing.serialMaterial) {
                return alert("O Serial do Material é obrigatório!");
            }

            const idUser = localStorage.getItem("id");

            if (!idUser) {
                return alert("Erro: Usuário não identificado. Faça login novamente.");
            }

            try {
                await axios.post("http://localhost:8000/api/washing/start", {
                    serialMaterial: this.newWashing.serialMaterial,
                    idUser: Number(idUser)
                });
                alert("Lavagem iniciada com sucesso!");
                this.fetchWashingData();
                this.closeWashingModal();
            } catch (error) {
                console.error("Erro ao iniciar lavagem:", error.response?.data || error.message);
                alert("Erro ao iniciar lavagem. Verifique o console para mais detalhes.");
            }
        },
        openFinishWashingModal() {
            this.finishWashing = { serialMaterial: "" };
            this.showFinishModal = true;
        },
        closeFinishWashingModal() {
            this.showFinishModal = false;
        },
        async finishWashingProcess() {
            if (!this.finishWashing.serialMaterial) {
                return alert("Selecione um material para finalizar a lavagem!");
            }
            const idUser = localStorage.getItem("id");
            try {
                await axios.post("http://localhost:8000/api/washing/finish", {
                    serialMaterial: this.finishWashing.serialMaterial,
                    idUser: Number(idUser)
                });

                alert("Lavagem finalizada com sucesso!");
                this.fetchWashingData();
                this.fetchFinishWashingData();
                this.closeFinishWashingModal();
            } catch (error) {
                console.error("Erro ao finalizar lavagem:", error.response?.data || error.message);
                alert("Erro ao finalizar lavagem. Verifique o console para mais detalhes.");
            }
        }
    },
    mounted() {
        this.fetchWashingData();
        this.fetchFinishWashingData();
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
</style>
