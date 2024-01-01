using RecipeBook.Models;

namespace RecipeBook.Data.DTOs
{
    public class StepDTO
    {
        public int Id { get; set; }
        public int PositionInRecipe { get; set; }
        public string Instruction { get; set; } = "";

        public virtual int RecipeId { get; set; }
    }
}
