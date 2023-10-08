using RecipeBook.Models;

namespace RecipeBook.Repositories
{
    public interface IRecipeRepository
    {
        public Recipe? GetRecipeIfExists(int id);
        public void AddRecipe(Recipe recipe);
    }
}
