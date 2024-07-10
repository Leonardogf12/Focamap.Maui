using System.Collections.ObjectModel;
using System.Windows.Input;
using AndroidX.Lifecycle;
using DevExpress.Maui.Controls;
using FocamapMaui.Controls;
using FocamapMaui.Helpers.Models;
using FocamapMaui.Models;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.Models;
using FocamapMaui.MVVM.Views;
using FocamapMaui.Repositories;
using FocamapMaui.Services.Map;
using FocamapMaui.Services.Navigation;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace FocamapMaui.MVVM.ViewModels
{
    [QueryProperty(nameof(AnonymousAccess), "AnonymousAccess")]
    public class HomeMapViewModel : ViewModelBase
    {
        #region Properties

        private bool _anonymousAccess;
        public bool AnonymousAccess
        {
            get => _anonymousAccess;
            set
            {
                _anonymousAccess = value;
                OnPropertyChanged();

                CheckAnonymousAccess(!AnonymousAccess);
            }
        }

        private ObservableCollection<PinDto> _pinsList;
        public ObservableCollection<PinDto> PinsList
        {
            get => _pinsList;
            set
            {
                _pinsList = value;
                OnPropertyChanged();
            }
        }

        private Grid _mainView;
        public Grid MainView
        {
            get => _mainView;
            set
            {
                _mainView = value;
                OnPropertyChanged();              
            }
        }

        private Map _map;
        public Map Map
        {
            get => _map;
            set
            {
                _map = value;
                OnPropertyChanged();             
            }
        }
       
        private BottomSheetState _bottomSheetAddOccurrenceState = BottomSheetState.Hidden;
        public BottomSheetState BottomSheetAddOccurrenceState
        {
            get => _bottomSheetAddOccurrenceState;
            set
            {
                _bottomSheetAddOccurrenceState = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _lockUnlockImage = ImageSource.FromFile("unlock_24");
        public ImageSource LockUnlockImage
        {
            get => _lockUnlockImage;
            set
            {
                _lockUnlockImage = value;
                OnPropertyChanged();
            }
        }

        private bool _lockUnlockButtonIsEnabled = true;
        public bool LockUnlockButtonIsEnabled
        {
            get => _lockUnlockButtonIsEnabled;
            set
            {
                _lockUnlockButtonIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _detailOccurrenceButtonIsEnabled = true;
        public bool DetailOccurrenceButtonIsEnabled
        {
            get => _detailOccurrenceButtonIsEnabled;
            set
            {
                _detailOccurrenceButtonIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _addOccurrenceButtonIsEnabled = true;
        public bool AddOccurrenceButtonIsEnabled
        {
            get => _addOccurrenceButtonIsEnabled;
            set
            {
                _addOccurrenceButtonIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _userButtonIsEnabled = true;
        public bool UserButtonIsEnabled
        {
            get => _userButtonIsEnabled;
            set
            {
                _userButtonIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _exitButtonIsEnabled = true;
        public bool ExitButtonIsEnabled
        {
            get => _exitButtonIsEnabled;
            set
            {
                _exitButtonIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _mainButtonIsEnabled = true;
        public bool MainButtonIsEnabled
        {
            get => _mainButtonIsEnabled;
            set
            {
                _mainButtonIsEnabled = value;
                OnPropertyChanged();
            }
        }        

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _hour;
        public TimeSpan Hour
        {
            get => _hour;
            set
            {
                _hour = value;
                OnPropertyChanged();
            }
        }

        private string _resume;
        public string Resume
        {
            get => _resume;
            set
            {
                _resume = value;
                OnPropertyChanged();
            }
        }

        private bool _isShowingUser;
        public bool IsShowingUser
        {
            get => _isShowingUser;
            set
            {
                _isShowingUser = value;
                OnPropertyChanged();
            }
        }

        private bool _isVisibleUserFloatButton;
        public bool IsVisibleUserFloatButton
        {
            get => _isVisibleUserFloatButton;
            set
            {
                _isVisibleUserFloatButton = value;
                OnPropertyChanged();
            }
        }

        private bool _isVisibleAddOccurrenceFloatButton;
        public bool IsVisibleAddOccurrenceFloatButton
        {
            get => _isVisibleAddOccurrenceFloatButton;
            set
            {
                _isVisibleAddOccurrenceFloatButton = value;
                OnPropertyChanged();
            }
        }

        private bool _isVisibleDetailOccurrenceFloatButton;
        public bool IsVisibleDetailOccurrenceFloatButton
        {
            get => _isVisibleDetailOccurrenceFloatButton;
            set
            {
                _isVisibleDetailOccurrenceFloatButton = value;
                OnPropertyChanged();
            }
        }

        private bool _isVisibleExitFloatButton;
        public bool IsVisibleExitFloatButton
        {
            get => _isVisibleExitFloatButton;
            set
            {
                _isVisibleExitFloatButton = value;
                OnPropertyChanged();
            }
        }

        private bool _isOpenMenu;
        public bool IsOpenMenu
        {
            get => _isOpenMenu;
            set
            {
                _isOpenMenu = value;
                OnPropertyChanged();
            }
        }
     
        private readonly INavigationService _navigationService;
        private readonly IMapService _mapService;
       
        private readonly UserRepository _userRepository;

        public ICommand UserDetailCommand;
        public ICommand ExitCommand;
        public ICommand AddOccurrenceCommand;
        public ICommand DetailOccurrenceCommand;

        #endregion

        public HomeMapViewModel(INavigationService navigationService, IMapService mapService)
        {
            _navigationService = navigationService;
            _mapService = mapService;
           
            _userRepository = new();

            UserDetailCommand = new Command(OnUserDetailCommand);
            ExitCommand = new Command(OnExitCommand);
            AddOccurrenceCommand = new Command(OnAddOccurrenceCommand);
            DetailOccurrenceCommand = new Command(OnDetailOccurrenceCommand);
        }

        #region Private Methods

        private void OnAddOccurrenceCommand()
        {
            BottomSheetAddOccurrenceState = BottomSheetState.HalfExpanded;
        }

        private async void OnDetailOccurrenceCommand()
        {
            await _navigationService.NavigationWithParameter<OccurrencesHistoryView>();

            CloseMunuRoundButtons();
        }
        
        private async void OnUserDetailCommand()
        {
            await _navigationService.NavigationWithParameter<UserDetailView>();

            CloseMunuRoundButtons();
        }

        private async void OnExitCommand()
        {
            var result = await App.Current.MainPage.DisplayAlert("Sair", "Deseja realmente deslogar sua conta?", "Sim", "Cancelar");

            if (!result) return;

            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_AUTH_TOKEN_KEY);
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_USER_LOCAL_ID_KEY);
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_USER_LOGGED);

            await _navigationService.NavigationWithRoute(StringConstants.LOGINVIEW_ROUTE);
        }

        private void ChangeIconOfLockUnlockButton(string nameIcon)
        {
            LockUnlockImage = ImageSource.FromFile(nameIcon);
        }

        private void ChangeIsEnabledOnGroupButtons(bool isEnabled)
        {
            MainButtonIsEnabled = isEnabled;
            DetailOccurrenceButtonIsEnabled = isEnabled;
            AddOccurrenceButtonIsEnabled = isEnabled;
            UserButtonIsEnabled = isEnabled;
            ExitButtonIsEnabled = isEnabled;
        }

        private void CheckAnonymousAccess(bool isEnabled)
        {
            ChangeIconOfLockUnlockButton(isEnabled ? "unlock_24" : "anonymous_24");

            LockUnlockButtonIsEnabled = isEnabled;
            DetailOccurrenceButtonIsEnabled = isEnabled;
            AddOccurrenceButtonIsEnabled = isEnabled;
            UserButtonIsEnabled = isEnabled;
            ExitButtonIsEnabled = isEnabled;
        }

        private void CloseMunuRoundButtons()
        {
           IsVisibleDetailOccurrenceFloatButton = false;
           IsVisibleAddOccurrenceFloatButton = false;
           IsVisibleUserFloatButton = false;
           IsVisibleExitFloatButton = false;
           IsOpenMenu = false;
        }

        #endregion

        #region Public Methods

        public async Task LoadPinsMock()
        {
            IsBusy = true;

            try
            {
                var list = await _mapService.GetPinsMock();

                PinsList = new ObservableCollection<PinDto>(list);
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

        public async Task<Location> GetLocationOfUserLogged()
        {
            try
            {
                var userLogged = await _userRepository.GetByLocalIdFirebase(App.FirebaseUserLocalIdKey);

                var cities = CitiesOfEs.GetCitiesOfEspiritoSanto();

                var city = cities.Where(x => x.Name.Equals(userLogged.City) && x.State.Equals(userLogged.State)).FirstOrDefault();

                return new Location(city.Latitude, city.Longitude);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Location();
            }
        }

        public void ChangeLockUnlokImage(object file)
        {
            var name = file.ToString()[6..];

            if (name.Equals("lock_24"))
            {
                ChangeIconOfLockUnlockButton("unlock_24");
                ChangeIsEnabledOnGroupButtons(isEnabled: true);             
            }
            else
            {
                ChangeIconOfLockUnlockButton("lock_24");
                ChangeIsEnabledOnGroupButtons(isEnabled: false);              
            }
        }
       
        public async Task SaveOccurrence()
        {
            try
            {
                var newOccurrence = new OccurrenceModel
                {
                    Address = Address,
                    Date = Date,
                    Hour = Hour,
                    Resume = Resume
                };

                var teste = newOccurrence;

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
               
        #endregion        
    }
}