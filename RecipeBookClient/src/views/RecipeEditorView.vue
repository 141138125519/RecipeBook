<template>
    <main>
        <h1>{{ title() }}</h1>
        <h3>Recipe Title</h3>
        <input
            v-model.trim="recipe.name"
        >
        <h3>Total Prep/Cooking Time:</h3>
        <input
            v-model.number="recipe.cookingTimeMins"
        >
        <button @click="save">Save</button>
    </main>
</template>

<script setup>
import { inject } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const recipeApi = inject('$recipeApi')

function title() {
    if (route.params.id == 'new')
    {
        return 'New Recipe'
    }
    else
    {
        return 'Edit Recipe'
    }
}

let recipe = {}

async function getRecipe(id) {
    let result = await recipeApi.recipes.get(id)
    return result.data
}

if (route.params.id != 'new') {
    recipe = await getRecipe(route.params.id)
}

async function save() {
    let missingFields = checkRequired(recipe)
    if (missingFields.length > 0) {
        alert(`Not All Required Fields Are Present:\n\n${missingFields.join('\n')}`)
        return
    }
    if (recipe.id == undefined) {
        recipeApi.recipes.create(recipe)
    }
    else {
        recipeApi.recipes.update(recipe)
    }
}

function checkRequired(recipe) {
    let missingFields = []

    if (recipe.name == undefined || recipe.name == "") {
        missingFields.push('Name')
    }
    if (recipe.cookingTimeMins == undefined || recipe.cookingTimeMins == "") {
        missingFields.push('Total Prep/Cooking Time')
    }

    return missingFields
}

</script>