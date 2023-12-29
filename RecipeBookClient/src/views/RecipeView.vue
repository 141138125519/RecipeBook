<script setup>
import { inject, ref } from 'vue'
import { useRouter, RouterLink } from 'vue-router'
import RecipeItem from '../components/RecipeItem.vue'

const router = useRouter()
const recipeApi = inject('$recipeApi')

const recipes = ref(await loadRecipes())

async function loadRecipes() {
    let result = await recipeApi.recipes.getAll()
    return result.data
}

function deletedRecipe(recipe) {
    const i = recipes.value.indexOf(recipe)
    recipes.value.splice(i, 1)
}

function editRecipe(recipe) {
    router.push({ path: `/recipe-edit/${recipe.id}` })
}

</script>

<template>
    <main>
        <h1>Recipes</h1>
        <RouterLink to="/recipe-edit/new">New Recipe</RouterLink>
        <TransitionGroup name="recipeList" tag="ul" class="container">
            <RecipeItem
                v-for="recipe in recipes"
                :key="recipe"
                :recipe="recipe"
                @deletedRecipe="deletedRecipe(recipe)"
                @editRecipe="editRecipe(recipe)"
            />
        </TransitionGroup>
    </main>
</template>


<style>

.container {
    box-shadow: 0px 0px 10px 0px rgba(45, 45, 45, 0.75);
    border-radius: 0.5rem;
    position: relative;
    padding: 0;
}

.recipe {
    width: 100%;
    height: 30px;
    box-sizing: border-box;
}

.recipe:hover {
    background: rgba(100, 100, 100, 0.5);
    border-radius: 0.5rem;
}

/* 1. declare transition */
.recipeList-move,
.recipeList-enter-active,
.recipeList-leave-active {
    transition: all 0.5s cubic-bezier(0.55, 0, 0.1, 1);
}

/* 2. declare enter from and leave to state */
.recipeList-enter-from,
.recipeList-leave-to {
    opacity: 0;
    transform: scaleY(0.01) translate(30px, 0);
}

/* 3. ensure leaving items are taken out of layout flow so that moving
      animations can be calculated correctly. */
.recipeList-leave-active {
    position: absolute;
}

</style>