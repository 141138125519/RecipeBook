using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;
using RecipeBook.Repositories;
using RecipeBook.Data.DTOs;

namespace RecipeBook.Controllers.v1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(ILogger<RecipeController> logger, IRecipeRepository recipeRepository)
        {
            _logger = logger;
            _recipeRepository = recipeRepository;

            _logger.LogInformation("Recipe Controller Started: {now}", DateTime.Now);
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            return Ok(_recipeRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetRecipeById(int id)
        {
            RecipeDTO? recipe = _recipeRepository.GetIfExists(id);

            if (recipe == null)
            {
                return NotFound(("No recipe with id: {0}", id));
            }

            return Ok(recipe);
        }

        [HttpPost]
        public IActionResult AddRecipe([FromBody] RecipeDTO recipe)
        {
            if (recipe == null)
            {
                return BadRequest();
            }
            if (_recipeRepository.GetIfExists(recipe.Id) != null)
            {
                return BadRequest("Recipe Exists");
            }
            _recipeRepository.AddRecipe(recipe);

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateRecipe([FromBody] RecipeDTO recipe)
        {
            if (recipe == null)
            {
                return BadRequest();
            }
            try
            {
                _recipeRepository.UpdateRecipe(recipe);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in Recipe Controller:\n{ex}");
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            try
            {
                _recipeRepository.DeleteRecipe(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
            
            return Ok();
        }

    }
}
