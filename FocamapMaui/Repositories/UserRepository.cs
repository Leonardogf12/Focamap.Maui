﻿using FocamapMaui.Controls;
using FocamapMaui.MVVM.Models;
using SQLite;

namespace FocamapMaui.Repositories
{
    public class UserRepository : GenericRepository<UserModel>
	{
		private readonly SQLiteAsyncConnection _dbConnection;

		public UserRepository()
		{           
            _dbConnection = new SQLiteAsyncConnection(GetDbPath());
        }
        
        public async Task<UserModel> GetByLocalIdFirebase(string localIdFirebase)
        {
            return await _dbConnection.Table<UserModel>().Where(x => x.LocalIdFirebase == localIdFirebase).FirstAsync();
        }

        private static string GetDbPath()
        {
            return ControlFiles.GetValueFromFilePropertyJson("app-config.json", StringConstants.APP_CONFIG, StringConstants.DB_PATH);
        }
    }
}