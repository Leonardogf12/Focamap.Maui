using System.Collections.ObjectModel;
using System.Windows.Input;
using DevExpress.Maui.Controls;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.Views;
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

        private ObservableCollection<Pin> _pinsList;
        public ObservableCollection<Pin> PinsList
        {
            get => _pinsList;
            set
            {
                _pinsList = value;
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

        private readonly INavigationService _navigationService;

        public ICommand AddOccurrenceCommand;
        public ICommand SeeOccurrencesHistoryCommand;
        public ICommand UserDetailCommand;
        public ICommand CloseDateEditCommand;

        #endregion

        public HomeMapViewModel(INavigationService navigationService)
		{
            _navigationService = navigationService;

            LoadPins();

            AddOccurrenceCommand = new Command(OnAddOccurrenceCommand);
            SeeOccurrencesHistoryCommand = new Command(OnSeeOccurrencesHistoryCommand);
            UserDetailCommand = new Command(OnUserDetailCommand);          
        }
     
        #region Public Methods

        public void UpdateMapPins()
        {
            if (Map == null || PinsList == null)
                return;

            Map.Pins.Clear();

            foreach (var pin in PinsList)
            {
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
        
        #endregion

        #region Private Methods

        private void LoadPins()
        {
            var list = new List<Pin>
            {
                new Pin
                {
                    Label = "Assalto",
                    Address = "Armado?:Sim | Feridos?:1",
                    Type = PinType.Generic,
                    Location = new Location(-19.394837, -40.049279),
                },
                new Pin
                {
                    Label = "Tiros",
                    Address = "Feridos?:1",
                    Type = PinType.Place,
                    Location = new Location(-19.391254, -40.050202)
                },
                new Pin
                {
                    Label = "Tiros",
                    Address = "Feridos?:Não",
                    Type = PinType.SearchResult,
                    Location = new Location(-19.395747, -40.037993)
                },
                new Pin
                {
                    Label = "Assalto",
                    Address = "Armado?:Sim | Feridos?:Não",
                    Type = PinType.SavedPin,
                    Location = new Location(-19.400564, -40.045224)
                },
            };

            PinsList = new ObservableCollection<Pin>(list);
        }
    
        private void ChangeIconOfLockUnlockButton(string nameIcon)
        {
            LockUnlockImage = ImageSource.FromFile(nameIcon);
        }

        private void ChangeIsEnabledOnGroupButtons(bool isEnabled)
        {
            OccurrenceButtonIsEnabled = isEnabled;
            AddButtonIsEnabled = isEnabled;
            UserButtonIsEnabled = isEnabled;
        }

        private void OnAddOccurrenceCommand()
        {
            BottomSheetAddOccurrenceState = BottomSheetState.HalfExpanded;
        }

        private async void OnSeeOccurrencesHistoryCommand()
        {
            await _navigationService.NavigationWithParameter<OccurrencesHistoryView>();
        }

        private async void OnUserDetailCommand(object obj)
        {
            await _navigationService.NavigationWithParameter<UserDetailView>();
        }

        private void CheckAnonymousAccess(bool isEnabled)
        {
            ChangeIconOfLockUnlockButton(isEnabled ? "unlock_24" : "anonymous_24");
            LockUnlockButtonIsEnabled = isEnabled;
            OccurrenceButtonIsEnabled = isEnabled;
            AddButtonIsEnabled = isEnabled;
            UserButtonIsEnabled = isEnabled;
        }

        #endregion
    }
}

