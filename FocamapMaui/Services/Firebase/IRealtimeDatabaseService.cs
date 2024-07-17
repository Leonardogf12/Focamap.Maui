using FocamapMaui.Models.Map;
using FocamapMaui.MVVM.Models;

namespace FocamapMaui.Services.Firebase
{
    public interface IRealtimeDatabaseService
	{
        Task<List<T>> GetAllAsync<T>(string nameChild) where T : new();

        Task SaveAsync<T>(string nameChild, T model) where T : new();

        Task<List<T>> GetByRegion<T>(string firstChild, string firstOrderBy, string firstEqualTo) where T : new();
    }
}

