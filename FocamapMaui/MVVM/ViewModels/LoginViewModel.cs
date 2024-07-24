using FocamapMaui.Controls;
using FocamapMaui.Controls.Resources;
using FocamapMaui.Helpers.Models;
using FocamapMaui.Models.Map;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.Models;
using FocamapMaui.Services.Authentication;

namespace FocamapMaui.MVVM.ViewModels
{
    public class LoginViewModel : ViewModelBase
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
       
        private readonly IAuthenticationService _authenticationService;    

        #endregion

        public LoginViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;            
        }

        #region Private Methods

        private static void SetKeysOfPreferencesFromUser()
        {
            City city = GetCityOfUserLogged();

            if (city is null) return;

            UpdateKeyCity(city);
        }

        private static City GetCityOfUserLogged()
        {           
            var userLogged = ControlPreferences.GetKeyObjectOfPreferences<UserModel>(key: StringConstants.FIREBASE_USER_LOGGED_KEY);

            var cities = CitiesOfEs.GetCitiesOfEspiritoSanto();

            return cities.Where(x => x.Name.Equals(userLogged.City.Name) && x.State.Equals(userLogged.City.State)).FirstOrDefault();
        }

        private static void UpdateKeyCity(City city)
        {
            ControlPreferences.UpdateKeyFromPreference(key: StringConstants.CITY, valueString: null, contentObject: city);          
        }

        private static Color GetBorderColorErrorToInput() => ControlResources.GetResource<Color>("CLErrorBorderColor");

        #endregion

        #region Public Methods
        
        public async Task Login()
        {
            IsBusy = true;

            try
            {               
                if (CheckIfInputsAreOk())
                {
                    var logged = await _authenticationService.LoginAsync(Email, Password);

                    if (logged)
                    {
                        LoginViewModel.SetKeysOfPreferencesFromUser();
                        await Shell.Current.GoToAsync(StringConstants.HOMEMAPVIEW_ROUTE);
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Ops", "Preencha corretamente todos os campos", "OK");
                }               
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

        #endregion
    }
}