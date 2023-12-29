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
const emit = defineEmits(['deletedRecipe'])

async function deleteRecipe() {
    console.log('delete', props.recipe.id)
    let result = await recipeApi.recipes.delete(props.recipe.id)
    console.log(result)
    // emit event
    emit('deletedRecipe')
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
    margin-right: 2rem;
}

.cookingTime {
    flex: 1;
    margin-left: 1rem;
}

.actions {
    margin-left: 1rem;
}

button {
    margin: 0.1rem;
}

h3 {
    margin-right: 1rem;
    margin-bottom: 0.4rem;
}

</style>