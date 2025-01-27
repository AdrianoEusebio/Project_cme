<template>
  <div class="login-container">
    <div class="login-card">
      <h2>Bem-vindo ao CMEBringel</h2>
      <p>Faça login para continuar</p>

      <input v-model="username" type="text" placeholder="Usuário" class="input-field" required />
      <input v-model="password" type="password" placeholder="Senha" class="input-field" required />

      <button @click="handleLogin" class="login-button">Entrar</button>

      <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
      <p class="forgot-password" @click="forgotPassword">Esqueceu sua senha?</p>
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

    const handleLogin = async () => {
      try {
        const response = await authService.login(username.value, password.value);

        localStorage.setItem("token", response.token);
        localStorage.setItem("idGroup", response.idGroup);

        console.log("Usuário logado:", response);

        if (response.idGroup === 1) {
          router.push("/homeadmin");
        } else {
          router.push("/home");
        }
      } catch (error) {
        errorMessage.value = "Usuário ou senha inválidos!";
      }
    };

    const forgotPassword = () => {
      alert("Função de recuperação ainda não implementada");
    };

    return { username, password, handleLogin, forgotPassword, errorMessage };
  },
};
</script>

<style scoped>
/* Estilo padronizado com a HomePage */
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background: #2c3e59;
}

.login-card {
  background: white;
  padding: 30px;
  border-radius: 10px;
  box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.2);
  text-align: center;
  width: 380px;
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
</style>
