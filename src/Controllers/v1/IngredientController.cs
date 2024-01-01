using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;
using RecipeBook.Repositories.IngredientRepository;
using RecipeBook.Data.DTOs;
using AutoMapper;

namespace RecipeBook.Controllers.v1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class IngredientController : ControllerBase
    {
        private readonly ILogger<IngredientController> _logger;
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(ILogger<IngredientController> logger,
            IIngredientRepository ingredientRepository)
        {
            _logger = logger;
            _ingredientRepository = ingredientRepository;

            _logger.LogInformation("Ingredient Controller Started: {now}", DateTime.Now);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_ingredientRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Add([FromBody] IngredientDTO ingredient)
        {
            _ingredientRepository.Add(ingredient);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] IngredientDTO ingredient)
        {
            if (ingredient == null)
            {
                return BadRequest();
            }

            try
            {
                _ingredientRepository.Update(ingredient);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in Ingredient Controller:\n{ex}");
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _ingredientRepository.Delete(id);
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
