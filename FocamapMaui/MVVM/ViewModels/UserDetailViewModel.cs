using FocamapMaui.Controls;
using FocamapMaui.MVVM.Base;
using FocamapMaui.Services.Authentication;

namespace FocamapMaui.MVVM.ViewModels
{
    public class UserDetailViewModel : ViewModelBase
	{
        #region Properties
        
        private string _letterUserName;
        public string LetterUserName
        {
            get => _letterUserName;
            set
            {
                _letterUserName = value;
                OnPropertyChanged();
            }
        }

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

                Name = value;
            }
        }

        private bool _isEnabledDisplayName;
        public bool IsEnabledDisplayName
        {
            get => _isEnabledDisplayName;
            set
            {
                _isEnabledDisplayName = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabledEmail;
        public bool IsEnabledEmail
        {
            get => _isEnabledEmail;
            set
            {
                _isEnabledEmail = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabledPassword;
        public bool IsEnabledPassword
        {
            get => _isEnabledPassword;
            set
            {
                _isEnabledPassword = value;
                OnPropertyChanged();
            }
        }
       
        private bool _isEnabledRegion;
        public bool IsEnabledRegion
        {
            get => _isEnabledRegion;
            set
            {
                _isEnabledRegion = value;
                OnPropertyChanged();
            }
        }

        private readonly IAuthenticationService _authenticationService;

        #endregion

        public UserDetailViewModel(IAuthenticationService authenticationService)
		{
            _authenticationService = authenticationService;

            LoadRegionListMock();

            LoadUserInformations();            
        }

        #region Private Methods

        private void LoadUserInformations()
        {
            try
            {
                Email = ControlPreferences.GetKeyOfPreferences(StringConstants.FIREBASE_USER_EMAIL);
                DisplayName = ControlPreferences.GetKeyOfPreferences(StringConstants.FIREBASE_USER_LOGGED);                
                LetterUserName = DisplayName[0].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        #endregion

        #region Public Methods

        public async Task UpdateProfileUser()
        {
            try
            {
                await _authenticationService.UpdateUserProfile(Email, Password, Name);
                EditUserProfile(false);
                LoadUserInformations();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
               
        }

        public void EditUserProfile(bool isEnabled)
        {
            IsEnabledDisplayName = isEnabled;
            IsEnabledPassword = isEnabled;          
        }

        #endregion
    }
}

