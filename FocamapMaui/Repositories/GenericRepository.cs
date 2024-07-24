using FocamapMaui.Controls;
using SQLite;

namespace FocamapMaui.Repositories
{
    public class GenericRepository<T> where T : new()
    {
		private readonly SQLiteAsyncConnection _dbConnection;

		public GenericRepository()
		{
			_dbConnection = new SQLiteAsyncConnection(GetDbPath());
			_dbConnection.CreateTableAsync<T>();
        }

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbConnection.Table<T>().ToListAsync();
		}

		public async Task<int> SaveAsync(T model)
		{
			return await _dbConnection.InsertAsync(model);
		}

        public async Task<int> UpdateAsync(T model)
		{
            return await _dbConnection.UpdateAsync(model);
        }

        public async Task<int>DeleteAsync(T model)
        {
            return await _dbConnection.DeleteAsync(model);
        }

        private static string GetDbPath()
        {
            return ControlFiles.GetValueFromFilePropertyJson("app-config.json", StringConstants.APP_CONFIG, StringConstants.DB_PATH);
        }
    }
}