using FocamapMaui.Controls;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.Models;
using FocamapMaui.Services.Authentication;

namespace FocamapMaui.MVVM.ViewModels
{
    public class UserDetailViewModel : ViewModelBase
	{
        #region Properties

        private readonly IAuthenticationService _authenticationService;
       
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set
            {
                _displayName = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public UserDetailViewModel(IAuthenticationService authenticationService)
		{
            _authenticationService = authenticationService;

            LoadRegionListMock();
            LoadUserLogged();
        }

        private void LoadUserLogged()
        {           
            DisplayName = ControlPreferences.GetKeyOfPreferences(StringConstants.FIREBASE_USER_LOGGED);
        }

        public async Task UpdateProfileUser()
        {
            //await _authenticationService.UpdateUserProfile(Email, Password, Name);
        }
    }
}

