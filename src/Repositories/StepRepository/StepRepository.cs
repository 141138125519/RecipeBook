using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Data.DTOs;
using RecipeBook.Models;

namespace RecipeBook.Repositories.StepRepository
{
    public class StepRepository : IStepRepository
    {
        private readonly ILogger<StepRepository> _logger;
        private readonly RecipeBookContext _context;
        private readonly IMapper _mapper;

        public StepRepository(ILogger<StepRepository> logger,
            RecipeBookContext context,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public List<StepDTO> GetAll()
        {
            _logger.LogInformation("Getting All Steps - {time}", DateTime.Now);

            var allSteps = _context.Steps
                                .Include(s => s.Recipe)
                                .Select(s => _mapper.Map<StepDTO>(s))
                                .ToList();

            return allSteps;
        }

        public StepDTO? GetIfExists(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(StepDTO step)
        {
            _logger.LogInformation("Adding New Step: '{id}'  - {time}", step.Id, DateTime.Now);

            var recipe = _context.Find<Recipe>(step.RecipeId) ?? throw new Exception("Recipe Does Not Exist");

            var newStep = _mapper.Map<Step>(step);
            newStep.Recipe = recipe;

            _context.Steps.Add(newStep);
            _context.SaveChanges();
        }

        public void Update(StepDTO step)
        {
            _logger.LogInformation("Updating Step: {id}  - {time}", step.Id, DateTime.Now);

            var updatedStep = _mapper.Map<Step>(step);

            try
            {
                _context.Entry(updatedStep).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("Something Went Wrong Updating Step:\n{ex}", ex);
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation("Deleting Step: {id} - {time}", id, DateTime.Now);

            var step = _context.Steps.Find(id);
            if (step != null)
            {
                _context.Steps.Remove(step);
                _context.SaveChanges();
            }
        }
    }
}
