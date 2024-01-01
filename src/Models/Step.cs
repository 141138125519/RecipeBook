namespace RecipeBook.Models
{
    public class Step
    {
        public int Id { get; set; }
        public int PositionInRecipe { get; set; }
        public string Instruction { get; set; } = "";

        public virtual Recipe Recipe { get; set; }
    }
}
