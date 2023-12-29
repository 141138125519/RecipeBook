export default class recipeRoutes {
    constructor(apiService) {
        this.apiService = apiService
    }

    getAll() {
        return this.apiService.get(`v1/Recipe/all`)
    }

    get(id) {
        return this.apiService.get(`v1/Recipe/${id}`)
    }

    create(recipe) {
        return this.apiService.post(`v1/Recipe/`, recipe)
    }

    delete(recipeId) {
        return this.apiService.delete(`v1/Recipe/${recipeId}`)
    }
}