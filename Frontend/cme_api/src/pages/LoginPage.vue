<template>
  <div class="login-container">
    <div class="login-card">
      <h2>Bem-vindo ao CMEBringel</h2>
      <p>Faça login para continuar</p>
      <input v-model="username" type="text" placeholder="Usuário" class="input-field" required />
      <input v-model="password" type="password" placeholder="Senha" class="input-field" required />

      <button @click="handleLogin" class="login-button">Entrar</button>
      <br /><br />
      <div class="info-button" @click="showTestInfo">?</div>
      <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
    </div>
    <div v-if="showModal" class="modal-overlay" @click.self="showModal = false">
      <div class="modal">
        <h3>Dados de Teste</h3>
        <p><strong>Usuário:</strong> superadmin</p>
        <p><strong>Senha:</strong> admin123</p>
        <button @click="showModal = false" class="close-button">Fechar</button>
      </div>
    </div>
  </div>
</template>

<script>
import { useRouter } from "vue-router";
import { ref } from "vue";
import authService from "@/services/authService";

export default {
  name: "LoginPage",
  setup() {
    const router = useRouter();
    const username = ref("");
    const password = ref("");
    const errorMessage = ref("");
    const showModal = ref(false);

    const handleLogin = async () => {
      try {
        const response = await authService.login(username.value, password.value);

        localStorage.setItem("token", response.token);
        localStorage.setItem("idGroup", response.idGroup);

        console.log("Usuário logado:", response);
        router.push("/home");
      } catch (error) {
        errorMessage.value = "Usuário ou senha inválidos!";
      }
    };

    const forgotPassword = () => {
      alert("Função de recuperação ainda não implementada");
    };

    const showTestInfo = () => {
      showModal.value = true;
    };

    return { username, password, handleLogin, forgotPassword, errorMessage, showModal, showTestInfo };
  },
};
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  width: 100vw;
  background: #2c3e50;
}

.login-card {
  position: relative;
  background: white;
  padding: 40px;
  border-radius: 10px;
  box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.2);
  text-align: center;
  width: 100%;
  max-width: 400px;
  margin: auto;
  box-sizing: border-box;
}

.info-button {
  position: absolute;
  top: 10px;
  right: 10px;
  cursor: pointer;
  font-size: 18px;
  background: #1abc9c;
  color: white;
  border-radius: 50%;
  width: 25px;
  height: 25px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: bold;
  transition: background 0.3s;
}

.info-button:hover {
  background: #16a085;
}

h2 {
  margin-bottom: 10px;
  color: #2c3e50;
  font-weight: bold;
}

p {
  color: #666;
  margin-bottom: 20px;
}

.input-field {
  width: 100%;
  padding: 12px;
  margin: 10px 0;
  border: 1px solid #ccc;
  border-radius: 5px;
  font-size: 16px;
  box-sizing: border-box;
}

.login-button {
  width: 100%;
  padding: 12px;
  background: #1abc9c;
  color: white;
  border: none;
  border-radius: 5px;
  font-size: 16px;
  font-weight: bold;
  cursor: pointer;
  transition: background 0.3s;
}

.login-button:hover {
  background: #16a085;
}

.error-message {
  color: red;
  margin-top: 10px;
  font-weight: bold;
}

.forgot-password {
  color: #1abc9c;
  cursor: pointer;
  margin-top: 15px;
  font-weight: bold;
}

.forgot-password:hover {
  text-decoration: underline;
}

/* Estilos do Modal */
.modal-overlay {
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

.modal {
  background: white;
  padding: 20px;
  border-radius: 10px;
  text-align: center;
  box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.3);
  width: 300px;
}

.modal h3 {
  margin-bottom: 15px;
  color: #2c3e50;
}

.modal p {
  color: #333;
  font-size: 16px;
  margin-bottom: 10px;
}

.close-button {
  background: #e74c3c;
  color: white;
  border: none;
  padding: 10px 15px;
  font-size: 14px;
  border-radius: 5px;
  cursor: pointer;
  transition: background 0.3s;
}

.close-button:hover {
  background: #c0392b;
}
</style>
