using System;
using System.Threading.Tasks;
using WeekMenu.Client.Models;

namespace WeekMenu.Client.Services
{
    public interface IDayMenuService
    {
        Task<DayMenuModel> GetDayMenuByDate(DateTime date);
        Task<DayMenuModel> GetDayMenuModel(DateTime date);
        Task<DayMenuModel> SaveDayMenuModel(DayMenuModel dayMenuModel);
        Task UpdateDayMenuModel(DayMenuModel dayMenuModel);
    }
}