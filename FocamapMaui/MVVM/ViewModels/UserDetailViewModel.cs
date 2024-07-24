using FocamapMaui.Controls;
using FocamapMaui.Controls.Resources;
using FocamapMaui.Helpers.Models;
using FocamapMaui.Models.Map;
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

        private List<City> _cities;
        public List<City> Cities
        {
            get => _cities;
            set
            {
                _cities = value;
                OnPropertyChanged();
            }
        }

        private Color _borderColorDisplayName = Colors.Transparent;
        public Color BorderColorDisplayName
        {
            get => _borderColorDisplayName;
            set
            {
                _borderColorDisplayName = value;
                OnPropertyChanged();
            }
        }

        private Color _borderColorPassword = Colors.Transparent;
        public Color BorderColorPassword
        {
            get => _borderColorPassword;
            set
            {
                _borderColorPassword = value;
                OnPropertyChanged();
            }
        }

        private readonly IAuthenticationService _authenticationService;

        public string OldNameUser = string.Empty;

        #endregion

        public UserDetailViewModel(IAuthenticationService authenticationService)
		{
            _authenticationService = authenticationService;
            
            UpdateUserInformations();            
        }

        #region Private Methods

        private async void UpdateUserInformations()
        {
            GetNewValuesForNameInput();
          
            if (!string.IsNullOrEmpty(DisplayName))
            {
                Password = string.Empty;
                OldNameUser = DisplayName;
                LetterUserName = DisplayName[0].ToString();
                return;
            }

            DisplayName = OldNameUser;

            await App.Current.MainPage.DisplayAlert("Ops", "Parece que ocorreu uma falha quando tentava alterar o nome de usuário. Por favor, tente novamente.", "OK");
        }

        private void GetNewValuesForNameInput()
        {
            Email = ControlPreferences.GetKeyOfPreferences(StringConstants.FIREBASE_USER_EMAIL);
            DisplayName = ControlPreferences.GetKeyOfPreferences(StringConstants.FIREBASE_USER_DISPLAY_NAME);
        }

        private bool ValidateNameInput(bool hasOk)
        {
            if (string.IsNullOrEmpty(Name) || Name.Length < 3)
            {
                BorderColorDisplayName = ControlResources.GetResource<Color>("CLErrorBorderColor");
                hasOk = false;
            }
            else
            {
                BorderColorDisplayName = Colors.Transparent;
            }

            return hasOk;
        }

        private bool ValidatePasswordInput(bool hasOk)
        {
            if (string.IsNullOrEmpty(Password) || Name.Length < 3)
            {
                BorderColorPassword = ControlResources.GetResource<Color>("CLErrorBorderColor");
                hasOk = false;
            }
            else
            {
                BorderColorPassword = Colors.Transparent;
            }

            return hasOk;
        }

        private void GetUserDisplayName()
        {
            OldNameUser = ControlPreferences.GetKeyOfPreferences(StringConstants.FIREBASE_USER_DISPLAY_NAME);
        }

        #endregion

        #region Public Methods

        public async Task UpdateProfileUser()
        {
            /*
            try
            {
                if (CheckIfInputsAreOk())
                {
                    GetUserDisplayName();
                    
                    var result = await _authenticationService.UpdateUserProfile(Email, Password, Name);

                    if (result.Equals(StringConstants.OK))
                    {
                        SetsValueForIsEnabledInputs(false);

                        UpdateUserInformations();

                        return;
                    }                   
                }

                await App.Current.MainPage.DisplayAlert("Atenção", "Preencha corretamente todos os campos.", "OK");                              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            */
        }

        public void LoadCities()
        {
            Cities = CitiesOfEs.GetCitiesOfEspiritoSanto();
        }

        public void SetsValueForIsEnabledInputs(bool isEnabled)
        {
            IsEnabledDisplayName = isEnabled;
            IsEnabledPassword = isEnabled;          
        }

        public bool CheckIfInputsAreOk()
        {
            var HasOk = true;

            if (!ValidateNameInput(HasOk) || !ValidatePasswordInput(HasOk))
            {
                HasOk = false;
            }

            return HasOk;
        }

        #endregion
    }
}