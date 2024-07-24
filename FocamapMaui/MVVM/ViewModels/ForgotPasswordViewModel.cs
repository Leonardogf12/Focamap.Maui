using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.Services.Authentication;

namespace FocamapMaui.MVVM.ViewModels
{
    public class ForgotPasswordViewModel : ViewModelBase
	{
        #region Properties
        
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

        private readonly IAuthenticationService _authenticationService;

        #endregion

        public ForgotPasswordViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        #region Public Methods

        public async Task ResetPassword()
        {          
            if (CheckIfInputsAreOk())
            {
                await _authenticationService.ResetPasswordAsync(Email);
                
                return;
            }

            await App.Current.MainPage.DisplayAlert("Atenção", "Preencha corretamente o email utilizado no cadastro.", "OK");
        }

        #endregion

        #region Private Methods

        private bool CheckIfInputsAreOk()
        {
            var HasOk = true;

            if (string.IsNullOrEmpty(Email) || !Email.Contains("@"))
            {
                BorderColorEmailInput = ControlResources.GetResource<Color>("CLErrorBorderColor");
                HasOk = false;
            }
            else
            {
               BorderColorEmailInput = Colors.Transparent;
            }            

            return HasOk;
        }

        #endregion
    }
}