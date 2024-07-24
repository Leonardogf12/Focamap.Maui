using System.ComponentModel;
using Android.Gms.Maps;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using DevExpress.Maui.Core;
using DevExpress.Maui.Editors;
using FocamapMaui.Components.UI;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Extensions.Events;
using FocamapMaui.Controls.Maps;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.ViewModels;
using FocamapMaui.Services.Authentication;
using FocamapMaui.Services.Firebase;
using FocamapMaui.Services.Map;
using FocamapMaui.Services.Navigation;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Maps.Handlers;
using static FocamapMaui.MVVM.ViewModels.HomeMapViewModel;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace FocamapMaui.MVVM.Views
{
    public class HomeMapView : ContentPageBase
	{
        #region Properties

        private readonly INavigationService _navigationService;        
        private readonly IRealtimeDatabaseService _realtimeDatabaseService;
        private readonly IMapService _mapService;
        private readonly IAuthenticationService _authenticationService;

        private HomeMapViewModel _viewModel;

        private Map _map = new();
        private Grid _mainGrid;
        private MenuFloatButtons _menuFloatButton;       
        private BottomSheetSettingsCustom _bottomSheetSettingsCustom;

        #endregion

        public HomeMapView(INavigationService navigationService,
                           IRealtimeDatabaseService realtimeDatabaseService,
                           IMapService mapService,
                           IAuthenticationService authenticationService)
		{           
            _navigationService = navigationService;            
            _realtimeDatabaseService = realtimeDatabaseService;
            _mapService = mapService;
            _authenticationService = authenticationService;

            SetBackgroundColorToView();
            
            SetNavigationServiceInstancaFromViewModel(_navigationService, _realtimeDatabaseService, _mapService, _authenticationService);

            SetsTemporaryContentToView();

            CreateLoadingPopupView(this, _viewModel);

            BindingContext = _viewModel;

            RegisterWeakReferenceMessenger_OnHandlerChanged();            
        }
        
        #region UI

        private static Grid CreateTemporaryContent() => new();

        private void SetBackgroundColorToView()
        {
            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");
        }
     
        public View BuildHomeMapView
        {            
            get
            {               
                _mainGrid = CreateMainGrid();
                
                CreateMap(_mainGrid);

                CreateHeader(_mainGrid);
              
                CreateGroupButtons(_mainGrid);

                CreateBottomSheetAddOccurrence(_mainGrid);

                CreateBottomSheetSettings(_mainGrid);

                return _mainGrid;
            }
        }
        
        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() {Height = GridLength.Star},
                    new() {Height = GridLength.Auto},
                }
            };         
        }

        private void CreateMap(Grid grid)
        {           
           _map = MapCustom.GetMap(mapType: MapType.Street,
                                   location: _viewModel.LocationOfUserLogged,
                                   eventMapClicked: Map_MapClicked,
                                   propertyChangedMap: Map_PropertyChanged);

            grid.AddWithSpan(_map);
        }

        private void CreateHeader(Grid grid)
        {
            var stack = new StackLayout
            {               
                Margin = new Thickness(0, 70, 0, 0),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    new SearchBarCustom("Burcar", SearchBar_SearchButtonPressed),
                }
            };
           
            grid.AddWithSpan(stack, 0);
        }
                   
        private void CreateGroupButtons(Grid grid)
        {
            _menuFloatButton = new MenuFloatButtons(eventMainButton: MainButton_ClickedEvent,                                                  
                                                    commandSettingsButton: _viewModel.OpenBottomSheetSettingsCommand,
                                                    commandAddOccurrenceButton: _viewModel.OpenBottomSheetAddOccurrenceCommand,
                                                    commandDetailOccurrenceButton: _viewModel.GoToViewDetailOccurrenceCommand);

            _menuFloatButton.MainButton.SetBinding(IsEnabledProperty, nameof(_viewModel.MainButtonIsEnabled), BindingMode.TwoWay);
            _menuFloatButton.MainButton.SetBinding(Button.ImageSourceProperty, nameof(_viewModel.ImageSourceMainButton), BindingMode.TwoWay);
           
            _menuFloatButton.SettingsButton.SetBinding(IsVisibleProperty, nameof(_viewModel.IsVisibleSettingsFloatButton), BindingMode.TwoWay);
            
            _menuFloatButton.AddOccurrenceButton.SetBinding(IsVisibleProperty, nameof(_viewModel.IsVisibleAddOccurrenceFloatButton), BindingMode.TwoWay);
            
            _menuFloatButton.DetailOccurrenceButton.SetBinding(IsVisibleProperty, nameof(_viewModel.IsVisibleDetailOccurrenceFloatButton), BindingMode.TwoWay);
            
            grid.AddWithSpan(_menuFloatButton, 0);
        }

        private void CreateBottomSheetAddOccurrence(Grid grid)
        {
            var bottomSheetAddOccurrence = new BottomSheetAddOccurrenceCustom(commandButtonSave: _viewModel.SaveOccurrenceCommand);           
            bottomSheetAddOccurrence.SetBinding(BottomSheet.StateProperty, nameof(_viewModel.BottomSheetAddOccurrenceState), BindingMode.TwoWay);

            bottomSheetAddOccurrence.TextEditTitle.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.TitleOccurrence));

            bottomSheetAddOccurrence.TextEditAddress.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.AddressOccurrence));
           
            bottomSheetAddOccurrence.DateEditDate.SetBinding(DateEdit.DateProperty, nameof(_viewModel.DateOccurrence));

            bottomSheetAddOccurrence.HourEditHour.SetBinding(TimeEdit.TimeSpanProperty, nameof(_viewModel.HourOccurrence));

            bottomSheetAddOccurrence.MultilineEditResume.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.ResumeOccurrence));
          
            bottomSheetAddOccurrence.ChipButtonGroup.LowChipButton.SetBinding(Button.TextColorProperty, nameof(_viewModel.LowChipTextColor), BindingMode.TwoWay);
            bottomSheetAddOccurrence.ChipButtonGroup.LowChipButton.SetBinding(Button.BorderColorProperty, nameof(_viewModel.LowChipBorderColor), BindingMode.TwoWay);
            bottomSheetAddOccurrence.ChipButtonGroup.LowChipButton.SetBinding(BackgroundColorProperty, nameof(_viewModel.LowChipBackgroundColor), BindingMode.TwoWay);
            bottomSheetAddOccurrence.ChipButtonGroup.LowChipButton.SetBinding(IsEnabledProperty, nameof(_viewModel.LowChipIsEnabled), BindingMode.TwoWay);
           
            bottomSheetAddOccurrence.ChipButtonGroup.AverageChipButton.SetBinding(Button.TextColorProperty, nameof(_viewModel.AverageChipTextColor), BindingMode.TwoWay);
            bottomSheetAddOccurrence.ChipButtonGroup.AverageChipButton.SetBinding(Button.BorderColorProperty, nameof(_viewModel.AverageChipBorderColor), BindingMode.TwoWay);
            bottomSheetAddOccurrence.ChipButtonGroup.AverageChipButton.SetBinding(BackgroundColorProperty, nameof(_viewModel.AverageChipBackgroundColor), BindingMode.TwoWay);
            bottomSheetAddOccurrence.ChipButtonGroup.AverageChipButton.SetBinding(IsEnabledProperty, nameof(_viewModel.AverageChipIsEnabled), BindingMode.TwoWay);
            
            bottomSheetAddOccurrence.ChipButtonGroup.HighChipButton.SetBinding(Button.TextColorProperty, nameof(_viewModel.HighChipTextColor), BindingMode.TwoWay);
            bottomSheetAddOccurrence.ChipButtonGroup.HighChipButton.SetBinding(Button.BorderColorProperty, nameof(_viewModel.HighChipBorderColor), BindingMode.TwoWay);
            bottomSheetAddOccurrence.ChipButtonGroup.HighChipButton.SetBinding(BackgroundColorProperty, nameof(_viewModel.HighChipBackgroundColor), BindingMode.TwoWay);
            bottomSheetAddOccurrence.ChipButtonGroup.HighChipButton.SetBinding(IsEnabledProperty, nameof(_viewModel.HighChipIsEnabled), BindingMode.TwoWay);

            bottomSheetAddOccurrence.StateChanged += BottomSheetAddOccurrence_StateChanged;
            bottomSheetAddOccurrence.ChipButtonGroup.LowChipButton.Clicked += LowChipButton_Clicked;             
            bottomSheetAddOccurrence.ChipButtonGroup.AverageChipButton.Clicked += AverageChipButton_Clicked;
            bottomSheetAddOccurrence.ChipButtonGroup.HighChipButton.Clicked += HighChipButton_Clicked;
            bottomSheetAddOccurrence.TextEditAddress.EndIconCommand = _viewModel.TextEditAddressEndIconCommand;

            grid.AddWithSpan(bottomSheetAddOccurrence, 0);
        }

        private void CreateBottomSheetSettings(Grid grid)
        {
            _bottomSheetSettingsCustom = new BottomSheetSettingsCustom();
            _bottomSheetSettingsCustom.SetBinding(BottomSheet.StateProperty, nameof(_viewModel.BottomSheetSettingsState), BindingMode.TwoWay);

            _bottomSheetSettingsCustom.LabelNameUser.SetBinding(Label.TextProperty, nameof(_viewModel.LetterUserName));
            _bottomSheetSettingsCustom.UserTextEdit.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.DisplayName), BindingMode.TwoWay);
            _bottomSheetSettingsCustom.PasswordTextEdit.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.Password), BindingMode.TwoWay);
            _bottomSheetSettingsCustom.DropdownRegions.SetBinding(ItemsEditBase.ItemsSourceProperty, nameof(_viewModel.Cities));
            _bottomSheetSettingsCustom.DropdownRegions.SetBinding(ItemsEditBase.DropDownSelectedItemBackgroundColorProperty, nameof(_viewModel.SelectedItemBackgroudColor), BindingMode.TwoWay);
            _bottomSheetSettingsCustom.DropdownRegions.SetBinding(ComboBoxEdit.SelectedItemProperty, nameof(_viewModel.SelectedItemCity), BindingMode.TwoWay);
            _bottomSheetSettingsCustom.DropdownRegions.SetBinding(ComboBoxEdit.SelectedValueProperty, nameof(_viewModel.SelectedValueCity), BindingMode.TwoWay);

            _bottomSheetSettingsCustom.SaveUserEditedButton.Clicked += SaveUserEdited_Clicked;
            _bottomSheetSettingsCustom.IconEdit.AddTapGesture(EditImageProfileButtonBottomSheetSettingsTapGestureRecognizer_Tapped);
            _bottomSheetSettingsCustom.LogOffBorderButton.AddTapGesture(LogOffBottomSheetSettingsTapGestureRecognizer_Tapped);
            _bottomSheetSettingsCustom.DropdownRegions.SelectionChanged += RegionDropdownInput_SelectionChanged;

            grid.AddWithSpan(_bottomSheetSettingsCustom, 0);
        }

        #endregion

        #region Events

        private async void MainButton_ClickedEvent(object sender, EventArgs e)
        {
            if (!_viewModel.IsOpenMenu)
            {
                await OpenFloatMenuButtonsUnlocked();               
            }
            else
            {
                await CloseFloatMenuButtonsLocked();
            }
        }       

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            if(sender is SearchBar element)
            {
                if (!string.IsNullOrEmpty(element.Text))
                {
                    await _viewModel.GetGeocoding(element.Text);
                }
            } 
        }

        private async void CloseButtonBottomSheetTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if(sender is Image element)
            {
                await ClickAnimation.FadeAnimation(element);

                _viewModel.BottomSheetAddOccurrenceState = BottomSheetState.Hidden;
            }            
        }
        
        private void BottomSheetAddOccurrence_StateChanged(object sender, ValueChangedEventArgs<BottomSheetState> e)
        {
            if (e.NewValue.Equals(BottomSheetState.Hidden))
            {
                _viewModel.ClearInputsOfBottomSheetAddOccurrence();
            }
        }

        private async void SaveUserEdited_Clicked(object sender, EventArgs e)
        {
            if (sender is Button element)
            {
                await ClickAnimation.FadeAnimation(element);

                _bottomSheetSettingsCustom.UserTextEdit.Unfocus();
                _bottomSheetSettingsCustom.PasswordTextEdit.Unfocus();

                await _viewModel.UpdateProfileUser();
            }
        }

        private void EditImageProfileButtonBottomSheetSettingsTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            return;
        }

        private async void LogOffBottomSheetSettingsTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is Border element)
            {
                await ClickAnimation.BrightnessAnimation(element);

                await _viewModel.OnExitViewCommand();
            }
        }

        private async void Map_MapClicked(object sender, MapClickedEventArgs e)
        {
            if (_viewModel.IsSelectingAddressOnMap)
            {
                var location = e.Location;

                await _viewModel.GetReverseGeocoding(location);
                
                _viewModel.OnOpenBottomSheetAddOccurrenceCommand();                       
            }          
        }
        
        private void Map_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_map.VisibleRegion))
            {
                //IDENTIFICA QUANDO O USUARIO MUDA POSICAO - MOVIMENTACAO NO MAPA.       
            }
        }

        private void LowChipButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button element)
            {
                _viewModel.SetChangeOnSelectedChip(element.Text);
            }
        }

        private void AverageChipButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button element)
            {
                _viewModel.SetChangeOnSelectedChip(element.Text);
            }
        }

        private void HighChipButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button element)
            {
                _viewModel.SetChangeOnSelectedChip(element.Text);
            }
        }

        private void RegionDropdownInput_SelectionChanged(object sender, EventArgs e) => _bottomSheetSettingsCustom.DropdownRegions.Unfocus();

        #endregion

        #region Actions

        private void SetNavigationServiceInstancaFromViewModel(INavigationService navigationService,
                                                               IRealtimeDatabaseService realtimeDatabaseService,
                                                               IMapService mapService,
                                                               IAuthenticationService authenticationService)
        {
            _viewModel = new HomeMapViewModel(navigationService, realtimeDatabaseService, mapService, authenticationService);
        }

        private void RegisterWeakReferenceMessenger_OnHandlerChanged()
        {
            WeakReferenceMessenger.Default.Register<UpdateMapMessage>(this, (r, m) => OnHandlerChanged());
        }

        private void SetsTemporaryContentToView()
        {
            Content = CreateTemporaryContent();           
        }

        private void SetsTheFullContentToView()
        {
            Content = BuildHomeMapView;
          
            _viewModel.Map = _map;
        }

        private void SetFalseToIsBusyViewModelBase()
        {
            //Set False because the modified Map handler does not recognize.
            (BindingContext as ViewModelBase).IsBusy = false;
        }

        private async Task LoadPinsOfFirebase() => await _viewModel.LoadPins();

        private void LoadUserLocationAsync()
        {
            try
            {
                _viewModel.LocationOfUserLogged = _viewModel.GetLocationOfUserLogged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _viewModel.LocationOfUserLogged = new Location();
            }
        }

        private void LoadListCities() => _viewModel.LoadCities();
        
        private void LoadUserLogged() => _viewModel.GetUserLogged();

        private void ApplyModificationsToTheMap() => OnHandlerChanged();

        private void EnsurePopupCloses()
        {
            if (App.popupLoading != null)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await App.popupLoading.CloseAsync();
                    App.popupLoading = null;
                });
            }

            SetFalseToIsBusyViewModelBase();
        }

        private async void SetTrueValueToIsVisiblePropertyOfFloatButtons(double x = 0, double y = 0, uint lenght = 20)
        {           
            _viewModel.IsVisibleSettingsFloatButton = true;
            await _menuFloatButton.SettingsButton.TranslateTo(x, y, lenght);
            await _menuFloatButton.SettingsButton.TranslateTo(x, y, lenght);

            _viewModel.IsVisibleAddOccurrenceFloatButton = true;
            await _menuFloatButton.AddOccurrenceButton.TranslateTo(x, y, lenght);
            await _menuFloatButton.AddOccurrenceButton.TranslateTo(x, y, lenght);

            _viewModel.IsVisibleDetailOccurrenceFloatButton = true;
            await _menuFloatButton.DetailOccurrenceButton.TranslateTo(x, y, lenght);
            await _menuFloatButton.DetailOccurrenceButton.TranslateTo(x, y, lenght);
        }

        private async void SetFalseValueToIsVisiblePropertyOfFloatButtons(double x = 0, double y = 0, uint lenght = 100)
        {
            _viewModel.IsVisibleDetailOccurrenceFloatButton = false;
            _viewModel.IsVisibleAddOccurrenceFloatButton = false;
            _viewModel.IsVisibleSettingsFloatButton = false;          
           
            await _menuFloatButton.SettingsButton.TranslateTo(x, y, lenght);
            await _menuFloatButton.SettingsButton.TranslateTo(x, y, lenght);

            await _menuFloatButton.AddOccurrenceButton.TranslateTo(x, y, lenght);
            await _menuFloatButton.AddOccurrenceButton.TranslateTo(x, y, lenght);

            await _menuFloatButton.DetailOccurrenceButton.TranslateTo(x, y, lenght);
            await _menuFloatButton.DetailOccurrenceButton.TranslateTo(x, y, lenght);
        }

        private async Task ExecuteMainButtonAnimation(double rotation, string iconeName)
        {
            _viewModel.ImageSourceMainButton = ControlResources.GetImage(iconeName);
            await _menuFloatButton.MainButton.RotateTo(rotation, 100);
        }

        private async Task CloseFloatMenuButtonsLocked()
        {
            SetFalseValueToIsVisiblePropertyOfFloatButtons();

            await ExecuteMainButtonAnimation(0, "menu_24");

            _viewModel.IsOpenMenu = false;
        }

        private async Task OpenFloatMenuButtonsUnlocked()
        {
            SetTrueValueToIsVisiblePropertyOfFloatButtons(x: 0, y: -15);

            await ExecuteMainButtonAnimation(90, "close_24");

            _viewModel.IsOpenMenu = true;
        }

        #endregion

        #region Overrides

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            SetFalseToIsBusyViewModelBase();

            LoadUserLogged();

            await LoadPinsOfFirebase();

            LoadUserLocationAsync();

            LoadListCities();           

            SetsTheFullContentToView();

            ApplyModificationsToTheMap();

            SetFalseToIsBusyViewModelBase();
        }
                
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            EnsurePopupCloses();

            SetsTemporaryContentToView();
        }
       
        protected override void OnHandlerChanged()
        {
            // Handler to NOT leave the map Null after building the page.

            base.OnHandlerChanged();

#if IOS
            (map.Handler.PlatformView as MauiMkMapView).OverrideUserInterfaceStyle = UIKit.UIUserInterfaceStyle.Dark;

#elif ANDROID
           
            if (_map.Handler is MapHandler mapHandler && mapHandler.PlatformView is MapView mapView)
            {
                mapView.GetMapAsync(new OnMapReadyCallback());

                //If to Check that pins are loaded before configuring the handler
                if (_viewModel.PinsList != null && _viewModel.PinsList.Any())
                {
                    mapView.GetMapAsync(new OnPinReadyCallback(_viewModel.PinsList, _mainGrid));
                }
            }
#endif
        }

        #endregion        
    }   
}