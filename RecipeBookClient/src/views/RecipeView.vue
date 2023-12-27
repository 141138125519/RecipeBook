<script>
import RecipeItem from '../components/RecipeItem.vue'

export default {
    inject: ['$recipeApi'],
    
    components: {
        RecipeItem,
    },

    data() {
        return {
            recipes: []
        }
    },

    async mounted() {
        let recipes = await this.$recipeApi.recipes.getAll()
        this.recipes = recipes.data
    }
}

</script>

<template>
    <main>
        <h1>Recipes</h1>
        <div>
            <RecipeItem v-for="recipe in this.recipes">
                <template #name>{{ recipe.name }}</template>
                <template #cookingTime>{{ recipe.cookingTimeMins }}</template>
            </RecipeItem>
        </div>
    </main>
</template>
