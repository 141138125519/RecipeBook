<template>
    <main>
        <h1>{{ title() }}</h1>
        <div class="card">
            <h3>Recipe Title</h3>
            <input
                v-model.trim="recipe.name"
            >
            <h3>Total Prep/Cooking Time (mins):</h3>
            <input
                v-model.number="recipe.cookingTimeMins"
            >
        </div>
        
        <div class="card">
            <h3>Ingredients</h3>
            <TransitionGroup name="ingredientList" tag="ul" class="container">
                <div v-for="ingredient in ingredientList"
                    :key="ingredient"
                    class="ingredient"
                >
                    <input v-model.trim="ingredient.name" placeholder="Ingredient" class="ingredientName">
                    <input v-model.number="ingredient.quantity" placeholder="Quantity" class="smallItem">
                    <input v-model.trim="ingredient.unit" placeholder="Unit" class="smallItem">
                    <div @click="markIngredientForDeletion(ingredient)">
                        <img alt="delete" class="logo" src="@/assets/delete.svg" width="25" height="25"/>
                    </div>
                </div>
            </TransitionGroup>
            <button @click="newIngredient">Add Ingredient</button>
        </div>

        <div class="card">
            <h3>Steps</h3>
            <TransitionGroup name="stepList" tag="ul" class="container">
                <div v-for="step in stepList"
                    :key="step"
                    class="step"
                >
                    <input v-model.number="step.positionInRecipe" placeholder="Position in recipe" class="smallItem" @input="updateStepOrder(step)">
                    <textarea v-model.trim="step.instruction" placeholder="Instruction" class="stepInstruction"></textarea>
                    <div @click="markStepForDeletion(step)">
                        <img alt="delete" class="logo" src="@/assets/delete.svg" width="25" height="25"/>
                    </div>
                </div>
            </TransitionGroup>
            <button @click="newStep">Add Step</button>
        </div>
        <button @click="save">Save</button>
    </main>
</template>

<script setup>
import { inject, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'

const route = useRoute()
const router = useRouter()
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
let stepList = {}
let stepsToDelete = []

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
    recipe.steps = []
}
ingredientList = ref(recipe.ingredients)
stepList = ref(recipe.steps)
stepList.value.sort(function(a, b){return a.positionInRecipe - b.positionInRecipe})

async function save() {
    let missingFields = checkRequired(recipe)

    if (missingFields.length > 0) {
        alert(`Not All Required Fields Are Present:\n\n${missingFields.join('\n')}`)
        return
    }

    // reset step order to 0 index
    stepList.value[0].positionInRecipe = 0
    updateStepOrder(stepList.value[0])

    let result = {}

    if (recipe.id == undefined) {
        result = await recipeApi.recipes.create(recipe)
    }
    else {
        result = await recipeApi.recipes.update(recipe)
        deleteIngredients()
        deleteSteps()
    }
    
    if (result.status == 200) {
        alert("Recipe Saved")
        // Should we really navigate away on saving?
        router.push({ path: `/recipes` })
    }
    else
    {
        alert("Saving Recipe Failed")
    }
}

function deleteIngredients() {
    ingredientsToDelete.forEach(ingredient => {
        if (ingredient.id != undefined) {
            recipeApi.ingredients.delete(ingredient.id)
        }
    });
}

function deleteSteps() {
    stepsToDelete.forEach(step => {
        if (step.id != undefined) {
            recipeApi.steps.delete(step.id)
        }
    });
}

function newIngredient() {
    ingredientList.value.push({name: "", recipeId: recipe.id})
}

function newStep() {
    stepList.value.push({positionInRecipe: stepList.value.length, recipeId: recipe.id})
}

function markIngredientForDeletion(ingredient) {
    ingredientsToDelete.push(ingredient)

    const i = ingredientList.value.indexOf(ingredient)
    ingredientList.value.splice(i, 1)
}

function markStepForDeletion(step) {
    stepsToDelete.push(step)

    const i = stepList.value.indexOf(step)
    stepList.value.splice(i, 1)
}

function updateStepOrder(step) {
    let nextPos = step.positionInRecipe + 1
    stepList.value.forEach(s => {
        if (s != step && s.positionInRecipe >= step.positionInRecipe) {
            s.positionInRecipe = nextPos
            nextPos++
        }
    })

    stepList.value.sort(function(a, b){return a.positionInRecipe - b.positionInRecipe})
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
    if (recipe.steps == undefined) {
        missingFields.push('Steps')
    }

    return missingFields
}

</script>


<style scoped>
.card {
    box-shadow: 0px 0px 10px 0px var(--dracular-c-current-line);
    border-radius: 0.5rem;
    display: grid;
    width: 100%;
    margin-bottom: 1rem;
}

.container {
    border-radius: 0.5rem;
    position: relative;
    padding: 0;
}

.ingredient {
    display: flex;
    flex-wrap: wrap;
    margin-bottom: 2px;
}

.ingredientName {
    flex-grow: 4;
}

.step {
    display: flex;
    flex-wrap: wrap;
    margin-bottom: 2px;
}

.stepInstruction {
    resize: vertical;
    flex-grow: 4;
}

/* Could do with a better name? */
.smallItem {
    max-width: 50px;
}

/* 1. declare transition */
.stepList-move,
.stepList-enter-active,
.stepList-leave-active,
.ingredientList-move,
.ingredientList-enter-active,
.ingredientList-leave-active {
    transition: all 0.5s cubic-bezier(0.55, 0, 0.1, 1);
}

/* 2. declare enter from and leave to state */
.stepList-enter-from,
.stepList-leave-to,
.ingredientList-enter-from,
.ingredientList-leave-to {
    opacity: 0;
    transform: scaleY(0.01) translate(30px, 0);
}

/* 3. ensure leaving items are taken out of layout flow so that moving
      animations can be calculated correctly. */
.stepList-leave-active,
.ingredientList-leave-active {
    position: absolute;
}

</style>
