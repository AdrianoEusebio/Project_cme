import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '../pages/LoginPage.vue';
import HomePage from '@/pages/HomePage.vue';
import ProcessPage from '@/pages/ProcessPage.vue';
import UsersPage from '@/pages/UsersPage.vue';
import MaterialsPage from '@/pages/MaterialsPage.vue';

const routes = [
  { path: '/', redirect: '/login' },
  { path: '/login', component: LoginPage },
  { path: '/home', component: HomePage },
  {path: '/process', component: ProcessPage},
  {path: '/users', component: UsersPage},
  {path: '/materials', component: MaterialsPage}
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;