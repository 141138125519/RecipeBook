using Moq;
using RecipeBook.Data.DTOs;
using RecipeBook.Models;
using RecipeBook.Repositories;

namespace Tests.Mocks
{
    internal class MockRecipeRepository
    {
        
        /*
         
        Need to rewrite this mock to make use of automapper.

        That should make converting between Recipe and RecipeDTO
        much easier and cleaner.
         
         */


        public static Mock<IRecipeRepository> GetMock()
        {
            List<Recipe> recipes = new List<Recipe>();
            // set up test recipes
            recipes.Add(new Recipe
            {
                Id = 1,
                Name = "First Of Many (maybe)",
                CookingTimeMins = 180,
                Ingredients = new List<Ingredient>()
            });


            var mock = new Mock<IRecipeRepository>();

            mock.Setup(m => m.GetAll()).Returns(() => recipes.Select(r => new RecipeDTO
            {
                Id= r.Id,
                Name = r.Name,
                CookingTimeMins = r.CookingTimeMins,
                Ingredients = r.Ingredients.Select(i  => new IngredientDTO
                {
                    Id = i.Id,
                    Name = i.Name,
                    RecipeId = i.Recipe.Id,
                }).ToList()
            }).ToList()
            );

            mock.Setup(m => m.GetIfExists(It.IsAny<int>()))
                .Returns((int id) => recipes.Select(r => new RecipeDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    CookingTimeMins = r.CookingTimeMins,
                    Ingredients = r.Ingredients.Select(i => new IngredientDTO
                    {
                        Id = i.Id,
                        Name = i.Name,
                        RecipeId = i.Recipe.Id,
                    }).ToList()
                }).FirstOrDefault(r => r.Id == id)
                );

            mock.Setup(m => m.AddRecipe(It.IsAny<RecipeDTO>()))
                .Callback((RecipeDTO recipe) =>
                {
                    Recipe newRecipe = new()
                    {
                        Id = recipe.Id,
                        Name = recipe.Name,
                        CookingTimeMins = recipe.CookingTimeMins,
                        Ingredients = new List<Ingredient>()
                    };
                    recipes.Add(newRecipe);
                });
            
            mock.Setup(m => m.UpdateRecipe(It.IsAny<RecipeDTO>()))
                .Callback((RecipeDTO recipe) => 
                {
                    // Could do with improving this
                    Recipe updatedRecipe = new()
                    {
                        Id = recipe.Id,
                        Name = recipe.Name,
                        CookingTimeMins = recipe.CookingTimeMins,
                        Ingredients = recipe.Ingredients.Select(i => new Ingredient
                        {
                            Id = i.Id,
                            Name = i.Name
                        }).ToList()
                    };


                    recipes.Remove(recipes.FirstOrDefault(r => r.Id == recipe.Id));
                    recipes.Add(updatedRecipe);
                });

            mock.Setup(m => m.DeleteRecipe(It.IsAny<int>()))
                .Callback((int id) => { recipes.Remove(recipes.FirstOrDefault(r => r.Id == id)); });

            return mock;
        }
    }
}
