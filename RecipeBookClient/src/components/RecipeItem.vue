<template>
    <div class="recipe">
        <h3>
            {{ recipe.name }}
        </h3>
        <h3>
            Total Cooking/Prep time: {{ recipe.cookingTimeMins }}
        </h3>
        <div @click="deleteRecipe">
            <img alt="delete" class="logo" src="@/assets/delete.svg" width="25" height="25"/>
        </div>
        <div @click="editRecipe">
            <img alt="edit" class="logo" src="@/assets/pencil.svg" width="25" height="25"/>
        </div>
    </div>
</template>

<script setup>
import { inject } from 'vue'

const props = defineProps(['recipe'])
const recipeApi = inject('$recipeApi')
const emit = defineEmits(['deletedRecipe', 'editRecipe'])

async function deleteRecipe() {
    console.log('delete', props.recipe.id)
    let result = await recipeApi.recipes.delete(props.recipe.id)
    console.log(result)
    // emit event
    emit('deletedRecipe')
}

function editRecipe() {
    emit('editRecipe')
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

.logo {
    display: block;
}

button {
    margin: 0.1rem;
}

h3 {
    margin-right: 1rem;
}

</style>