<template>
    <div class="home-container">
        <!-- Sidebar -->
        <aside class="sidebar">
            <h2>Menu</h2>
            <button @click="handleNavigation('home')" :class="{ active: isActive('home') }">Histórico</button>
            <button @click="handleNavigation('process')" :class="{ active: isActive('process') }">Process</button>
            <button @click="handleNavigation('materials')" :class="{ active: isActive('materials') }">Materials</button>
            <button @click="handleNavigation('users')" :class="{ active: isActive('users') }">Users</button>
            <button @click="generatePDF">Generate PDF</button>
        </aside>

        <!-- Main Content -->
        <main class="content">
            <header>
                <h1 class="title">CMEBringel - Users</h1>
                <button class="account-button" @click="showUserInfo">👤 Account</button>
            </header>

            <div class="table-actions">
                <button class="add-button" @click="addUser">➕ Adicionar Usuário</button>
                <p/>
            </div>

            <section class="process-history">
                <h2>Users</h2>
                <div class="table-wrapper">
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Username</th>
                                <th>Email</th>
                                <th>Role</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="user in users" :key="user.id">
                                <td>{{ user.id }}</td>
                                <td>{{ user.username }}</td>
                                <td>{{ user.email }}</td>
                                <td>{{ user.role }}</td>
                                <td>
                                    <button class="small-button" @click="editUser(user)">✏️</button>
                                    <button class="small-button danger" @click="deleteUser(user)">🗑️</button>
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
            users: [
                { id: 1, username: "johndoe", email: "john@example.com", role: "Admin" },
                { id: 2, username: "janesmith", email: "jane@example.com", role: "Nurse" }
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
        },
        addUser() {
            console.log("Adicionar Usuário...");
        },
        editUser(user) {
            console.log(`Editando usuário: ${user.username}`);
        },
        deleteUser(user) {
            console.log(`Excluindo usuário: ${user.username}`);
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
