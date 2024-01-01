namespace RecipeBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int CookingTimeMins { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
    }
}
