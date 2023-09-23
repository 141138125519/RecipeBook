﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;

namespace RecipeBook.Controllers.v1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeBookContext _context;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(RecipeBookContext context, ILogger<RecipeController> logger)
        {
            _context = context;
            _logger = logger;

            _logger.LogInformation("\n Recipe Controller Started: {0}\n", DateTime.Now);
        }

        [HttpGet("recipe/{id}")]
        public IActionResult GetRecipeById(int id)
        {
            Recipe? recipe = _context.Find<Recipe>(id);
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
            if (_context.Find<Recipe>(recipe.Id) != null)
            {
                return BadRequest("Recipe Exists");
            }
            _context.Add(recipe);
            _context.SaveChanges();
            _context.Dispose();

            return Ok();
        }
    }
}