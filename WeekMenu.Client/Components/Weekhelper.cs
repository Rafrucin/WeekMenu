using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Weekhelper(IDayMenuService dayService, IRecipeService recipeService)
        {
            _dayService = dayService;
            _recipeService = recipeService;
        }

        DateTime ReturnMonday(DateTime date)
        {
            var today = (int)DateTime.Now.DayOfWeek;
            if (today == 1)
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
                var day = await CreateDay(date.AddDays(i));
                output.Add(day);
            }

            return output;
        }

        public async Task<DayMenuModel> CreateDay(DateTime date)
        {
            DayMenuModel dayMenu = new DayMenuModel();
            var randoms = await _recipeService.Get5Random();
            dayMenu.DayMenuDate = date;
            dayMenu.Breakfast = randoms[0];
            dayMenu.SecondBreakfast = randoms[1];
            dayMenu.Lunch = randoms[2];
            dayMenu.AfternoonTea = randoms[3];
            dayMenu.Dinner = randoms[4];
            return dayMenu;
        }
    }
}
