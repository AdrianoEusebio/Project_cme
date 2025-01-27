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
                <h1 class="title">CMEBringel - Process</h1>
                <button class="account-button" @click="showUserInfo">👤 Account</button>
            </header>

            <!-- Process Table Switch Buttons -->
            <div class="process-buttons">
                <button :class="{ active: selectedTable === 'receiving' }" @click="selectedTable = 'receiving'">
                    📥 Receiving
                </button>
                <button :class="{ active: selectedTable === 'washing' }" @click="selectedTable = 'washing'">
                    🧼 Washing
                </button>
                <button :class="{ active: selectedTable === 'distribution' }" @click="selectedTable = 'distribution'">
                    📦 Distribution
                </button>
            </div>

            <section class="process-history">
                <h2>{{ tableTitle }}</h2>

                <div class="table-actions">
                    <button class="add-button" @click="addMaterial">➕ Adicionar Material</button>
                    <p/>
                </div>

                <div class="table-wrapper">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Material</th>
                                <th>Responsible User</th>
                                <th>Status</th>
                                <th>Date</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in tableData" :key="item.id">
                                <td>{{ item.id }}</td>
                                <td>{{ item.material }}</td>
                                <td>{{ item.user }}</td>
                                <td class="status">{{ item.status }}</td>
                                <td>{{ item.date }}</td>
                                <td>
                                    <button class="small-button danger" @click="deleteMaterial(item)">🗑️</button>
                                </td>
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
import authService from "@/services/authService.js";

export default {
    data() {
        return {
            selectedTable: "receiving",
            tables: {
                receiving: {
                    title: "Receiving",
                    data: [
                        { id: 1, material: "Scalpel", user: "John Doe", status: "Pending", date: "2025-01-15" },
                        { id: 2, material: "Gauze", user: "Jane Smith", status: "Completed", date: "2025-01-14" }
                    ]
                },
                washing: {
                    title: "Washing",
                    data: [
                        { id: 3, material: "Forceps", user: "Alice Brown", status: "In Progress", date: "2025-01-16" },
                        { id: 4, material: "Gloves", user: "Bob Green", status: "Pending", date: "2025-01-17" }
                    ]
                },
                distribution: {
                    title: "Distribution",
                    data: [
                        { id: 5, material: "Syringe", user: "Emily White", status: "Shipped", date: "2025-01-18" },
                        { id: 6, material: "Bandage", user: "Tom Blue", status: "Delivered", date: "2025-01-19" }
                    ]
                }
            }
        };
    },
    computed: {
        tableTitle() {
            return this.tables[this.selectedTable].title;
        },
        tableData() {
            return this.tables[this.selectedTable].data;
        }
    },
    methods: {
        isActive(route) {
            return this.$route.path === `/${route}`;
        },
        handleNavigation(route) {
            if (this.isActive(route)) {
                location.reload(); // Recarrega a página se o usuário clicar onde já está
            } else {
                this.$router.push(`/${route}`);
            }
        },
        generatePDF() {
            console.log("Generating PDF...");
        },
        addMaterial() {
            console.log("Adicionar Material...");
        },
        deleteMaterial(item) {
            console.log(`Excluindo material: ${item.material}`);
        },
        async showUserInfo() {
            await authService.getUserCredentials();
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
</style>
