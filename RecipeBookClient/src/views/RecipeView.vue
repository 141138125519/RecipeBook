<script setup>
import { inject, ref } from 'vue'
import RecipeItem from '../components/RecipeItem.vue'

const recipeApi = inject('$recipeApi')

const recipes = ref(await loadRecipes())

async function loadRecipes() {
    let result = await recipeApi.recipes.getAll()
    return result.data
}
console.log("recipes", recipes)

function deletedRecipe(recipe) {
    const i = recipes.value.indexOf(recipe)
    recipes.value.splice(i, 1)
}

</script>

<template>
    <Suspense>
        <main>
            <h1>Recipes</h1>
            <TransitionGroup name="recipeList" tag="ul" class="container">
                <RecipeItem v-for="recipe in recipes" :key="recipe" :recipe="recipe" @deletedRecipe="deletedRecipe(recipe)"/>
            </TransitionGroup>
        </main>
    </Suspense>
</template>


<style>

.container {
  position: relative;
  padding: 0;
}

.recipe {
  width: 100%;
  height: 30px;
  /* background-color: #000000;
  border: 1px solid #666; */
  box-sizing: border-box;
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