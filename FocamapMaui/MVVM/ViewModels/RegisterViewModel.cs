using FocamapMaui.Controls.Resources;
using FocamapMaui.Helpers.Models;
using FocamapMaui.Models.Map;
using FocamapMaui.MVVM.Base;
using FocamapMaui.Services.Authentication;

namespace FocamapMaui.MVVM.ViewModels
{
    public class RegisterViewModel : ViewModelBase
	{
        #region Properties

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
        
        private string _repeatPassword;
        public string RepeatPassword
        {
            get => _repeatPassword;
            set
            {
                _repeatPassword = value;
                OnPropertyChanged();
            }
        }
       
        private City _selectedCity;
        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
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

        private Color _borderColorNameInput = Colors.Transparent;
        public Color BorderColorNameInput
        {
            get => _borderColorNameInput;
            set
            {
                _borderColorNameInput = value;
                OnPropertyChanged();
            }
        }

        private Color _borderColorEmailInput = Colors.Transparent;
        public Color BorderColorEmailInput
        {
            get => _borderColorEmailInput;
            set
            {
                _borderColorEmailInput = value;
                OnPropertyChanged();
            }
        }

        private Color _borderColorPasswordInput = Colors.Transparent;
        public Color BorderColorPasswordInput
        {
            get => _borderColorPasswordInput;
            set
            {
                _borderColorPasswordInput = value;
                OnPropertyChanged();
            }
        }

        private Color _borderColorRePasswordInput = Colors.Transparent;
        public Color BorderColorRePasswordInput
        {
            get => _borderColorRePasswordInput;
            set
            {
                _borderColorRePasswordInput = value;
                OnPropertyChanged();
            }
        }

        private Color _borderColorRegionInput = Colors.Transparent;
        public Color BorderColorRegionInput
        {
            get => _borderColorRegionInput;
            set
            {
                _borderColorRegionInput = value;
                OnPropertyChanged();
            }
        }

        private readonly IAuthenticationService _authenticationService;

        #endregion

        public RegisterViewModel(IAuthenticationService authenticationService)
		{
            _authenticationService = authenticationService;
		}

        #region Public Methods

        public async Task RegisterNewUser()
        {
            IsBusy = true;

            try
            {
                if (CheckIfInputsAreOk())
                {
                    await _authenticationService.RegisterNewUserAsync(Name, Email, Password, SelectedCity);

                    return;
                }

                await App.Current.MainPage.DisplayAlert("Atenção", "Preencha corretamente todos os campos.", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void LoadCities()
        {
            Cities = CitiesOfEs.GetCitiesOfEspiritoSanto();
        }

        public bool CheckIfInputsAreOk()
        {
            var HasOk = true;

            if (!ValidateNameInput()
            || !ValidateEmailInput()
            || !ValidatePasswordInput()
            || !ValidateRepeatPasswordInput()
            || !ValidateRegionInput())
            {
                HasOk = false;
            }

            return HasOk;
        }

        public bool ValidateNameInput()
        {
            if (string.IsNullOrEmpty(Name) || Name.Length < 3)
            {
                BorderColorNameInput = ControlResources.GetResource<Color>("CLErrorBorderColor");
                return false;
            }

            BorderColorNameInput = Colors.Transparent;

            return true;
        }

        public bool ValidateEmailInput()
        {
            if (string.IsNullOrEmpty(Email) || !Email.Contains('@') || Email.Length < 7)
            {
                BorderColorEmailInput = ControlResources.GetResource<Color>("CLErrorBorderColor");
                return false;
            }

            BorderColorEmailInput = Colors.Transparent;

            return true;
        }

        public bool ValidatePasswordInput()
        {
            if (string.IsNullOrEmpty(Password) || Password.Length < 6)
            {
                BorderColorPasswordInput = ControlResources.GetResource<Color>("CLErrorBorderColor");
                return false;
            }

            BorderColorPasswordInput = Colors.Transparent;

            return true;
        }

        public bool ValidateRepeatPasswordInput()
        {
            if (string.IsNullOrEmpty(RepeatPassword) || RepeatPassword.Length < 6 || !(RepeatPassword == Password))
            {
                BorderColorRePasswordInput = ControlResources.GetResource<Color>("CLErrorBorderColor");
                return false;
            }

            BorderColorRePasswordInput = Colors.Transparent;

            return true;
        }

        #endregion

        #region Private Methods

        private bool ValidateRegionInput()
        {
            if (SelectedCity == null)
            {
                BorderColorRegionInput = ControlResources.GetResource<Color>("CLErrorBorderColor");
                return false;
            }

            BorderColorRegionInput = Colors.Transparent;

            return true;
        }

        #endregion
    }
}