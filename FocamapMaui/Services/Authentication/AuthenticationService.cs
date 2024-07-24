using Firebase.Auth;
using FocamapMaui.Controls;
using FocamapMaui.Controls.Connections;
using FocamapMaui.Models.Map;
using FocamapMaui.MVVM.Models;
using FocamapMaui.Services.Firebase;

namespace FocamapMaui.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {    
        private readonly IRealtimeDatabaseService _realtimeDatabaseService;

        public AuthenticationService(IRealtimeDatabaseService realtimeDatabaseService)
        {     
            _realtimeDatabaseService = realtimeDatabaseService;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            bool allOk = true;

            try
            {
                if (!ToastFailConnection.CheckIfConnectionIsSuccessful())
                {
                    ToastFailConnection.ShowToastMessageFailConnection();

                    allOk = false;

                    return allOk;
                }

                var authProvider = GetFirebaseAuthProvider();
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                var content = await auth.GetFreshAuthAsync();

                await UpdateUserLoggedKey(content.User.LocalId);

                SaveKeysOnPreferences(content);
              
                ControlUsers.SetLocalIdByUserLogged();

                return allOk;              
            }
            catch (FirebaseAuthException f)
            {
                if (f.ResponseData.Contains(StringConstants.INVALID_LOGIN_CREDENTIALS))
                {
                    await App.Current.MainPage.DisplayAlert("Ops", "Email ou senha inválidos. Favor verificar.", "Ok");                   
                }

                if (f.ResponseData.Contains("400"))
                {
                    await App.Current.MainPage.DisplayAlert("Ops", "Não foi possível realizar login neste momento. Favor, tente novamente em alguns instantes.", "Ok");
                }

                allOk = false;
                return allOk;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                await App.Current.MainPage.DisplayAlert("Ops", "Ocorreu um erro inesperado ao tentar realizar login. Tente novamente em alguns instantes.", "Ok");

                allOk = false;
                return allOk;
            }
        }

        public async Task RegisterNewUserAsync(string name, string email, string password, City city)
        { 
            try
            {
                if (!ToastFailConnection.CheckIfConnectionIsSuccessful())
                {
                    ToastFailConnection.ShowToastMessageFailConnection();

                    return;
                }

                var authProvider = GetFirebaseAuthProvider();

                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);

                await auth.UpdateProfileAsync(name, string.Empty);

                var content = await auth.GetFreshAuthAsync();
            
                await SaveUserOnFirebaseAuth(name, email, city, content);

                await App.Current.MainPage.DisplayAlert("Sucesso", "Usuário registrado com sucesso!", "Ok");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                await App.Current.MainPage.DisplayAlert("Ops", "Ocorreu um erro inesperado ao tentar registrar um novo usuário. Tente novamente em alguns instantes.", "Ok");
            }            
        }        

        public async Task ResetPasswordAsync(string email)
        {
            try
            {
                if (!ToastFailConnection.CheckIfConnectionIsSuccessful())
                {
                    ToastFailConnection.ShowToastMessageFailConnection();

                    return;
                }

                var authProvider = GetFirebaseAuthProvider();

                await authProvider.SendPasswordResetEmailAsync(email);

                await App.Current.MainPage.DisplayAlert("Sucesso", $"Enviamos um email para ({email}) com as instruções para redefinir a senha.", "Ok");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                await App.Current.MainPage.DisplayAlert("Ops", "Ocorreu um erro inesperado ao tentar redefinir nova senha. Tente novamente em alguns instantes.", "Ok");
            }
        }

        public async Task<string> UpdateUserProfile(string email, string password, string newName, City city)
        {
            string result = StringConstants.OK;

            try
            {
                var authProvider = GetFirebaseAuthProvider();

                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

                await auth.UpdateProfileAsync(newName, string.Empty);

                var content = await auth.GetFreshAuthAsync();
              
                await UpdateUserOnFirebaseAuth(newName, email, city, content);
                
                return result;
            }
            catch (FirebaseAuthException f)
            {
                if (f.ResponseData.Contains(StringConstants.INVALID_LOGIN_CREDENTIALS))
                {
                    await App.Current.MainPage.DisplayAlert("Ops", "Senha inválida. Favor verificar.", "Ok");
                    
                    return StringConstants.INVALID_LOGIN_CREDENTIALS;
                }

                return StringConstants.FIREBASE_AUTH_EXCEPTION;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Ops", "Ocorreu um erro inesperado ao tentar editar o nome de usuário.Tente novamente em alguns instantes.", "Ok");

                return StringConstants.EXCEPTION;
            }
        }
       
        #region Others

        private static FirebaseAuthProvider GetFirebaseAuthProvider()
        {
            var authProviderKey = ControlFiles.GetValueFromFilePropertyJson("firebase-config.json", StringConstants.FIREBASE, StringConstants.FIREBASE_AUTH_PROVIDER_KEY);

            return new FirebaseAuthProvider(new FirebaseConfig(authProviderKey));
        }

        private async Task SaveUserOnFirebaseAuth(string name, string email, City city, FirebaseAuthLink firebase)
        {
            try
            {
                var user = new UserModel
                {
                    LocalIdFirebase = firebase.User.LocalId,
                    Name = name,
                    Email = email,
                    City = city
                };

                await _realtimeDatabaseService.SaveAsync(nameof(UserModel), user);

                Console.WriteLine("Novo registro em UserModel incluído com sucesso");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task UpdateUserOnFirebaseAuth(string name, string email, City city, FirebaseAuthLink content)
        {
            try
            {                
                UserModel user = new()
                {                    
                    LocalIdFirebase = content.User.LocalId,
                    Name = name,
                    Email = email,
                    City = city
                };

                await _realtimeDatabaseService.UpdateAsync(user.LocalIdFirebase, nameof(UserModel), user);

                await UpdateUserLoggedKey(content.User.LocalId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

        private async Task UpdateUserLoggedKey(string localId)
        {
            try
            {
                var user = await _realtimeDatabaseService.GetUserByFirebaseLocalId<UserModel>(child: nameof(UserModel), firebaseLocalId: localId);

                if (user is not null)
                {
                    UpdateFirebaseUserLoggedKey(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Preferences

        private static void SaveKeysOnPreferences(FirebaseAuthLink content)
        {
            UpdateFirebaseAuthTokenKey(content);

            UpdateFirebaseUserLocalIdKey(content);

            UpdateFirebaseUserDisplayNameKey(content);

            UpdateFirebaseUserEmailKey(content);            
        }
       
        private static void UpdateFirebaseAuthTokenKey(FirebaseAuthLink content)
        {
            ControlPreferences.UpdateKeyFromPreference(key: StringConstants.FIREBASE_AUTH_TOKEN_KEY, valueString: "", contentObject: content);
        }

        private static void UpdateFirebaseUserEmailKey(FirebaseAuthLink content)
        {
            ControlPreferences.UpdateKeyFromPreference(key: StringConstants.FIREBASE_USER_EMAIL, valueString: content.User.Email);
        }

        private static void UpdateFirebaseUserDisplayNameKey(FirebaseAuthLink content)
        {
            ControlPreferences.UpdateKeyFromPreference(key: StringConstants.FIREBASE_USER_DISPLAY_NAME, valueString: content.User.DisplayName);
        }

        private static void UpdateFirebaseUserLocalIdKey(FirebaseAuthLink content)
        {
            ControlPreferences.UpdateKeyFromPreference(key: StringConstants.FIREBASE_USER_LOCAL_ID_KEY, valueString: content.User.LocalId);
        }

        private static void UpdateFirebaseUserLoggedKey(UserModel user)
        {
            ControlPreferences.UpdateKeyFromPreference(key: StringConstants.FIREBASE_USER_LOGGED_KEY, valueString: "", contentObject: user);
        }
       
        #endregion
    }
}