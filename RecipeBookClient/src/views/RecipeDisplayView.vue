<template>
    <main class="recipeDisplay">
        <h1>{{ recipe.name }}</h1>
        <div>
            <h3>Total Prep/Cooking Time (mins): {{ recipe.cookingTimeMins }}</h3>
        </div>

        <h3>Ingredients</h3>
        <div v-for="ingredient in recipe.ingredients"
            :key="ingredient"
            class="ingredient"
        >
            <a>{{ ingredient.name }}</a>
            <a>{{ ingredient.quantity }}</a>
            <a>{{ ingredient.unit }}</a>
        </div>

        <h3>Steps</h3>
        <div v-for="step in recipe.steps"
            :key="step"
            class="step"
        >
            <a>{{ step.positionInRecipe }} -</a>
            <a>{{ step.instruction }}</a>
        </div>
    </main>
</template>


<script setup>
import { inject } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const recipeApi = inject('$recipeApi')

async function getRecipe(id) {
    let result = await recipeApi.recipes.get(id)
    return result.data
}

let recipe = await getRecipe(route.params.id)
recipe.steps.sort(function(a, b){return a.positionInRecipe - b.positionInRecipe})

</script>

<style scoped>

.recipeDisplay {
    box-shadow: 0px 0px 10px 0px var(--dracular-c-current-line);
    border-radius: 0.5rem;
    padding: 0 5px;
}

</style>
