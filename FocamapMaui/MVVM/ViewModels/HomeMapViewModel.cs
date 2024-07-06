using System.Collections.ObjectModel;
using System.Windows.Input;
using AndroidX.Lifecycle;
using DevExpress.Maui.Controls;
using FocamapMaui.Controls;
using FocamapMaui.Models;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.Models;
using FocamapMaui.MVVM.Views;
using FocamapMaui.Services.Map;
using FocamapMaui.Services.Navigation;
using Microsoft.Maui.Controls.Maps;
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
                UpdateMapPins();
            }
        }

        private BottomSheetState _bottomSheetAddOccurrenceState;
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

        private bool _occurrenceButtonIsEnabled = true;
        public bool OccurrenceButtonIsEnabled
        {
            get => _occurrenceButtonIsEnabled;
            set
            {
                _occurrenceButtonIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _addButtonIsEnabled = true;
        public bool AddButtonIsEnabled
        {
            get => _addButtonIsEnabled;
            set
            {
                _addButtonIsEnabled = value;
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

        private bool _isOpenPopupPinMap;
        public bool IsOpenPopupPinMap
        {
            get => _isOpenPopupPinMap;
            set
            {
                _isOpenPopupPinMap = value;
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
       
        public ICommand CloseDateEditCommand;       

        #endregion

        public HomeMapViewModel(INavigationService navigationService, IMapService mapService)
        {
            _navigationService = navigationService;
            _mapService = mapService;
                       
            LoadPinsMock();
        }

        #region Public Methods

        public void UpdateMapPins()
        {
            if (Map == null || PinsList == null)
                return;

            Map.Pins.Clear();

            foreach (var pinDto in PinsList)
            {               
                var pin = new Pin
                {                    
                    Label = pinDto.Title,
                    Address = pinDto.Content,
                    Type = PinType.Generic,
                    Location = new Location(pinDto.Latitude, pinDto.Longitude)
                };

                // Associe o evento MarkerClicked do PinDto ao evento do Pin
                pin.MarkerClicked += (s, e) =>
                {
                    var args = new MarkerClickedEventArgs();
                    pinDto.RaiseMarkerClickedEvent(args);
                    e.HideInfoWindow = args.HideInfoWindow;
                };

                Map.Pins.Add(pin);              
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

        #region Private Methods

        private void LoadPinsMock()
        {                      
            PinsList = new ObservableCollection<PinDto>(_mapService.GetPinsMock());
        }
        
        public void OnAddOccurrenceCommand()
        {
            BottomSheetAddOccurrenceState = BottomSheetState.HalfExpanded;
        }

        public async void OnSeeOccurrencesHistoryCommand()
        {
            await _navigationService.NavigationWithParameter<OccurrencesHistoryView>();
        }

        public async void OnUserDetailCommand()
        {
            await _navigationService.NavigationWithParameter<UserDetailView>();
        }

        public async void OnExitCommand()
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
            OccurrenceButtonIsEnabled = isEnabled;
            AddButtonIsEnabled = isEnabled;
            UserButtonIsEnabled = isEnabled;
            ExitButtonIsEnabled = isEnabled;
        }
       
        private void CheckAnonymousAccess(bool isEnabled)
        {
            ChangeIconOfLockUnlockButton(isEnabled ? "unlock_24" : "anonymous_24");

            LockUnlockButtonIsEnabled = isEnabled;
            OccurrenceButtonIsEnabled = isEnabled;
            AddButtonIsEnabled = isEnabled;
            UserButtonIsEnabled = isEnabled;
            ExitButtonIsEnabled = isEnabled;
        }

        #endregion               
    }
}

