namespace RecipeBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeOnly CookingTime { get; set; }
    }
}
