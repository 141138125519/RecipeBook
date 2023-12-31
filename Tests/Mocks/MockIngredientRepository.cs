using Moq;
using RecipeBook.Models;
using RecipeBook.Repositories.IngredientRepository;

namespace Tests.Mocks
{
    internal class MockIngredientRepository
    {
        public static Mock<IIngredientRepository> GetMock()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            ingredients.Add(new Ingredient
            {
                Id = 1,
                Name = "Test Ingredient",
                
            });

            var mock = new Mock<IIngredientRepository>();


            return mock;
        }
    }
}
