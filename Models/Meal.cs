using System.Linq;
using SundownBoulevard.DTO;

namespace SundownBoulevard.Models
{
    public class Meal
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public Meal(MealDTO meal)
        {
            Id = meal.meals.First().idMeal;
            Name = meal.meals.First().strMeal;
            Category = meal.meals.First().strCategory;
        }
        
        
    }
}