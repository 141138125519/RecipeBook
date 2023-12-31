<template>
    <main>
        <h1>{{ title() }}</h1>
        <h3>Recipe Title</h3>
        <input
            v-model.trim="recipe.name"
        >
        <h3>Total Prep/Cooking Time (mins):</h3>
        <input
            v-model.number="recipe.cookingTimeMins"
        >
        <!-- <IngredientsEdit :ingredients="recipe.ingredients"/> -->
        <h3>Ingredients</h3>
        <TransitionGroup name="ingredientList" tag="ul" class="container">
            <div v-for="ingredient in ingredientList" 
                :key="ingredient"
            >
                <input v-model.trim="ingredient.name">
                <input v-model.number="ingredient.quantity">
                <input v-model.trim="ingredient.unit">
                <button @click="markForDeletion(ingredient)">Delete</button>
            </div>
        </TransitionGroup>
        <button @click="newIngredient">Add Ingredient</button>
        <button @click="save">Save</button>
    </main>
</template>

<script setup>
import { inject, ref } from 'vue'
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
let ingredientList = {}
let ingredientsToDelete = []

async function getRecipe(id) {
    let result = await recipeApi.recipes.get(id)
    return result.data
}

if (route.params.id != 'new') {
    recipe = await getRecipe(route.params.id)
}
else
{
    recipe.ingredients = []
}
ingredientList = ref(recipe.ingredients)

async function save() {
    let missingFields = checkRequired(recipe)
    console.log(recipe)
    if (missingFields.length > 0) {
        alert(`Not All Required Fields Are Present:\n\n${missingFields.join('\n')}`)
        return
    }
    if (recipe.id == undefined) {
        recipeApi.recipes.create(recipe)
    }
    else {
        recipeApi.recipes.update(recipe)
        deleteIngredients()
    }
}

function deleteIngredients() {
    ingredientsToDelete.forEach(ingredient => {
        if (ingredient.id != undefined) {
            recipeApi.ingredients.delete(ingredient.id)
        }
    });
}

function newIngredient() {
    ingredientList.value.push({name: "new ingredient", recipeId: recipe.id})
}

function markForDeletion(ingredient) {
    ingredientsToDelete.push(ingredient)

    const i = ingredientList.value.indexOf(ingredient)
    ingredientList.value.splice(i, 1)
}

function checkRequired(recipe) {
    let missingFields = []

    if (recipe.name == undefined || recipe.name == "") {
        missingFields.push('Name')
    }
    if (recipe.cookingTimeMins == undefined || recipe.cookingTimeMins == "") {
        missingFields.push('Total Prep/Cooking Time')
    }
    if (recipe.ingredients == undefined) {
        missingFields.push('Ingredients')
    }

    return missingFields
}

</script>


<style scoped>
.container {
    box-shadow: 0px 0px 10px 0px var(--dracular-c-current-line);
    border-radius: 0.5rem;
    position: relative;
    padding: 0;
}


/* 1. declare transition */
.ingredientList-move,
.ingredientList-enter-active,
.ingredientList-leave-active {
    transition: all 0.5s cubic-bezier(0.55, 0, 0.1, 1);
}

/* 2. declare enter from and leave to state */
.ingredientList-enter-from,
.ingredientList-leave-to {
    opacity: 0;
    transform: scaleY(0.01) translate(30px, 0);
}

/* 3. ensure leaving items are taken out of layout flow so that moving
      animations can be calculated correctly. */
.ingredientList-leave-active {
    position: absolute;
}

</style>
