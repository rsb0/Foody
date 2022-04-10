namespace Foody.Models
{
    public class FoodItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Brand { get; set; }
        public int? Calories { get; set; }
        public int? Fat { get; set; }
        public int? Carbs { get; set; }
        public int? Protein { get; set; }
    }
}