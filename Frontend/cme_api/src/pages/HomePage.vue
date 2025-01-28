<template>
    <div class="home-container">
        <aside class="sidebar">
            <h2>Menu</h2>
            <button @click="handleNavigation('home')" :class="{ active: isActive('home') }">Histórico</button>
            <button @click="handleNavigation('process')" :class="{ active: isActive('process') }">Processos</button>
            <button v-if="isAdmin" @click="handleNavigation('materials')" :class="{ active: isActive('materials') }">
                Materiais</button>
            <button v-if="isAdmin" @click="handleNavigation('users')" :class="{ active: isActive('users') }">
                Usuarios</button>
            <button @click="generatePDF">Gerar PDF</button>
        </aside>

        <main class="content">
            <header>
                <h1 class="title">CMEBringel - Histórico</h1>
                <button class="account-button" @click="showUserInfo">👤 Conta</button>
            </header>

            <section class="process-history">
                <h2>Histórico de Processos</h2>
                <div class="table-wrapper">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Material</th>
                                <th>Usuário Responsável</th>
                                <th>Status</th>
                                <th>Data</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in processHistory" :key="item.idProcess">
                                <td>{{ item.idProcess }}</td>
                                <td>{{ item.serialMaterial }}</td>
                                <td>{{ item.idUser }}</td>
                                <td class="status">{{ item.enumStatus }}</td>
                                <td>{{ formatDate(item.entryData) }}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </section>
        </main>
    </div>
</template>

<script>
import "@/assets/css/homeStyles.css";
import axios from "axios";
import authService from "@/services/authService.js";

export default {
    data() {
        return {
            isAdmin: false,
            processHistory: []
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
        generatePDF() {
            console.log("Generating PDF...");
        },
        async showUserInfo() {
            await authService.getUserCredentials();
        },
        async fetchProcessHistory() {
            try {
                const response = await axios.get("http://localhost:8000/api/processHistory");
                this.processHistory = response.data;
            } catch (error) {
                console.error("Erro ao buscar histórico de processos:", error);
            }
        },
        formatDate(date) {
            return date ? new Date(date).toLocaleDateString("pt-BR") : "-";
        },
        formatValue(value) {
            return value === 0 || value === null || value === undefined ? "-" : value;
        },
        checkAdminAccess() {
            const role = localStorage.getItem("role");
            this.isAdmin = role === "1";
        }
    },
    mounted() {
        this.checkAdminAccess();
        this.fetchProcessHistory();
        window.addEventListener("roleUpdated", this.checkAdminAccess);
    },
    beforeUnmount() {
        window.removeEventListener("roleUpdated", this.checkAdminAccess);
    }
};
</script>

<style scoped>
.sidebar button.active {
    background: #1abc9c;
    color: white;
    font-weight: bold;
}
</style>
