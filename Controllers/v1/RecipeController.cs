using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;
using RecipeBook.Repositories;

namespace RecipeBook.Controllers.v1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeBookContext _context;
        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(RecipeBookContext context, ILogger<RecipeController> logger, IRecipeRepository recipeRepository)
        {
            _context = context;
            _logger = logger;
            _recipeRepository = recipeRepository;

            _logger.LogInformation("\n Recipe Controller Started: {0}\n", DateTime.Now);
        }

        [HttpGet("recipe/{id}")]
        public IActionResult GetRecipeById(int id)
        {
            Recipe? recipe = _recipeRepository.GetRecipeIfExists(id);

            if (recipe == null)
            {
                return NotFound(("No recipe with id: {0}", id));
            }
            _context.Dispose();

            return Ok(recipe);
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] Recipe recipe)
        {
            if (recipe == null)
            {
                return BadRequest();
            }
            if (_recipeRepository.GetRecipeIfExists(recipe.Id) != null)
            {
                return BadRequest("Recipe Exists");
            }
            _recipeRepository.AddRecipe(recipe);

            return Ok();
        }
    }
}
