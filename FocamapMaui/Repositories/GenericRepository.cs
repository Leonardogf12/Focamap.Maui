using FocamapMaui.Controls;
using SQLite;

namespace FocamapMaui.Repositories
{
    public class GenericRepository<T> where T : new()
    {
		private readonly SQLiteAsyncConnection _dbConnection;

		public GenericRepository()
		{
			_dbConnection = new SQLiteAsyncConnection(StringConstants.DB_PATH);
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
    }
}

