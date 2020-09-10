using System;

namespace WeekMenu.Client.Models
{
    public class DayMenuModel
    {
        public int DayMenuModelId { get; set; }
        public RecipeModel Breakfast { get; set; }
        public RecipeModel SecondBreakfast { get; set; }
        public RecipeModel Lunch { get; set; }
        public RecipeModel AfternoonTea { get; set; }
        public RecipeModel Dinner { get; set; }
        public DateTime DayMenuDate { get; set; }
        public string DayMenuOwner { get; set; }
    }
}
