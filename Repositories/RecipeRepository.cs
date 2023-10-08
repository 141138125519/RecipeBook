using RecipeBook.Models;

namespace RecipeBook.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ILogger<RecipeRepository> _logger;
        private readonly RecipeBookContext _context;

        public RecipeRepository(ILogger<RecipeRepository> logger, RecipeBookContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Recipe> GetAll()
        {
            _logger.LogInformation("Getting All Recipes - {time}", DateTime.Now);
            var allRecipes = _context.Recipes.ToList();
            return allRecipes;
        }

        public Recipe? GetIfExists(int id)
        {
            _logger.LogInformation("Get Recipe: {id} (If It Exists) - {time}", id, DateTime.Now);
            var recipe = _context.Find<Recipe>(id);
            return recipe;
        }

        public void AddRecipe(Recipe recipe)
        {
            _logger.LogInformation("Adding New Recipe: '{name}'  - {time}", recipe.Name, DateTime.Now);
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }

        public void DeleteRecipe(int id)
        {
            _logger.LogInformation("Deleting Recipe: {id} - {time}", id, DateTime.Now);
            var recipe = _context.Recipes.Find(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
            }
        }
    }
}
