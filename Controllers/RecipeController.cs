using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;

namespace RecipeBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
