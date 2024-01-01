<template>
    <div class="recipe">
        <h3 class="name">
            {{ recipe.name }}
        </h3>
        <h3>
            Total time: {{ recipe.cookingTimeMins }}
        </h3>
        <div @click.stop="deleteRecipe">
            <img alt="delete" class="logo" src="@/assets/delete.svg" width="25" height="25"/>
        </div>
        <div @click.stop="editRecipe">
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
    padding: 0 10px;
}

.logo {
    display: block;
}

.name {
    flex-grow: 1;
}

</style>