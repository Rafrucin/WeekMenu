using System;
using System.Threading.Tasks;
using WeekMenu.Client.Models;

namespace WeekMenu.Client.Services
{
    public interface IDayMenuService
    {
        Task<DayMenuModel> GetRecipeByDate(DateTime date);
    }
}