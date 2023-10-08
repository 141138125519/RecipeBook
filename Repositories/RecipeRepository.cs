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
            var allRecipes = _context.Recipes.ToList();
            return allRecipes;
        }

        public Recipe? GetIfExists(int id)
        {
            var recipe = _context.Find<Recipe>(id);
            return recipe;
        }

        public void AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }
    }
}
