using RecipeBook.Data.DTOs;
using RecipeBook.Models;

namespace RecipeBook.Repositories
{
    public interface IRecipeRepository
    {
        public List<RecipeDTO> GetAll();
        public RecipeDTO? GetIfExists(int id);
        public void AddRecipe(RecipeDTO recipe);
        public void UpdateRecipe(RecipeDTO recipe);
        public void DeleteRecipe(int id);
    }
}
