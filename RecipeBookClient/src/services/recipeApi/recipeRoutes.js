export default class recipeRoutes {
    constructor(apiService) {
        this.apiService = apiService
    }

    getAll() {
        return this.apiService.get(`v1/Recipe/all`)
    }

    delete(recipeId) {
        return this.apiService.delete(`v1/Recipe/${recipeId}`)
    }
}