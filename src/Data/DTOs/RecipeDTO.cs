namespace RecipeBook.Data.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int CookingTimeMins { get; set; }

        public ICollection<IngredientDTO> Ingredients { get; set; }
        public ICollection<StepDTO> Steps { get; set; }
    }
}
