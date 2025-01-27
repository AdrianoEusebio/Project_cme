<template>
    <div class="home-container">
        <!-- Sidebar -->
        <aside class="sidebar">
            <h2>Menu</h2>
            <button @click="handleNavigation('homeadmin')" :class="{ active: isActive('homeadmin') }">Histórico</button>
            <button @click="handleNavigation('process')" :class="{ active: isActive('process') }">Process</button>
            <button @click="handleNavigation('materials')" :class="{ active: isActive('materials') }">Materials</button>
            <button @click="handleNavigation('users')" :class="{ active: isActive('users') }">Users</button>
            <button @click="generatePDF">Generate PDF</button>
        </aside>

        <!-- Main Content -->
        <main class="content">
            <header>
                <h1 class="title">CMEBringel - Materials</h1>
                <button class="account-button" @click="showUserInfo">👤 Account</button>
            </header>

            <!-- Action Buttons -->
            <div class="table-actions">
                <button class="add-button" @click="addMaterial">➕ Adicionar Material</button>
                <p/>
            </div>

            <!-- Materials Table -->
            <section class="process-history">
                <h2>Materials</h2>
                <div class="table-wrapper">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Name</th>
                                <th>Category</th>
                                <th>Quantity</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="material in materials" :key="material.id">
                                <td>{{ material.id }}</td>
                                <td>{{ material.name }}</td>
                                <td>{{ material.category }}</td>
                                <td>{{ material.quantity }}</td>
                                <td>
                                    <button class="small-button" @click="editMaterial(material)">✏️</button>
                                    <button class="small-button danger" @click="deleteMaterial(material)">🗑️</button>
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
            materials: [
                { id: 1, name: "Scalpel", category: "Surgical", quantity: 20 },
                { id: 2, name: "Gauze", category: "Bandage", quantity: 50 }
            ]
        };
    },
    methods: {
        isActive(route) {
            return this.$route.path === `/${route}`;
        },
        handleNavigation(route) {
            if (this.isActive(route)) {
                location.reload(); // Recarrega a página se já estiver nela
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
        editMaterial(material) {
            console.log(`Editando material: ${material.name}`);
        },
        deleteMaterial(material) {
            console.log(`Excluindo material: ${material.name}`);
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