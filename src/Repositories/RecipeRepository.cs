using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Data.DTOs;
using RecipeBook.Models;
//using RecipeBook.Data.DTOs

namespace RecipeBook.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ILogger<RecipeRepository> _logger;
        private readonly RecipeBookContext _context;
        private readonly IMapper _mapper;

        public RecipeRepository(ILogger<RecipeRepository> logger,
            RecipeBookContext context,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public List<RecipeDTO> GetAll()
        {
            _logger.LogInformation("Getting All Recipes - {time}", DateTime.Now);

            var allRecipes = _context.Recipes.Include(r => r.Ingredients).ToList();
            var allRecipeDTOs = _mapper.Map<List<RecipeDTO>>(allRecipes);

            return allRecipeDTOs;
        }

        public RecipeDTO? GetIfExists(int id)
        {
            _logger.LogInformation("Get Recipe: {id} (If It Exists) - {time}", id, DateTime.Now);

            var recipe = _context.Find<Recipe>(id);
            if (recipe == null)
            {
                return null;
            }

            RecipeDTO recipeDTO = new()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                CookingTimeMins = recipe.CookingTimeMins
            };

            return recipeDTO;
        }

        public void AddRecipe(RecipeDTO recipe)
        {
            _logger.LogInformation("Adding New Recipe: '{name}'  - {time}", recipe.Name, DateTime.Now);

            Recipe newRecipe = new()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                CookingTimeMins = recipe.CookingTimeMins,
                Ingredients = new List<Ingredient>()
            };

            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();
        }

        public void UpdateRecipe(RecipeDTO recipe)
        {
            _logger.LogInformation("Updating Recipe: {id}  - {time}", recipe.Id, DateTime.Now);

            var updatedRecipe = _context.Find<Recipe>(recipe.Id);
            if (updatedRecipe != null)
            {
                updatedRecipe.Name = recipe.Name;
                updatedRecipe.CookingTimeMins = recipe.CookingTimeMins;

                _context.Entry(updatedRecipe).State = EntityState.Modified;
            }
            
            _context.SaveChanges();
        }

        public void DeleteRecipe(int id)
        {
            _logger.LogInformation("Deleting Recipe: {id} - {time}", id, DateTime.Now);

            var recipe = _context.Recipes.Find(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
            }
        }
    }
}
