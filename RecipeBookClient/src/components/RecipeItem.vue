<template>
    <div class="recipe">
        <h3>
            {{ recipe.name }}
        </h3>
        <h4>
            Total Cooking/Prep time: {{ recipe.cookingTimeMins }}
        </h4>
        <div class="actions">
            <button @click="deleteRecipe">delete</button>
            <button>edit</button>
        </div>
    </div>
</template>

<script setup>
import { inject } from 'vue'

const props = defineProps(['recipe'])
const recipeApi = inject('$recipeApi')

async function deleteRecipe() {
    console.log('delete', props.recipe.id)
    let result = await recipeApi.recipes.delete(props.recipe.id)
    console.log(result)
    // emit event
}

</script>

<style scoped>
.recipe {
    display: flex;
    position: relative;
    place-items: center;
    place-content: center;
    width: 100%;
}

.name {
    border: 1px red solid;
    margin-right: 2rem;
}

.cookingTime {
    flex: 1;
    margin-left: 1rem;
}

.actions {
    border: 1px red solid;
}

h3 {
    margin-right: 1rem;
    margin-bottom: 0.4rem;
}

</style>