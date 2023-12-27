using Moq;
using RecipeBook.Models;
using RecipeBook.Repositories;

namespace Tests.Mocks
{
    internal class MockRecipeRepository
    {
        public static Mock<IRecipeRepository> GetMock()
        {
            List<Recipe> recipes = new List<Recipe>();
            // set up test recipes
            recipes.Add(new Recipe
            {
                Id = 1,
                Name = "First Of Many (maybe)",
                CookingTimeMins = 180,
            });


            var mock = new Mock<IRecipeRepository>();

            mock.Setup(m => m.GetAll()).Returns(() => recipes);

            mock.Setup(m => m.GetIfExists(It.IsAny<int>()))
                .Returns((int id) => recipes.Find(r => r.Id == id));

            mock.Setup(m => m.AddRecipe(It.IsAny<Recipe>()))
                .Callback((Recipe recipe) => { recipes.Add(recipe); });     
            
            mock.Setup(m => m.UpdateRecipe(It.IsAny<Recipe>()))
                .Callback((Recipe recipe) => 
                {
                    // Could do with improving this
                    recipes.Remove(recipes.FirstOrDefault(r => r.Id == recipe.Id));
                    recipes.Add(recipe);
                });

            mock.Setup(m => m.DeleteRecipe(It.IsAny<int>()))
                .Callback((int id) => { recipes.Remove(recipes.FirstOrDefault(r => r.Id == id)); });

            return mock;
        }
    }
}
