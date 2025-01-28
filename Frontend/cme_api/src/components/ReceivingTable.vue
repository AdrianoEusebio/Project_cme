<template>
    <section class="process-table">
        <h2>Receiving</h2>
        <div class="table-actions">
            <button class="add-button" @click="openReceivingModal">➕ Iniciar um processo</button>
        </div>
        <br />
        <div class="table-wrapper">
            <table>
                <thead>
                    <tr>
                        <th>Material</th>
                        <th>Serial</th>
                        <th>Status</th>
                        <th>Data de Criação</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in receivingData" :key="item.idMaterial">
                        <td>{{ item.name }}</td>
                        <td>{{ item.serial }}</td>
                        <td>{{ item.status }}</td>
                        <td>{{ formatDate(item.entryDate) }}</td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div v-if="showModal" class="modal">
            <div class="modal-content">
                <h3>Iniciar Recebimento</h3>
                <label>Selecione o Serial do Material:</label>
                <br />
                <select v-model="newReceiving.serialMaterial">
                    <option disabled value="">Escolha um serial</option>
                    <option v-for="item in receivingData" :key="item.serial" :value="item.serial">
                        {{ item.serial }} - {{ item.name }}
                    </option>
                </select>
                <br /><br />
                <div class="modal-buttons">
                    <button class="add-button" @click="addReceiving">Salvar</button>
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
            receivingData: [],
            showModal: false,
            newReceiving: {
                serialMaterial: ""
            }
        };
    },
    methods: {
        async fetchReceivingData() {
            try {
                const response = await axios.get("http://localhost:8000/api/receiving");
                this.receivingData = response.data;
            } catch (error) {
                console.error("Erro ao buscar dados de Receiving:", error);
            }
        },

        openReceivingModal() {
            this.newReceiving = { serialMaterial: "" };
            this.showModal = true;
        },

        closeModal() {
            this.showModal = false;
        },

        async addReceiving() {
            if (!this.newReceiving.serialMaterial) {
                return alert("Selecione um serial antes de continuar!");
            }

            const idUser = localStorage.getItem("id");

            if (!idUser) {
                return alert("Erro: Usuário não identificado. Faça login novamente.");
            }

            try {
                await axios.post("http://localhost:8000/api/receiving/register", {
                    serialMaterial: this.newReceiving.serialMaterial,
                    idUser: Number(idUser)
                });

                alert("Material recebido com sucesso!");
                this.fetchReceivingData();
                this.closeModal();
            } catch (error) {
                console.error("Erro ao iniciar recebimento:", error.response?.data || error.message);
                alert("Erro ao iniciar recebimento. Verifique o console para mais detalhes.");
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

    },
    mounted() {
        this.fetchReceivingData();
    }
};
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
</style>
