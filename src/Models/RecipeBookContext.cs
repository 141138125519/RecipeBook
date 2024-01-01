using Microsoft.EntityFrameworkCore;

namespace RecipeBook.Models
{
    public class RecipeBookContext : DbContext
    {
        private ILogger<RecipeBookContext> _logger;

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Step> Steps { get; set; }

        public string DbPath { get; }

        public RecipeBookContext(ILogger<RecipeBookContext> logger)
        {
            _logger = logger;

            _logger.LogInformation("Recipe Context Started {now}", DateTime.Now);

            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "recipeBook.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");

    }
}
