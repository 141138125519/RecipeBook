export default class ingredientRoutes {
    constructor(apiService) {
        this.apiService = apiService
    }

    delete(id) {
        return this.apiService.delete(`v1/Ingredient/${id}`)
    }
}