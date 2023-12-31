using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Data.DTOs;
using RecipeBook.Models;

namespace RecipeBook.Repositories.IngredientRepository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ILogger<IngredientRepository> _logger;
        private readonly RecipeBookContext _context;
        private readonly IMapper _mapper;

        public IngredientRepository(ILogger<IngredientRepository> logger,
            RecipeBookContext context,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public List<IngredientDTO> GetAll()
        {
            _logger.LogInformation("Getting All Ingredients - {time}", DateTime.Now);

            var allIngredients = _context.Ingredients
                                    .Include(i => i.Recipe)
                                    .Select(i => _mapper.Map<IngredientDTO>(i))
                                    .ToList();

            return allIngredients;
        }

        public IngredientDTO? GetIfExists(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(IngredientDTO ingredient)
        {
            _logger.LogInformation("Adding New Ingredient: '{name}'  - {time}", ingredient.Name, DateTime.Now);

            var recipe = _context.Find<Recipe>(ingredient.RecipeId) ?? throw new Exception("Recipe Does Not Exist");

            var newIngredient = new Ingredient()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Recipe = recipe
            };

            _context.Ingredients.Add(newIngredient);
            _context.SaveChanges();
        }

        public void Update(IngredientDTO ingredient)
        {
            _logger.LogInformation("Updating Ingredient: {id}  - {time}", ingredient.Id, DateTime.Now);

            var updatedIngredient = _mapper.Map<Ingredient>(ingredient);

            try
            {
                _context.Entry(updatedIngredient).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("Something Went Wrong Updating Ingredient:\n{ex}", ex);
            }
            
        }

        public void Delete(int id)
        {
            _logger.LogInformation("Deleting Recipe: {id} - {time}", id, DateTime.Now);

            var ingredient = _context.Ingredients.Find(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                _context.SaveChanges();
            }
        }
    }
}
