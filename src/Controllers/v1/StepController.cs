using Microsoft.AspNetCore.Mvc;
using RecipeBook.Data.DTOs;
using RecipeBook.Repositories.StepRepository;

namespace RecipeBook.Controllers.v1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class StepController : ControllerBase
    {
        private readonly ILogger<StepController> _logger;
        private readonly IStepRepository _stepRepository;

        public StepController(ILogger<StepController> logger, IStepRepository stepRepository)
        {
            _logger = logger;
            _stepRepository = stepRepository;

            _logger.LogInformation("Step Controller Started: {now}", DateTime.Now);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_stepRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Add([FromBody] StepDTO step)
        {
            _stepRepository.Add(step);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] StepDTO step)
        {
            if (step == null)
            {
                return BadRequest();
            }

            try
            {
                _stepRepository.Update(step);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in Step Controller:\n{ex}", ex);
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _stepRepository.Delete(id);
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
