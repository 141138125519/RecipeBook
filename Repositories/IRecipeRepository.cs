using RecipeBook.Models;

namespace RecipeBook.Repositories
{
    public interface IRecipeRepository
    {
        public List<Recipe> GetAll();
        public Recipe? GetIfExists(int id);
        public void AddRecipe(Recipe recipe);
        public void DeleteRecipe(int id);
    }
}
