using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using FocamapMaui.Controls;
using FocamapMaui.Controls.Maps;
using FocamapMaui.Controls.Resources;
using FocamapMaui.Helpers.Models;
using FocamapMaui.Models.Map;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.Models;
using FocamapMaui.MVVM.Views;
using FocamapMaui.Services.Authentication;
using FocamapMaui.Services.Firebase;
using FocamapMaui.Services.Map;
using FocamapMaui.Services.Navigation;
using Microsoft.Maui.Maps;
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

        private bool _isVisibleSettingsFloatButton;
        public bool IsVisibleSettingsFloatButton
        {
            get => _isVisibleSettingsFloatButton;
            set
            {
                _isVisibleSettingsFloatButton = value;
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

        private bool _isEnabledDisplayName;
        public bool IsEnabledDisplayName
        {
            get => _isEnabledDisplayName;
            set
            {
                _isEnabledDisplayName = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabledEmail;
        public bool IsEnabledEmail
        {
            get => _isEnabledEmail;
            set
            {
                _isEnabledEmail = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabledPassword;
        public bool IsEnabledPassword
        {
            get => _isEnabledPassword;
            set
            {
                _isEnabledPassword = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabledRegion;
        public bool IsEnabledRegion
        {
            get => _isEnabledRegion;
            set
            {
                _isEnabledRegion = value;
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

        private BottomSheetState _bottomSheetSettingsState = BottomSheetState.Hidden;
        public BottomSheetState BottomSheetSettingsState
        {
            get => _bottomSheetSettingsState;
            set
            {
                _bottomSheetSettingsState = value;
                OnPropertyChanged();
            }
        }     

        private ImageSource _imageSourceMainButton = ControlResources.GetImage("menu_24");
        public ImageSource ImageSourceMainButton
        {
            get => _imageSourceMainButton;
            set
            {
                _imageSourceMainButton = value;
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

        private string _letterUserName;
        public string LetterUserName
        {
            get => _letterUserName;
            set
            {
                _letterUserName = value;
                OnPropertyChanged();
            }
        }

        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set
            {
                _displayName = value;
                OnPropertyChanged();

                Name = value;
                OldNameUser = value;
            }
        }

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

        private City _selectedItemCity;
        public City SelectedItemCity
        {
            get => _selectedItemCity;
            set
            {
                _selectedItemCity = value;
                OnPropertyChanged();
            }
        }

        private City _selectedValueCity;
        public City SelectedValueCity
        {
            get => _selectedValueCity;
            set
            {
                _selectedValueCity = value;
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

        private Color _borderColorPassword = Colors.Transparent;
        public Color BorderColorPassword
        {
            get => _borderColorPassword;
            set
            {
                _borderColorPassword = value;
                OnPropertyChanged();
            }
        }

        private Color _borderColorDisplayName = Colors.Transparent;
        public Color BorderColorDisplayName
        {
            get => _borderColorDisplayName;
            set
            {
                _borderColorDisplayName = value;
                OnPropertyChanged();
            }
        }

        private Color _selectedItemBackgroudColor = ControlResources.GetResource<Color>("CLPrimaryOrange");
        public Color SelectedItemBackgroudColor
        {
            get => _selectedItemBackgroudColor;
            set
            {
                _selectedItemBackgroudColor = value;
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

        private Location _locationOfUserLogged;
        public Location LocationOfUserLogged
        {
            get => _locationOfUserLogged;
            set
            {
                _locationOfUserLogged = value;
                OnPropertyChanged();
            }
        }

        private UserModel _userLogged = new();
        public UserModel UserLogged
        {
            get => _userLogged;
            set
            {
                _userLogged = value;
                OnPropertyChanged();
            }
        }

        public string OldNameUser = string.Empty;

        private readonly INavigationService _navigationService;
        private readonly IRealtimeDatabaseService _realtimeDatabaseService;
        private readonly IMapService _mapService;
        private readonly IAuthenticationService _authenticationService;

        public ICommand OpenBottomSheetSettingsCommand;
        public ICommand ExitViewCommand;    
        public ICommand GoToViewDetailOccurrenceCommand;
        public ICommand OpenBottomSheetAddOccurrenceCommand;        
        public ICommand SaveOccurrenceCommand;     
        public ICommand TextEditAddressEndIconCommand;
       
        public class UpdateMapMessage { }

        #endregion

        public HomeMapViewModel(INavigationService navigationService,
                                IRealtimeDatabaseService realtimeDatabaseService,
                                IMapService mapService,
                                IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _realtimeDatabaseService = realtimeDatabaseService;
            _mapService = mapService;
            _authenticationService = authenticationService;

            CommandManagment();
        }

        #region Private Methods

        private void CommandManagment()
        {
            OpenBottomSheetSettingsCommand = new Command(OnOpenBottomSheetSettingsCommand);
            ExitViewCommand = new Command(async ()=> await OnExitViewCommand());
            OpenBottomSheetAddOccurrenceCommand = new Command(OnOpenBottomSheetAddOccurrenceCommand);
            GoToViewDetailOccurrenceCommand = new Command(OnGoToViewDetailOccurrenceCommand);
            SaveOccurrenceCommand = new Command(async () => await OnSaveOccurrenceCommand());
            TextEditAddressEndIconCommand = new Command(OnTextEditAddressEndIconCommand);                  
        }

        private async Task OnSaveOccurrenceCommand()
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

        private async void OnGoToViewDetailOccurrenceCommand()
        {
            await _navigationService.NavigationWithParameter<OccurrencesHistoryView>();

            CloseMenuRoundButtons();
        }
        
        private void OnOpenBottomSheetSettingsCommand()
        {
            BottomSheetSettingsState = BottomSheetState.FullExpanded;
        }       

        private void OnTextEditAddressEndIconCommand()
        {
            IsSelectingAddressOnMap = true;
            BottomSheetAddOccurrenceState = BottomSheetState.Hidden;
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
                Region = $"{LocationOccurrence.City}-{LocationOccurrence.State}",
                Location = LocationOccurrence,
                User = UserLogged,
            };
        }        

        private void UpdateLocationOfUserLoggedAndMoveToNewRegion()
        {
            LocationOfUserLogged.Latitude = SelectedItemCity.Latitude;
            LocationOfUserLogged.Longitude = SelectedItemCity.Longitude;

            var newLocation = new Location
            {
                Latitude = SelectedItemCity.Latitude,
                Longitude = SelectedItemCity.Longitude
            };

            _map.MoveToRegion(MapSpan.FromCenterAndRadius(newLocation, Distance.FromMeters(2700)));
        }
               
        private void UpdateUserInfos()
        {
            Email = UserLogged.Email;
            DisplayName = UserLogged.Name;            
            SelectedItemCity = UserLogged.City;
            LetterUserName = DisplayName[0].ToString();
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

        private static void SendWeakReferenceMessenger_OnHandlerChanged()
        {
            WeakReferenceMessenger.Default.Send(new UpdateMapMessage());
        }

        private void ChangeIconOfLockUnlockButton(string nameIcon)
        {
            ImageSourceMainButton = ControlResources.GetImage(nameIcon);
        }

        private bool CheckIfInputsAreOk()
        {
            var HasOk = true;

            if (!ValidateNameInput(HasOk) || !ValidatePasswordInput(HasOk))
            {
                HasOk = false;
            }

            return HasOk;
        }

        private bool ValidatePasswordInput(bool hasOk)
        {
            if (string.IsNullOrEmpty(Password) || Name.Length < 3)
            {
                BorderColorPassword = ControlResources.GetResource<Color>("CLErrorBorderColor");
                hasOk = false;
            }
            else
            {
                BorderColorPassword = Colors.Transparent;
            }

            return hasOk;
        }

        private bool ValidateNameInput(bool hasOk)
        {
            if (string.IsNullOrEmpty(Name) || Name.Length < 3)
            {
                BorderColorDisplayName = ControlResources.GetResource<Color>("CLErrorBorderColor");
                hasOk = false;
            }
            else
            {
                BorderColorDisplayName = Colors.Transparent;
            }

            return hasOk;
        }

        private void CheckAnonymousAccess(bool isEnabled)
        {
            ChangeIconOfLockUnlockButton(isEnabled ? "menu_24" : "anonymous_24");

            MainButtonIsEnabled = isEnabled;          
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

        private static void RemoveUserKeysOfPreferencesForLogOff()
        {
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_AUTH_TOKEN_KEY);
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_USER_LOCAL_ID_KEY);         
            ControlPreferences.RemoveKeyFromPreferences(StringConstants.FIREBASE_USER_LOGGED_KEY);           
        }

        #endregion

        #region Public Methods

        public async Task OnExitViewCommand()
        {
            var result = await App.Current.MainPage.DisplayAlert("Sair", "Deseja realmente deslogar sua conta?", "Sim", "Cancelar");

            if (!result) return;

            RemoveUserKeysOfPreferencesForLogOff();

            CloseBottomSheetSettings();

            CloseMenuRoundButtons();

            await _navigationService.NavigationWithRoute(StringConstants.LOGINVIEW_ROUTE);
        }

        public void OnOpenBottomSheetAddOccurrenceCommand()
        {
            BottomSheetAddOccurrenceState = BottomSheetState.FullExpanded;
        }

        public async Task LoadPins()
        {
            IsBusy = true;

            try
            {
                List<OccurrenceModel> listOccurrences = new();

                if(AnonymousAccess)
                {
                    listOccurrences = await _realtimeDatabaseService.GetAllAsync<OccurrenceModel>(nameChild: nameof(OccurrenceModel));
                }
                else
                {
                    listOccurrences = await _realtimeDatabaseService.GetByRegion<OccurrenceModel>(firstChild: nameof(OccurrenceModel),
                                                firstOrderBy: "Region", firstEqualTo: $"{UserLogged.City.Name}-{UserLogged.City.State}");
                }

                if (listOccurrences.Count == 0)
                {
                    PinsList.Clear();
                    return;
                }

                UpdateListPins(listOccurrences);

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

        public async Task UpdateProfileUser()
        {
            IsBusy = true;

            try
            {
                if (CheckIfInputsAreOk())
                {
                    var result = await _authenticationService.UpdateUserProfile(Email, Password, Name, SelectedItemCity);

                    if (result.Equals(StringConstants.OK))
                    {
                        GetUserLogged();

                        UpdateLocationOfUserLoggedAndMoveToNewRegion();

                        await LoadPins();

                        SendWeakReferenceMessenger_OnHandlerChanged();
                        
                        await App.Current.MainPage.DisplayAlert("Sucesso", "Alterações realizadas com sucesso!", "Ok");

                        return;
                    }
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

        public async Task GetReverseGeocoding(Location location)
        {
            IsBusy = true;

            try
            {
                var address = await _mapService.GetReverseGeocodingAsync(location);

                if (!string.IsNullOrEmpty(address))
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

                        var userLogged = ControlPreferences.GetKeyObjectOfPreferences<UserModel>(StringConstants.FIREBASE_USER_LOGGED_KEY);

                        LocationOccurrence = new LocationOccurrence
                        {
                            State = userLogged.City.State,
                            City = userLogged.City.Name,
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
                await App.Current.MainPage.DisplayAlert("Ops", "Parece que não foi possivel obter o local selecionado. Verifique sua conexão e tente novamente.", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task GetGeocoding(string entry)
        {
            IsBusy = true;

            try
            {
                var geocodeResult = await _mapService.GetGeocodingAsync(entry);

                if (geocodeResult is not null)
                {
                    var location = new Location(geocodeResult.Geometry.Location.Latitude, geocodeResult.Geometry.Location.Longitude);

                    Map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMeters(2700)));
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Buscar", "Local não encontrado. Tente buscar o endereço desta forma; \"Nome_Rua, Nome_Bairro, Nome_Cidade\".", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Ops", "Parece que não foi possivel obter o local especificado. Verifique sua conexão e tente novamente.", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void GetUserLogged()
        {
            if(AnonymousAccess)
            {
                GetAnonymousUserLogged();
                return;
            }

            UserLogged = ControlPreferences.GetKeyObjectOfPreferences<UserModel>(StringConstants.FIREBASE_USER_LOGGED_KEY);

            UpdateUserInfos();
        }

        private void GetAnonymousUserLogged()
        {                      
            var cities = CitiesOfEs.GetCitiesOfEspiritoSanto();

            var city = cities.Where(x => x.Name.StartsWith("Vitória") && x.State.Equals(StringConstants.ES)).FirstOrDefault();

            ControlPreferences.UpdateKeyFromPreference(key: StringConstants.FIREBASE_USER_LOGGED_KEY, valueString: "", contentObject: new UserModel
            {
                Name = "Anonymous",
                Email = "anonymousaccess@focamap.com",
                City = city,
                LocalIdFirebase = "no_id"
            });

            UserLogged = ControlPreferences.GetKeyObjectOfPreferences<UserModel>(StringConstants.FIREBASE_USER_LOGGED_KEY);
        }

        public void LoadCities()
        {
            Cities = CitiesOfEs.GetCitiesOfEspiritoSanto();
        }

        public Location GetLocationOfUserLogged()
        {
            try
            {
                var cities = CitiesOfEs.GetCitiesOfEspiritoSanto();

                var city = cities.Where(x => x.Name.Equals(UserLogged.City.Name) && x.State.Equals(UserLogged.City.State)).FirstOrDefault();

                return new Location(city.Latitude, city.Longitude);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Location();
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
       
        private static bool CheckIfRegionIsAllowed(string address)
        {           
            var userLogged = ControlPreferences.GetKeyObjectOfPreferences<UserModel>(StringConstants.FIREBASE_USER_LOGGED_KEY);

            return address.Contains(userLogged.City.Name) && address.Contains(userLogged.City.State);
        }
        
        private void CloseBottomSheetAddOccurrence()
        {
            BottomSheetAddOccurrenceState = BottomSheetState.Hidden;
        }

        private void CloseBottomSheetSettings()
        {
            BottomSheetSettingsState = BottomSheetState.Hidden;
        }

        public void CloseMenuRoundButtons()
        {
            IsVisibleDetailOccurrenceFloatButton = false;
            IsVisibleAddOccurrenceFloatButton = false;
            IsVisibleSettingsFloatButton = false;           
            ImageSourceMainButton = ControlResources.GetImage("menu_24");
            IsOpenMenu = false;
        }

        #endregion
    }
}