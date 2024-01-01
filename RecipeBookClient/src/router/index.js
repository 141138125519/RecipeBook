import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import RecipeView from '../views/RecipeView.vue'
import RecipeEditorView from '@/views/RecipeEditorView.vue'
import RecipeDisplayView from '@/views/RecipeDisplayView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/recipes',
      name: 'recipes',
      component: RecipeView
    },
    {
      path: '/recipe/:id',
      name: 'recipe',
      component: RecipeDisplayView
    },
    {
      path: '/recipe-edit/:id',
      name: 'recipeEdit',
      component: RecipeEditorView,
    }
  ]
})

export default router
