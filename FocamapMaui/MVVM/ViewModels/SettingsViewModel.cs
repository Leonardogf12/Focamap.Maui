using FocamapMaui.Controls;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.Views;
using FocamapMaui.Services.Navigation;

namespace FocamapMaui.MVVM.ViewModels
{
    public class SettingsViewModel : ViewModelBase
	{
        private readonly INavigationService _navigationService;

        public SettingsViewModel(INavigationService navigationService)
		{
            _navigationService = navigationService;
        }

		public async Task LogOff()
		{
            var result = await App.Current.MainPage.DisplayAlert("Sair", "Deseja realmente deslogar sua conta?", "Sim", "Cancelar");

            if (!result) return;

            RemoveUserKeysOfPreferencesForLogOff();

            await _navigationService.NavigationPopToRoot();
                      
            await _navigationService.NavigationWithRoute(StringConstants.LOGINVIEW_ROUTE);           
        }

        public async Task GotoUserDetailView()
        {
            await _navigationService.NavigationWithParameter<UserDetailView>();
        }

        private static void RemoveUserKeysOfPreferencesForLogOff()
        {
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_AUTH_TOKEN_KEY);
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_USER_LOCAL_ID_KEY);
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_USER_DISPLAY_NAME);
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.CITY);
        }
    }
}