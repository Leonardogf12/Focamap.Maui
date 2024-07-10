using Firebase.Auth;
using FocamapMaui.Controls;
using FocamapMaui.Controls.Connections;
using FocamapMaui.Models;
using FocamapMaui.MVVM.Models;
using FocamapMaui.Repositories;

namespace FocamapMaui.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserRepository _userRepository;

        public AuthenticationService()
        {
            _userRepository = new();
        }

        public async Task LoginAsync(string email, string password)
        {
            try
            {
                if (!ToastFailConnection.CheckIfConnectionIsSuccessful())
                {
                    ToastFailConnection.ShowToastMessageFailConnection();

                    return;
                }

                var authProvider = GetFirebaseAuthProvider();
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                var content = await auth.GetFreshAuthAsync();

                SaveKeysOnPreferences(content);

                ControlUsers.SetLocalIdByUserLogged();

                await Shell.Current.GoToAsync(StringConstants.HOMEMAPVIEW_ROUTE);
            }
            catch (FirebaseAuthException f)
            {
                if (f.ResponseData.Contains(StringConstants.INVALID_LOGIN_CREDENTIALS))
                {
                    await App.Current.MainPage.DisplayAlert("Ops", "Email ou senha inválidos. Favor verificar.", "Ok");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                await App.Current.MainPage.DisplayAlert("Ops", "Ocorreu um erro inesperado ao tentar realizar login. Tente novamente em alguns instantes.", "Ok");
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

                //TODO - Save User and City on SqLite. Create repository.             
                //SaveCityKeyOnPreferences(city); // remove after implemeted repository.
                await SaveUserOnDevice(name, email, city, content);

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

        public async Task<string> UpdateUserProfile(string email, string password, string newName)
        {
            string result = StringConstants.OK;

            try
            {
                var authProvider = GetFirebaseAuthProvider();

                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

                await auth.UpdateProfileAsync(newName, string.Empty);

                var content = await auth.GetFreshAuthAsync();
               
                UpdateKeyFirebaseUserLogged(content);
                
                await App.Current.MainPage.DisplayAlert("Sucesso", "O nome do Usuário foi alterado com sucesso!", "Ok");

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
            return new FirebaseAuthProvider(new FirebaseConfig(StringConstants.FIREBASE_AUTH_PROVIDER_KEY));
        }

        private async Task SaveUserOnDevice(string name, string email, City city, FirebaseAuthLink firebase)
        {
            try
            {
                var user = new UserModel
                {
                    LocalIdFirebase = firebase.User.LocalId,
                    Name = name,
                    Email = email,
                    State = city.State,
                    City = city.Name
                };

                if (await _userRepository.SaveAsync(user) > 0)
                    Console.WriteLine("Novo registro em UserModel incluído com sucesso");
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
            UpdateKeyFirebaseAuthTokenKey(content);

            UpdateKeyFirebaseUserLocalIdKey(content);

            UpdateKeyFirebaseUserLogged(content);

            UpdateKeyFirebaseUserEmail(content);
        }

        private static void SaveCityKeyOnPreferences(City city)
        {
            ControlPreferences.AddKeyObjectOnPreferences(key: StringConstants.CITY, contentOfObject: city);
        }

        private static void UpdateKeyFirebaseUserEmail(FirebaseAuthLink content)
        {
            ControlPreferences.UpdateKeyFromPreference(key: StringConstants.FIREBASE_USER_EMAIL, valueString: content.User.Email);
        }

        private static void UpdateKeyFirebaseUserLogged(FirebaseAuthLink content)
        {
            ControlPreferences.UpdateKeyFromPreference(key: StringConstants.FIREBASE_USER_LOGGED, valueString: content.User.DisplayName);
        }

        private static void UpdateKeyFirebaseAuthTokenKey(FirebaseAuthLink content)
        {
            ControlPreferences.UpdateKeyFromPreference(key: StringConstants.FIREBASE_AUTH_TOKEN_KEY, valueString: "", contentObject: content);
        }

        private static void UpdateKeyFirebaseUserLocalIdKey(FirebaseAuthLink content)
        {
            ControlPreferences.UpdateKeyFromPreference(key: StringConstants.FIREBASE_USER_LOCAL_ID_KEY, valueString: content.User.LocalId);
        }

        #endregion
    }
}

