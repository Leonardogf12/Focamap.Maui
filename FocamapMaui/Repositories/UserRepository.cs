using FocamapMaui.Controls;
using FocamapMaui.MVVM.Models;
using SQLite;

namespace FocamapMaui.Repositories
{
    public class UserRepository : GenericRepository<UserModel>
	{
		private readonly SQLiteAsyncConnection _dbConnection;

		public UserRepository()
		{
			_dbConnection = new SQLiteAsyncConnection(StringConstants.DB_PATH);
        }

		public async Task<UserModel> GetById(int id)
		{
            return await _dbConnection.Table<UserModel>().Where(x => x.Id == id).FirstAsync();
		}

        public async Task<UserModel> GetByLocalIdFirebase(string localIdFirebase)
        {
            return await _dbConnection.Table<UserModel>().Where(x => x.LocalIdFirebase == localIdFirebase).FirstAsync();
        }
    }
}

