using Firebase.Database;
using Firebase.Database.Query;
using FocamapMaui.Controls;

namespace FocamapMaui.Services.Firebase
{
    public class RealtimeDatabaseService : IRealtimeDatabaseService
    {
        private readonly FirebaseClient _client;

        public RealtimeDatabaseService()
        {
            _client = new(StringConstants.FIREBASE_REALTIME_DATABASE);
        }

        public async Task<List<T>> GetAllAsync<T>(string nameChild) where T : new()
        {
            try
            {               
                var listOfTs = await _client.Child(nameChild).OnceAsync<T>();

                await Task.Delay(1000);

                if (listOfTs.Count == 0) return new List<T>();

                return listOfTs.Select(x => x.Object).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<T>();
            }            
        }

        public async Task SaveAsync<T>(string nameChild, T model) where T : new()
        {
            try
            {
                await _client.Child(nameChild).PostAsync(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }
    }
}

