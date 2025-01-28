<template>
    <div class="home-container">
        <aside class="sidebar">
            <h2>Menu</h2>
            <button @click="handleNavigation('home')" :class="{ active: isActive('home') }">Histórico</button>
            <button @click="handleNavigation('process')" :class="{ active: isActive('process') }">Processos</button>
            <button v-if="isAdmin" @click="handleNavigation('materials')" :class="{ active: isActive('materials') }">
                Materiais</button>
            <button v-if="isAdmin" @click="handleNavigation('users')" :class="{ active: isActive('users') }">
                Usuários</button>
        </aside>

        <main class="content">
            <header>
                <h1 class="title">CMEBringel - Histórico</h1>
                <div class="header-actions">
                    <button class="account-button" @click="showUserInfo">👤 Conta</button>
                    <button class="logout-button" @click="logout">🚪 Sair</button>
                </div>
            </header>

            <section class="process-history">
                <h2>Histórico de Processos</h2>

                <div class="filter-wrapper">
                    <label for="serialFilter">Filtrar por Serial:</label>
                    <select id="serialFilter" v-model="selectedSerial">
                        <option value="">Todos</option>
                        <option v-for="serial in uniqueSerials" :key="serial" :value="serial">
                            {{ serial }}
                        </option>
                    </select>
                </div>

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
                            <tr v-for="item in filteredProcessHistory" :key="item.idProcess">
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
            processHistory: [],
            selectedSerial: "" 
        };
    },
    computed: {
        uniqueSerials() {
            return [...new Set(this.processHistory.map(item => item.serialMaterial))];
        },
        filteredProcessHistory() {
            let history = [...this.processHistory];

            history.sort((a, b) => b.idProcess - a.idProcess);

            if (this.selectedSerial) {
                history = history.filter(item => item.serialMaterial === this.selectedSerial);
            }

            return history;
        }
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
        logout() {
            localStorage.removeItem("token");
            localStorage.removeItem("role");
            this.$router.push("/login");
        },
        formatDate(date) {
            return date
                ? new Date(date).toLocaleString("pt-BR", {
                      year: "numeric",
                      month: "2-digit",
                      day: "2-digit",
                      hour: "2-digit",
                      minute: "2-digit",
                      second: "2-digit"
                  })
                : "-";
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

.filter-wrapper {
    margin-bottom: 15px;
}

.filter-wrapper label {
    font-weight: bold;
    margin-right: 10px;
}

.header-actions {
    display: flex;
    gap: 10px;
}

.logout-button {
    background-color: #e74c3c;
    color: white;
    border: none;
    padding: 8px 12px;
    cursor: pointer;
    font-weight: bold;
    border-radius: 5px;
}

.logout-button:hover {
    background-color: #c0392b;
}
</style>
