using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeekMenu.Client.HelperClasses;
using WeekMenu.Client.Models;
using WeekMenu.Client.Services;

namespace WeekMenu.Client.Components
{
    public class Weekhelper
    {
        DateTime today = DateTime.Now;
        List<DayMenuModel> weekMenu = new List<DayMenuModel>();
        private readonly IDayMenuService _dayService;
        private readonly IRecipeService _recipeService;
        private readonly DayMenuSetter _daySetter;

        public Weekhelper(IDayMenuService dayService, IRecipeService recipeService, DayMenuSetter daySetter)
        {
            _dayService = dayService;
            _recipeService = recipeService;
            _daySetter = daySetter;
        }

        DateTime ReturnMonday(DateTime date)
        {
            var today = (int)DateTime.Now.DayOfWeek;
            if (today == 0)
            {
                return DateTime.Now.AddDays(-6);
            }
            else
            {
                return DateTime.Now.AddDays(-today+1);
            }
        }


        public async Task<List<DayMenuModel>>CreateSevenDays(DateTime date)
        {
            List<DayMenuModel> output = new List<DayMenuModel>();

            date = ReturnMonday(date);
            
            for (int i = 0; i < 7; i++)
            {
                var day = await _daySetter.GetDayMenuAsync(date.AddDays(i));
                
                output.Add(day);
            }

            return output;
        }
    }
}
