namespace FocamapMaui.Services.Firebase
{
    public interface IRealtimeDatabaseService
	{
        Task<List<T>> GetAllAsync<T>(string nameChild) where T : new();

        Task SaveAsync<T>(string nameChild, T model) where T : new();
    }
}

