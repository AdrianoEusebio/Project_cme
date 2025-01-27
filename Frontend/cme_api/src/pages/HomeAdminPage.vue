<template>
    <div class="home-container">
        <!-- Sidebar -->
        <aside class="sidebar">
            <h2>Menu</h2>
            <button @click="handleNavigation('homeadmin')" :class="{ active: isActive('homeadmin') }">🏠 Histórico</button>
            <button @click="handleNavigation('process')" :class="{ active: isActive('process') }">📋 Process</button>
            <button @click="handleNavigation('materials')" :class="{ active: isActive('materials') }">📦 Materials</button>
            <button @click="handleNavigation('users')" :class="{ active: isActive('users') }">👥 Users</button>
            <button @click="generatePDF">📄 Generate PDF</button>
        </aside>

        <!-- Main Content -->
        <main class="content">
            <header>
                <h1 class="title">CMEBringel - Admin</h1>
                <button class="account-button">👤 Account</button>
            </header>

            <!-- Process History Table -->
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
                            <tr v-for="item in processHistory" :key="item.id">
                                <td>{{ item.id }}</td>
                                <td>{{ item.material }}</td>
                                <td>{{ item.user }}</td>
                                <td class="status">{{ item.status }}</td>
                                <td>{{ item.date }}</td>
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

export default {
    data() {
        return {
            processHistory: [
                { id: 1, material: "Syringe", user: "Emily White", status: "Distribuído", date: "2025-01-18" },
                { id: 2, material: "Bandage", user: "Tom Blue", status: "Em Processo", date: "2025-01-19" }
            ]
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
        }
    }
};
</script>

<style scoped>
.sidebar button.active {
    background: #1abc9c;
    color: white;
    font-weight: bold;
}

/* Estilização da Tabela */
.table-wrapper {
    overflow-x: auto;
}

table {
    width: 100%;
    border-collapse: collapse;
    border-radius: 8px;
    overflow: hidden;
}

th {
    background: #2c3e50;
    color: white;
    padding: 12px;
    text-align: left;
}

td {
    padding: 12px;
    border-bottom: 1px solid #ddd;
}

tbody tr:nth-child(even) {
    background: #f9f9f9;
}

tbody tr:hover {
    background: #ecf0f1;
}

.status {
    font-weight: bold;
    color: #27ae60;
}
</style>
