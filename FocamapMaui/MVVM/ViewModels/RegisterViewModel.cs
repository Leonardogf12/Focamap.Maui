using FocamapMaui.Controls.Resources;
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
       
        private string _selectedRegion;
        public string SelectedRegion
        {
            get => _selectedRegion;
            set
            {
                _selectedRegion = value;
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

            LoadRegionListMock();
		}

        public async Task RegisterNewUser()
        {
            if (CheckIfInputsAreOk())
            {
                await _authenticationService.RegisterNewUserAsync(Name, Email, Password);

                return;
            }

            await App.Current.MainPage.DisplayAlert("Atenção", "Preencha corretamente todos os campos.", "OK");            
        }

        public bool CheckIfInputsAreOk()
        {
            var HasOk = true;

            if (!ValidateNameInput(HasOk)
            || !ValidateEmailInput(HasOk)
            || !ValidatePasswordInput(HasOk)
            || !ValidateRepeatPasswordInput(HasOk)
            || !ValidateRegionInput(HasOk))
            {
                HasOk = false;
            }
           
            return HasOk;
        }
       
        private bool ValidateNameInput(bool hasOk)
        {
            if (string.IsNullOrEmpty(Name) || Name.Length < 3)
            {
                BorderColorNameInput = ControlResources.GetResource<Color>("CLErrorBorderColor");
                hasOk = false;
            }
            else
            {
                BorderColorNameInput = Colors.Transparent;
            }

            return hasOk;
        }

        private bool ValidateEmailInput(bool hasOk)
        {
            if (string.IsNullOrEmpty(Email) || !Email.Contains('@'))
            {
                BorderColorEmailInput = ControlResources.GetResource<Color>("CLErrorBorderColor");
                hasOk = false;
            }
            else
            {
                BorderColorEmailInput = Colors.Transparent;
            }

            return hasOk;
        }

        private bool ValidateRepeatPasswordInput(bool hasOk)
        {
            if (string.IsNullOrEmpty(RepeatPassword) || RepeatPassword.Length < 6)
            {
                BorderColorRePasswordInput = ControlResources.GetResource<Color>("CLErrorBorderColor");
                hasOk = false;
            }
            else
            {
                BorderColorRePasswordInput = Colors.Transparent;
            }

            return hasOk;
        }

        private bool ValidatePasswordInput(bool hasOk)
        {
            if (string.IsNullOrEmpty(Password) || Password.Length < 6)
            {
                BorderColorPasswordInput = ControlResources.GetResource<Color>("CLErrorBorderColor");
                hasOk = false;
            }
            else
            {
                BorderColorPasswordInput = Colors.Transparent;
            }

            return hasOk;
        }

        private bool ValidateRegionInput(bool hasOk)
        {
            if (string.IsNullOrEmpty(SelectedRegion))
            {
                BorderColorRegionInput = ControlResources.GetResource<Color>("CLErrorBorderColor");
                hasOk = false;
            }
            else
            {
                BorderColorRegionInput = Colors.Transparent;
            }

            return hasOk;
        }
    }
}

