using System.ComponentModel;
using Android.Gms.Maps;
using DevExpress.Maui.Controls;
using DevExpress.Maui.Core;
using DevExpress.Maui.Editors;
using FocamapMaui.Components.UI;
using FocamapMaui.Components.Views;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Maps;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.ViewModels;
using FocamapMaui.Services.Firebase;
using FocamapMaui.Services.Map;
using FocamapMaui.Services.Navigation;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Maps.Handlers;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace FocamapMaui.MVVM.Views
{
    public class HomeMapView : ContentPageBase
	{
        #region Properties

        private readonly INavigationService _navigationService;
        
        private readonly IRealtimeDatabaseService _realtimeDatabaseService;

        private readonly IMapService _mapService;

        private HomeMapViewModel _viewModel;

        private Map _map = new();

        private Grid _mainGrid;

        private MenuFloatButtons _menuFloatButton;

        private Location _locationOfUserLogged;
     
        #endregion

        public HomeMapView(INavigationService navigationService,
                           IRealtimeDatabaseService realtimeDatabaseService, IMapService mapService)
		{           
            _navigationService = navigationService;            
            _realtimeDatabaseService = realtimeDatabaseService;
            _mapService = mapService;

            SetBackgroundColorToView();
            
            SetNavigationServiceInstancaFromViewModel(_navigationService, _realtimeDatabaseService, _mapService);

            SetsTemporaryContentToView();

            CreateLoadingPopupView(this, _viewModel);

            BindingContext = _viewModel;          
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

                CreateLockUnlockButton(_mainGrid);

                CreateMapGpsButton(_mainGrid);

                CreateUpDownButton(_mainGrid);

                CreateGroupButtons(_mainGrid);

                CreateBottomSheetAddOccurrence(_mainGrid);

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
                                   location: _locationOfUserLogged,
                                   eventMapClicked: Map_MapClicked,
                                   propertyChangedMap: Map_PropertyChanged);

            grid.AddWithSpan(_map);
        }
               
        private void CreateLockUnlockButton(Grid grid)
        {
            var stackGroup = new StackLayout
            {
                Margin = new Thickness(0, 10, 0, 0),
                Spacing = 10,
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };

            var lockUnlockButton = RoundButton.GetRoundButton(iconName: "lock_24", eventHandler: LockUnlockButton_Clicked);
            lockUnlockButton.SetBinding(Button.ImageSourceProperty, nameof(_viewModel.LockUnlockImage), BindingMode.TwoWay);
            lockUnlockButton.SetBinding(IsEnabledProperty, nameof(_viewModel.LockUnlockButtonIsEnabled), BindingMode.TwoWay);
            stackGroup.Children.Add(lockUnlockButton);
          
            grid.AddWithSpan(stackGroup, 0);
        }

        private void CreateMapGpsButton(Grid grid)
        {           
            var gpsButton= new Button
            {
                BackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),
                CornerRadius = 5,
                HeightRequest = 48,
                WidthRequest = 48,
                Margin = new Thickness(0, 5, 5, 0),                    
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.End,
                ImageSource = ControlResources.GetImage("gps_24"),
                FontSize = 30
            };
            gpsButton.Clicked += GpsButton_Clicked;

            grid.AddWithSpan(gpsButton, 0);
        }
        
        private static void CreateUpDownButton(Grid grid)
        {
            var stackUpDowButtons = new VerticalStackLayout
            {             
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 5, 5),
            };

            var upZoom = new Button
            {
                ImageSource = ControlResources.GetImage("plus_24"),
                BackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),
                CornerRadius = 0,
                HeightRequest = 45,
                WidthRequest = 45,
                FontSize = 30,                              
            };

            stackUpDowButtons.Children.Add(upZoom);

            var downZoom = new Button
            {
                ImageSource = ControlResources.GetImage("minus_24"),
                BackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),
                CornerRadius = 0,
                HeightRequest = 45,
                WidthRequest = 45,
                FontSize = 30,
            };
            stackUpDowButtons.Children.Add(downZoom);

            grid.AddWithSpan(stackUpDowButtons, 0);
        }
        
        private void CreateGroupButtons(Grid grid)
        {
            _menuFloatButton = new MenuFloatButtons(eventMainButton: MainButton_ClickedEvent,
                                                    commandExitButton: _viewModel.ExitViewCommand,
                                                    commandUserButton: _viewModel.GoToViewUserDetailCommand,
                                                    commandAddOccurrenceButton: _viewModel.OpenBottomSheetAddOccurrenceCommand,
                                                    commandDetailOccurrenceButton: _viewModel.GoToViewDetailOccurrenceCommand);

            _menuFloatButton.MainButton.SetBinding(IsEnabledProperty, nameof(_viewModel.MainButtonIsEnabled), BindingMode.TwoWay);
            _menuFloatButton.MainButton.SetBinding(Button.ImageSourceProperty, nameof(_viewModel.ImageSourceMainButton), BindingMode.TwoWay);

            _menuFloatButton.ExitButton.SetBinding(IsVisibleProperty, nameof(_viewModel.IsVisibleExitFloatButton), BindingMode.TwoWay);
            _menuFloatButton.ExitButton.SetBinding(IsEnabledProperty, nameof(_viewModel.ExitButtonIsEnabled), BindingMode.TwoWay);

            _menuFloatButton.UserButton.SetBinding(IsVisibleProperty, nameof(_viewModel.IsVisibleUserFloatButton), BindingMode.TwoWay);
            _menuFloatButton.UserButton.SetBinding(IsEnabledProperty, nameof(_viewModel.UserButtonIsEnabled), BindingMode.TwoWay);

            _menuFloatButton.AddOccurrenceButton.SetBinding(IsVisibleProperty, nameof(_viewModel.IsVisibleAddOccurrenceFloatButton), BindingMode.TwoWay);
            _menuFloatButton.AddOccurrenceButton.SetBinding(IsEnabledProperty, nameof(_viewModel.AddOccurrenceButtonIsEnabled), BindingMode.TwoWay);

            _menuFloatButton.DetailOccurrenceButton.SetBinding(IsVisibleProperty, nameof(_viewModel.IsVisibleDetailOccurrenceFloatButton), BindingMode.TwoWay);
            _menuFloatButton.DetailOccurrenceButton.SetBinding(IsEnabledProperty, nameof(_viewModel.DetailOccurrenceButtonIsEnabled), BindingMode.TwoWay);
           
            grid.AddWithSpan(_menuFloatButton, 0);
        }

        private void CreateBottomSheetAddOccurrence(Grid grid)
        {
            var bottomSheetAddOccurrence = new BottomSheetAddOccurrenceCustom(eventHandler: AddOccurrenceButton_Clicked);           
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
            bottomSheetAddOccurrence.TextEditAddress.EndIconClicked += TextEditAddress_EndIconClicked;

            grid.AddWithSpan(bottomSheetAddOccurrence, 0);
        }
        
        #endregion

        #region Events

        private async void MainButton_ClickedEvent(object sender, EventArgs e)
        {
            if (!_viewModel.IsOpenMenu)
            {
                SetTrueValueToIsVisiblePropertyOfFloatButtons(x: 0, y: -15);
                
                await ExecuteMainButtonAnimation(90, "close_24");

                _viewModel.IsOpenMenu = true;
            }
            else
            {
                SetFalseValueToIsVisiblePropertyOfFloatButtons();
                
                await ExecuteMainButtonAnimation(0, "menu_24");

                _viewModel.IsOpenMenu = false;
            }
        }
        
        private void GpsButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.IsShowingUser = true;
        }

        private void LockUnlockButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button element)
            {
                var nameImageSource = element.ImageSource.GetValue;

                var name = nameImageSource.Target;

                _viewModel.ChangeLockUnlokImage(name);
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

        private async void AddOccurrenceButton_Clicked(object sender, EventArgs e) => await _viewModel.SaveOccurrence();

        private void BottomSheetAddOccurrence_StateChanged(object sender, ValueChangedEventArgs<BottomSheetState> e)
        {
            if (e.NewValue.Equals(BottomSheetState.Hidden))
            {
                _viewModel.ClearInputsOfBottomSheetAddOccurrence();
            }
        }

        private void TextEditAddress_EndIconClicked(object sender, EventArgs e)
        {
            _viewModel.IsSelectingAddressOnMap = true;
            _viewModel.BottomSheetAddOccurrenceState = BottomSheetState.Hidden;
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

        #endregion

        #region Actions

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            SetFalseToIsBusyViewModelBase();

            await LoadPinsOfFirebase();

            await LoadUserLocationAsync();

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

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void SetNavigationServiceInstancaFromViewModel(INavigationService navigationService, IRealtimeDatabaseService realtimeDatabaseService, IMapService mapService)
        {
            _viewModel = new HomeMapViewModel(navigationService, realtimeDatabaseService, mapService);
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
       
        private async Task LoadUserLocationAsync()
        {
            try
            {
                _locationOfUserLogged = await _viewModel.GetLocationOfUserLogged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _locationOfUserLogged = new Location();
            }
        }
       
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
            _viewModel.IsVisibleExitFloatButton = true;
            await _menuFloatButton.ExitButton.TranslateTo(x, y, lenght);
            await _menuFloatButton.ExitButton.TranslateTo(x, y, lenght);

            _viewModel.IsVisibleUserFloatButton = true;
            await _menuFloatButton.UserButton.TranslateTo(x, y, lenght);
            await _menuFloatButton.UserButton.TranslateTo(x, y, lenght);

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
            _viewModel.IsVisibleUserFloatButton = false;
            _viewModel.IsVisibleExitFloatButton = false;

            await _menuFloatButton.ExitButton.TranslateTo(x, y, lenght);
            await _menuFloatButton.ExitButton.TranslateTo(x, y, lenght);

            await _menuFloatButton.UserButton.TranslateTo(x, y, lenght);
            await _menuFloatButton.UserButton.TranslateTo(x, y, lenght);

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