using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RecipeBook.Controllers.v1;
using RecipeBook.Data.DTOs;
using RecipeBook.Models;
using Tests.Mocks;

namespace Tests
{
    public class RecipeControllerTests
    {
        RecipeController controller;
        ILogger<RecipeController> logger;
        

        public RecipeControllerTests()
        {
            var mockRecipeRepository = MockRecipeRepository.GetMock();
            logger = Mock.Of<ILogger<RecipeController>>();

            controller = new RecipeController(logger, mockRecipeRepository.Object);
        }

        [Fact]
        public void ControllerExitsts()
        {
            Assert.NotNull(controller);
        }

        [Fact]
        public void GetAll_Test()
        {
            var result = controller.Get() as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<RecipeDTO>>(result.Value);
            Assert.NotEmpty(result.Value as IEnumerable<RecipeDTO>);
        }

        [Fact]
        public void GetById_Test1()
        {
            var result = controller.GetRecipeById(1) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<RecipeDTO>(result.Value);

            RecipeDTO recipe = (RecipeDTO)result.Value;
            Assert.NotNull(recipe);
            Assert.Equal(1, recipe.Id);
            Assert.Equal("First Of Many (maybe)", recipe.Name);
            Assert.Equal(180, recipe.CookingTimeMins);
        }

        [Fact]
        public void GetById_Test2()
        {
            var result = controller.GetRecipeById(2) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public void AddRecipe_Test1()
        {
            RecipeDTO testRecipe = new RecipeDTO
            {
                Id = 2,
                Name = "Test Recipe",
                CookingTimeMins = 60,
                Ingredients = new List<IngredientDTO>()
            };

            controller.AddRecipe(testRecipe);

            var result = controller.GetRecipeById(testRecipe.Id) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<RecipeDTO>(result.Value);

            RecipeDTO returnedRecipe = (RecipeDTO)result.Value;

            Assert.NotNull(returnedRecipe);

            Assert.Equal(testRecipe.Id, returnedRecipe.Id);
            Assert.Equal(testRecipe.Name, returnedRecipe.Name);
            Assert.Equal(testRecipe.CookingTimeMins, returnedRecipe.CookingTimeMins);
            Assert.Equal(testRecipe.Ingredients, returnedRecipe.Ingredients);

            // need to work out why this returns false when it shouldnt?
            // I have a feeling this could be fixed with automapper?
            //Assert.Equal(testRecipe, returnedRecipe);
        }

        [Fact]
        public void AddRecipe_Test2()
        {
            RecipeDTO testRecipe = new RecipeDTO
            {
                Id = 1,
                Name = "Test Recipe",
                CookingTimeMins = 60
            };

            var result = controller.AddRecipe(testRecipe) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public void UpdateRecipe_Test()
        {
            RecipeDTO testRecipe = new RecipeDTO
            {
                Id = 1,
                Name = "Test Recipe",
                CookingTimeMins = 60,
                Ingredients = new List<IngredientDTO>()
            };

            controller.UpdateRecipe(testRecipe);

            var result = controller.GetRecipeById(testRecipe.Id) as ObjectResult;
            
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            
            var returnedRecipe = (RecipeDTO)result.Value;

            Assert.NotNull(returnedRecipe);

            Assert.Equal(testRecipe.Id, returnedRecipe.Id);
            Assert.Equal(testRecipe.Name, returnedRecipe.Name);
            Assert.Equal(testRecipe.CookingTimeMins, returnedRecipe.CookingTimeMins);
            Assert.Equal(testRecipe.Ingredients, returnedRecipe.Ingredients);

            // need to work out why this returns false when it shouldnt?
            // I have a feeling this could be fixed with automapper?
            //Assert.Equal(testRecipe, returnedRecipe); 
        }

        [Fact]
        public void DeleteRecipe_Test()
        {
            controller.DeleteRecipe(1);

            var result = controller.Get() as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<RecipeDTO>>(result.Value);
            Assert.Empty((IEnumerable<RecipeDTO>)result.Value);
        }
    }
}
