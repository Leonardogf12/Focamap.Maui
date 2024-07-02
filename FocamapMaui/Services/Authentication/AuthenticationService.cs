using Firebase.Auth;
using FocamapMaui.Controls;
using FocamapMaui.Controls.Connections;

namespace FocamapMaui.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {		
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

                await Shell.Current.GoToAsync("//HomeMapView");
            }
            catch (FirebaseAuthException f)
            {
                if (f.ResponseData.Contains("INVALID_LOGIN_CREDENTIALS"))
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
        
        public async Task RegisterNewUserAsync(string name, string email, string password)
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

                await App.Current.MainPage.DisplayAlert("Sucesso", "Usuário registrado com sucesso!", "Voltar");

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

        private static FirebaseAuthProvider GetFirebaseAuthProvider()
        {
            return new FirebaseAuthProvider(new FirebaseConfig(StringConstants.FIREBASE_AUTH_PROVIDER_KEY));
        }

        private static void SaveKeysOnPreferences(FirebaseAuthLink contentByUserLogged)
        {
            ControlPreferences.AddKeyOnPreferences(key: StringConstants.FIREBASE_AUTH_TOKEN_KEY, contentOfObject: contentByUserLogged);
            ControlPreferences.AddKeyOnPreferences(key: StringConstants.FIREBASE_USER_LOCAL_ID_KEY, contentOfObject: contentByUserLogged.User.LocalId);
        }
    }
}

