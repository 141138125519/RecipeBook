<template>
    <div class="recipe">
        <h3>
            <slot name="name"></slot>
        </h3>
        <h4>
            Total time: <slot name="cookingTime"></slot>
        </h4>
    </div>
</template>

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

h3 {
    margin-right: 1rem;
    margin-bottom: 0.4rem;
}

</style>