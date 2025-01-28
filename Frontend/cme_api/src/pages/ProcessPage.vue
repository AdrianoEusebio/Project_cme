<template>
    <div class="home-container">
        <aside class="sidebar">
            <h2>Menu</h2>
            <button @click="handleNavigation('home')" :class="{ active: isActive('home') }">Histórico</button>
            <button @click="handleNavigation('process')" :class="{ active: isActive('process') }">Processos</button>
            <button v-if="isAdmin" @click="handleNavigation('materials')" :class="{ active: isActive('materials') }">
                Materiais</button>
            <button v-if="isAdmin" @click="handleNavigation('users')" :class="{ active: isActive('users') }">
                Usuárioss</button>
        </aside>

        <main class="content">
            <header>
                <h1 class="title">CMEBringel - Processos</h1>
                <button class="account-button" @click="showUserInfo">👤 Account</button>
            </header>

            <div class="process-buttons">
                <button :class="{ active: selectedTable === 'ReceivingTable' }" @click="selectedTable = 'ReceivingTable'">
                    📥 Receiving
                </button>
                <button :class="{ active: selectedTable === 'WashingTable' }" @click="selectedTable = 'WashingTable'">
                    🧼 Washing
                </button>
                <button :class="{ active: selectedTable === 'DistributionTable' }" @click="selectedTable = 'DistributionTable'">
                    📦 Distribution
                </button>
            </div>
            <component :is="selectedTable"></component>
        </main>
    </div>
</template>

<script>
import "@/assets/css/homeStyles.css";
import authService from "@/services/authService.js";
import ReceivingTable from "@/components/ReceivingTable.vue";
import WashingTable from "@/components/WashingTable.vue";
import DistributionTable from "@/components/DistributionTable.vue";

export default {
    components: { ReceivingTable, WashingTable, DistributionTable },
    data() {
        return {
            isAdmin: false,
            selectedTable: "ReceivingTable",
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
                location.reload();
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
        },
        checkAdminAccess() {
            const role = localStorage.getItem("role");
            this.isAdmin = role === "1";
        }
    },
    mounted() {
        this.checkAdminAccess();
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
