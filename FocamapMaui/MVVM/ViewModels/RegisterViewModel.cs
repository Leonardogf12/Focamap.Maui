using FocamapMaui.MVVM.Base;

namespace FocamapMaui.MVVM.ViewModels
{
    public class RegisterViewModel : ViewModelBase
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
       
        private object _selectedRegion;
        public object SelectedRegion
        {
            get => _selectedRegion;
            set
            {
                _selectedRegion = value;
                OnPropertyChanged();
            }
        }
        
        #endregion

        public RegisterViewModel()
		{
            LoadRegionListMock();
		}    
    }
}

