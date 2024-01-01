export default class stepRoutes {
    constructor(apiService) {
        this.apiService = apiService
    }

    delete(id) {
        return this.apiService.delete(`v1/Step/${id}`)
    }
}