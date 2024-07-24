using Firebase.Database;
using Firebase.Database.Query;
using FocamapMaui.Controls;
using FocamapMaui.MVVM.Models;

namespace FocamapMaui.Services.Firebase
{
    public class RealtimeDatabaseService : IRealtimeDatabaseService
    {
        private readonly FirebaseClient _client;

        public RealtimeDatabaseService()
        {
            _client = ConfigureFirebaseClient();
        }

        private static FirebaseClient ConfigureFirebaseClient()
        {           
            var authKey = ControlFiles.GetValueFromFilePropertyJson("firebase-config.json", StringConstants.FIREBASE, StringConstants.AUTH_SECRET);

            var firebaseRealtimeDatabase = ControlFiles.GetValueFromFilePropertyJson("firebase-config.json", StringConstants.FIREBASE, StringConstants.FIREBASE_REALTIME_DATABASE);

            return new FirebaseClient(firebaseRealtimeDatabase, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(authKey)
            });
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

        public async Task<List<T>> GetByRegion<T>(string firstChild, string firstOrderBy, string firstEqualTo) where T : new()
        {
            try
            {                
                var query = await _client
                              .Child(firstChild)
                              .OrderBy(firstOrderBy)                             
                              .EqualTo(firstEqualTo)
                              .OnceAsync<T>();

               
                List<T> list = new();

                foreach (var item in query)
                {
                    list.Add(item.Object);                  
                }

                return list;
            }
            catch (FirebaseException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<T>();
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

        public async Task UpdateAsync<T>(string localIdFirebase, string nameChild, T model) where T : new()
        {
            try
            {
                var user = await _client.Child(nameof(UserModel))
                                 .OrderBy("LocalIdFirebase") //todo - refactor
                                 .EqualTo(localIdFirebase)
                                 .LimitToFirst(1)
                                 .OnceAsync<UserModel>(); //todo - refactor

                var internalId = user.FirstOrDefault()?.Key;

                string fullPath = $"{nameChild}/{internalId}";

                await _client.Child(fullPath).PutAsync(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public async Task<T> GetUserByFirebaseLocalId<T>(string child, string firebaseLocalId) where T : class
        {
            try
            {                
                var query = await _client.Child(child)
                                        .OrderBy("LocalIdFirebase") //todo - refactor
                                        .EqualTo(firebaseLocalId)
                                        .LimitToFirst(1)
                                        .OnceAsync<T>();

                if (query is not null)
                {                    
                    return query.First().Object;
                }
                return default;
            }
            catch (FirebaseException ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }                       
        }
    }
}