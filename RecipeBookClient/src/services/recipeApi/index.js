// import config
import axios from "axios";
import recipeRoutes from "./recipeRoutes";
import ingredientRoutes from "./ingredientRoutes";
import stepRoutes from "./stepRoutes";

export default class apiService {
    constructor() {
        this.apiService = axios.create()
        this.apiService.defaults.baseURL = 'https://localhost:7109/'

        this.recipes = new recipeRoutes(this.apiService)
        this.ingredients = new ingredientRoutes(this.apiService)
        this.steps = new stepRoutes(this.apiService)
    }

    // use auth
}