using System.Collections.ObjectModel;
using System.Windows.Input;
using DevExpress.Maui.Controls;
using Firebase.Database;
using FocamapMaui.Controls;
using FocamapMaui.Controls.Maps;
using FocamapMaui.Controls.Resources;
using FocamapMaui.Helpers.Models;
using FocamapMaui.Models.Map;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.Models;
using FocamapMaui.MVVM.Views;
using FocamapMaui.Repositories;
using FocamapMaui.Services.Firebase;
using FocamapMaui.Services.Map;
using FocamapMaui.Services.Navigation;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace FocamapMaui.MVVM.ViewModels
{
    [QueryProperty(nameof(AnonymousAccess), StringConstants.ANONYMOUS_ACCESS)]
    public class HomeMapViewModel : ViewModelBase
    {
        #region Properties

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

        private ObservableCollection<PinDto> _pinsList = new();
        public ObservableCollection<PinDto> PinsList
        {
            get => _pinsList;
            set
            {
                _pinsList = value;
                OnPropertyChanged();
            }
        }
        
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

        private bool _lowChipIsEnabled = true;
        public bool LowChipIsEnabled
        {
            get => _lowChipIsEnabled;
            set
            {
                _lowChipIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _averageChipIsEnabled = true;
        public bool AverageChipIsEnabled
        {
            get => _averageChipIsEnabled;
            set
            {
                _averageChipIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _highChipIsEnabled = true;
        public bool HighChipIsEnabled
        {
            get => _highChipIsEnabled;
            set
            {
                _highChipIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _lowChipIsSelectedToAdd;
        public bool LowChipIsSelectedToAdd
        {
            get => _lowChipIsSelectedToAdd;
            set
            {
                _lowChipIsSelectedToAdd = value;
                OnPropertyChanged();
            }
        }

        private bool _averageChipIsSelectedToAdd;
        public bool AverageChipIsSelectedToAdd
        {
            get => _averageChipIsSelectedToAdd;
            set
            {
                _averageChipIsSelectedToAdd = value;
                OnPropertyChanged();
            }
        }

        private bool _highChipIsSelectedToAdd;
        public bool HighChipIsSelectedToAdd
        {
            get => _highChipIsSelectedToAdd;
            set
            {
                _highChipIsSelectedToAdd = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelectingAddressOnMap;
        public bool IsSelectingAddressOnMap
        {
            get => _isSelectingAddressOnMap;
            set
            {
                _isSelectingAddressOnMap = value;
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
        
        private DateTime _dateOccurrence = DateTime.Now;
        public DateTime DateOccurrence
        {
            get => _dateOccurrence;
            set
            {
                _dateOccurrence = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _hourOccurrence;
        public TimeSpan HourOccurrence
        {
            get => _hourOccurrence;
            set
            {
                _hourOccurrence = value;
                OnPropertyChanged();
            }
        }

        private string _titleOccurrence;
        public string TitleOccurrence
        {
            get => _titleOccurrence;
            set
            {
                _titleOccurrence = value;
                OnPropertyChanged();
            }
        }

        private string _addressOccurrence;
        public string AddressOccurrence
        {
            get => _addressOccurrence;
            set
            {
                _addressOccurrence = value;
                OnPropertyChanged();
            }
        }

        private string _resumeOccurrence;
        public string ResumeOccurrence
        {
            get => _resumeOccurrence;
            set
            {
                _resumeOccurrence = value;
                OnPropertyChanged();
            }
        }
       
        private Color _lowChipTextColor = Colors.Gray;
        public Color LowChipTextColor
        {
            get => _lowChipTextColor;
            set
            {
                _lowChipTextColor = value;
                OnPropertyChanged();
            }
        }

        private Color _lowChipBorderColor = Colors.Gray;
        public Color LowChipBorderColor
        {
            get => _lowChipBorderColor;
            set
            {
                _lowChipBorderColor = value;
                OnPropertyChanged();
            }
        }

        private Color _lowChipBackgroundColor = Colors.Transparent;
        public Color LowChipBackgroundColor
        {
            get => _lowChipBackgroundColor;
            set
            {
                _lowChipBackgroundColor = value;
                OnPropertyChanged();
            }
        }

        private Color _averageChipTextColor = Colors.Gray;
        public Color AverageChipTextColor
        {
            get => _averageChipTextColor;
            set
            {
                _averageChipTextColor = value;
                OnPropertyChanged();
            }
        }

        private Color _averageChipBorderColor = Colors.Gray;
        public Color AverageChipBorderColor
        {
            get => _averageChipBorderColor;
            set
            {
                _averageChipBorderColor = value;
                OnPropertyChanged();
            }
        }

        private Color _averageChipBackgroundColor = Colors.Transparent;
        public Color AverageChipBackgroundColor
        {
            get => _averageChipBackgroundColor;
            set
            {
                _averageChipBackgroundColor = value;
                OnPropertyChanged();
            }
        }
       
        private Color _highChipTextColor = Colors.Gray;
        public Color HighChipTextColor
        {
            get => _highChipTextColor;
            set
            {
                _highChipTextColor = value;
                OnPropertyChanged();
            }
        }

        private Color _highChipBorderColor = Colors.Gray;
        public Color HighChipBorderColor
        {
            get => _highChipBorderColor;
            set
            {
                _highChipBorderColor = value;
                OnPropertyChanged();
            }
        }

        private Color _highChipBackgroundColor = Colors.Transparent;
        public Color HighChipBackgroundColor
        {
            get => _highChipBackgroundColor;
            set
            {
                _highChipBackgroundColor = value;
                OnPropertyChanged();
            }
        }

        private LocationOccurrence _locationOccurrence;
        public LocationOccurrence LocationOccurrence
        {
            get => _locationOccurrence;
            set
            {
                _locationOccurrence = value;
                OnPropertyChanged();
            }
        }
        
        private readonly INavigationService _navigationService;      
        private readonly IRealtimeDatabaseService _realtimeDatabaseService;
        private readonly IMapService _mapService;


        private readonly UserRepository _userRepository;

        public FirebaseClient client = new(StringConstants.FIREBASE_REALTIME_DATABASE);

        public ICommand GoToViewUserDetailCommand;
        public ICommand ExitViewCommand;
        public ICommand OpenBottomSheetAddOccurrenceCommand;
        public ICommand GoToViewDetailOccurrenceCommand;

        #endregion

        public HomeMapViewModel(INavigationService navigationService,
                                IRealtimeDatabaseService realtimeDatabaseService, IMapService mapService)
        {
            _navigationService = navigationService;           
            _realtimeDatabaseService = realtimeDatabaseService;
            _mapService = mapService;

            _userRepository = new();

            GoToViewUserDetailCommand = new Command(OnGoToViewUserDetailCommand);
            ExitViewCommand = new Command(OnExitViewCommand);
            OpenBottomSheetAddOccurrenceCommand = new Command(OnOpenBottomSheetAddOccurrenceCommand);
            GoToViewDetailOccurrenceCommand = new Command(OnGoToViewDetailOccurrenceCommand);
        }

        #region Private Methods
       
        private async void OnGoToViewDetailOccurrenceCommand()
        {
            await _navigationService.NavigationWithParameter<OccurrencesHistoryView>();

            CloseMunuRoundButtons();
        }
        
        private async void OnGoToViewUserDetailCommand()
        {
            await _navigationService.NavigationWithParameter<UserDetailView>();

            CloseMunuRoundButtons();
        }

        private async void OnExitViewCommand()
        {
            var result = await App.Current.MainPage.DisplayAlert("Sair", "Deseja realmente deslogar sua conta?", "Sim", "Cancelar");

            if (!result) return;

            RemoveUserKeysAndFromPreferences();
           
            await _navigationService.NavigationWithRoute(StringConstants.LOGINVIEW_ROUTE);
        }

        private static void RemoveUserKeysAndFromPreferences()
        {
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_AUTH_TOKEN_KEY);
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_USER_LOCAL_ID_KEY);
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_USER_LOGGED);
        }

        private OccurrenceModel CreateOccurrenceModel()
        {           
            return new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = TitleOccurrence,
                Address = AddressOccurrence,
                Date = DateOccurrence.ToShortDateString(),
                Hour = HourOccurrence.ToString(@"hh\:mm"),
                Resume = ResumeOccurrence,
                Status = LowChipIsSelectedToAdd ? (int)PinStatus.Baixo : AverageChipIsSelectedToAdd ? (int)PinStatus.Medio : (int)PinStatus.Alto,
                Location = LocationOccurrence
            };
        }
      
        private void UpdateListPins(List<OccurrenceModel> list)
        {
            PinsList.Clear();
          
            PinsList = new ObservableCollection<PinDto>(CreateListOfPinDtos(list));
        }

        private static List<PinDto> CreateListOfPinDtos(List<OccurrenceModel> occurrenceModels)
        {
            List<PinDto> pins = new();

            occurrenceModels.ForEach(x => {

                var pin = new PinDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Address = x.Address,
                    Content = x.Resume,
                    FullDate = $"{x.Date} ás {x.Hour}",
                    Status = x.Status,
                    Latitude = x.Location.Latitude,
                    Longitude = x.Location.Longitude
                };

                pins.Add(pin);
            });

            return pins;
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

        private void SetFalseToStatusOfSelectionChipsButtos()
        {
            LowChipIsSelectedToAdd = false;
            AverageChipIsSelectedToAdd = false;
            HighChipIsSelectedToAdd = false;
        }

        private void SetChangeOnLowChip()
        {
            SetFalseToStatusOfSelectionChipsButtos();
            SetChangeDefaultStyleOnChips();

            LowChipTextColor = ControlResources.GetResource<Color>("CLWhite");
            LowChipBorderColor = ControlResources.GetResource<Color>("CLPrimaryOrange");
            LowChipBackgroundColor = ControlResources.GetResource<Color>("CLSecondary");

            LowChipIsSelectedToAdd = true;
        }
        
        private void SetChangeOnAverageChip()
        {
            SetFalseToStatusOfSelectionChipsButtos();
            SetChangeDefaultStyleOnChips();

            AverageChipTextColor = ControlResources.GetResource<Color>("CLWhite");
            AverageChipBorderColor = ControlResources.GetResource<Color>("CLPrimaryOrange");
            AverageChipBackgroundColor = ControlResources.GetResource<Color>("CLSecondary");

            AverageChipIsSelectedToAdd = true;
        }

        private void SetChangeOnHighChip()
        {
            SetFalseToStatusOfSelectionChipsButtos();
            SetChangeDefaultStyleOnChips();

            HighChipTextColor = ControlResources.GetResource<Color>("CLWhite");
            HighChipBorderColor = ControlResources.GetResource<Color>("CLPrimaryOrange");
            HighChipBackgroundColor = ControlResources.GetResource<Color>("CLSecondary");

            HighChipIsSelectedToAdd = true;
        }

        private void SetVaueForProperty_IsSelectingAddressOnMap(bool value)
        {
            IsSelectingAddressOnMap = value;
        }

        #endregion

        #region Public Methods

        public async Task LoadPins()
        {
            IsBusy = true;

            try
            {                
                var list = await _realtimeDatabaseService.GetAllAsync<OccurrenceModel>(nameof(OccurrenceModel));

                if (list.Count == 0)
                {
                    PinsList.Clear();
                    return;
                }

                UpdateListPins(list);

                await Task.CompletedTask;
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
            IsBusy = true;

            try
            {
                await _realtimeDatabaseService.SaveAsync(nameof(OccurrenceModel), CreateOccurrenceModel());
                
                ClearInputsOfBottomSheetAddOccurrence();

                await LoadPins();

                await App.Current.MainPage.DisplayAlert("Ocorrência",
                    "Sua ocorrência foi enviada com sucesso. Agora é com a gente; vamos analisar sua solicitação e, posteriormente, disponibilizá-la ao mapa.", "Ok");

                SetVaueForProperty_IsSelectingAddressOnMap(false);

                CloseBottomSheetAddOccurrence();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Ops",
                    "Houve um problema com sua solicitação de ocorrência. Por favor, tente novamente em alguns instantes.", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        public void SetChangeOnSelectedChip(string chip)
        { 
            switch (chip)
            {
                case StringConstants.LOW:
                    SetChangeOnLowChip();
                    break;
                case StringConstants.AVERAGE:
                    SetChangeOnAverageChip();
                    break;
                default:
                    SetChangeOnHighChip();
                    break;
            }
        }
       
        public void SetChangeDefaultStyleOnChips()
        {
            LowChipTextColor = ControlResources.GetResource<Color>("CLGray");
            LowChipBorderColor = ControlResources.GetResource<Color>("CLGray");
            LowChipBackgroundColor = Colors.Transparent;

            AverageChipTextColor = ControlResources.GetResource<Color>("CLGray");
            AverageChipBorderColor = ControlResources.GetResource<Color>("CLGray");
            AverageChipBackgroundColor = Colors.Transparent;

            HighChipTextColor = ControlResources.GetResource<Color>("CLGray");
            HighChipBorderColor = ControlResources.GetResource<Color>("CLGray");
            HighChipBackgroundColor = Colors.Transparent;
        }

        public void ClearInputsOfBottomSheetAddOccurrence()
        {
            TitleOccurrence = string.Empty;
            AddressOccurrence = string.Empty;
            DateOccurrence = DateTime.Now;
            HourOccurrence = new TimeSpan();
            ResumeOccurrence = string.Empty;

            SetChangeDefaultStyleOnChips();
        }

        public async Task GetReverseGeocoding(Location location)
        {
            IsBusy = true;

            try
            {
                var address = await _mapService.GetAddressFromLocationAsync(location);

                if(!string.IsNullOrEmpty(address))
                {
                    if (!CheckIfRegionIsAllowed(address))
                    {
                        await App.Current.MainPage.DisplayAlert("Ops", $"Selecione somente locais de sua cidade.", "Ok");
                        return;
                    }

                    var result = await App.Current.MainPage.DisplayAlert("Local", $"Confirmar local selecionado?: {address}", "Sim", "Cancelar");

                    if (result)
                    {
                        AddressOccurrence = address;
                        LocationOccurrence = new LocationOccurrence
                        {
                            Latitude = location.Latitude,
                            Longitude = location.Longitude
                        };

                        SetVaueForProperty_IsSelectingAddressOnMap(false);
                    }                   
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Ops","Parece que não foi possivel obter o local selecionado. Verifique sua conexão e tente novamente.","Ok");
            }
            finally
            {
                IsBusy = false;
            }            
        }

        private static bool CheckIfRegionIsAllowed(string address)
        {
           var city = ControlPreferences.GetKeyObjectOfPreferences<City>(StringConstants.CITY);

            return address.Contains(city.Name) && address.Contains(city.State);
        }

        public void OnOpenBottomSheetAddOccurrenceCommand()
        {
            BottomSheetAddOccurrenceState = BottomSheetState.FullExpanded;
        }

        private void CloseBottomSheetAddOccurrence()
        {
            BottomSheetAddOccurrenceState = BottomSheetState.Hidden;
        }

        #endregion
    }
}