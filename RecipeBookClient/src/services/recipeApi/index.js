// import config
import axios from "axios";
import recipeRoutes from "./recipeRoutes";

export default class apiService {
    constructor() {
        this.apiService = axios.create()
        this.apiService.defaults.baseURL = 'https://localhost:7109/'

        this.recipes = new recipeRoutes(this.apiService)
    }

    // use auth
}