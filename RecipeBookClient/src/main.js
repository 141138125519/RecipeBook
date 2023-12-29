import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import apiService from '@/services/recipeApi'

const app = createApp(App)

app.use(router)

app.provide('$recipeApi', new apiService())

app.mount('#app')
