using RecipeBook.Data.DTOs;
using RecipeBook.Models;

namespace RecipeBook.Repositories.IngredientRepository
{
    public interface IIngredientRepository
    {
        public List<IngredientDTO> GetAll();
        public IngredientDTO? GetIfExists(int id);
        public void Add(IngredientDTO ingredient);
        public void Update(IngredientDTO ingredient);
        public void Delete(int id);
    }
}
