<script>

export default {
    inject: ['$recipeApi'],
    async mounted() {
        console.log("trying to get recipes")

        let recipes = await this.$recipeApi.recipes.getAll()
        console.log(recipes.data)

        console.log("done attempt")
    }
}

</script>

<template>
    <main>
        <h1>Recipes</h1>
        <div>
            
        </div>
    </main>
</template>
