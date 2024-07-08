using System.Windows.Input;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.Services.Authentication;

namespace FocamapMaui.MVVM.ViewModels
{
    public class LoginViewModel : ViewModelBase
	{
        #region Properties

        private readonly IAuthenticationService _authenticationService;

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
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

        private bool _allOkToLogin;
        public bool AllOkToLogin
        {
            get => _allOkToLogin;
            set
            {
                _allOkToLogin = value;
                OnPropertyChanged();
            }
        }
        

        #endregion

        public LoginViewModel(IAuthenticationService authenticationService)
		{
            _authenticationService = authenticationService;           
        }

        public async Task Login()
        {
            IsBusy = true;

            try
            {               
                if (CheckIfInputsAreOk())
                {
                    await _authenticationService.LoginAsync(Email, Password);

                    return;
                }

                await App.Current.MainPage.DisplayAlert("Ops", "Preencha corretamente todos os campos", "OK");
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

        public bool CheckIfInputsAreOk()
        {
            var HasOk = true;

            if(string.IsNullOrEmpty(Email) || !Email.Contains("@"))
            {
                BorderColorEmailInput = GetBorderColorErrorToInput();
                HasOk = false;
            }
            else
            {
                BorderColorEmailInput = Colors.Transparent;
            }

            if (string.IsNullOrEmpty(Password) || Password.Length < 6)
            {
                BorderColorPasswordInput = GetBorderColorErrorToInput();
                HasOk = false;
            }
            else
            {
                BorderColorPasswordInput = Colors.Transparent;
            }

            return HasOk;
        }

        private static Color GetBorderColorErrorToInput() => ControlResources.GetResource<Color>("CLErrorBorderColor");
    }
}

