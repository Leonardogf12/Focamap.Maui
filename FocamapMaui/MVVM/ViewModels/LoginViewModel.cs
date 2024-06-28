using FocamapMaui.MVVM.Base;

namespace FocamapMaui.MVVM.ViewModels
{
    public class LoginViewModel : ViewModelBase
	{
        #region Properties

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

        #endregion

        public LoginViewModel()
		{
		}
	}
}

