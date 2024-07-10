using System.ComponentModel;
using Android.Gms.Maps;
using DevExpress.Maui.Controls;
using DevExpress.Maui.Editors;
using FocamapMaui.Components.UI;
using FocamapMaui.Components.Views;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Maps;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.ViewModels;
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

        private readonly IMapService _mapService;
       
        private HomeMapViewModel _viewModel;

        private Map _map = new();

        private Grid _mainGrid;

        private MenuFloatButtons _menuFloatButton;

        private Location _locationOfUserLogged;
     
        #endregion

        public HomeMapView(INavigationService navigationService, IMapService mapService)
		{           
            _navigationService = navigationService;
            _mapService = mapService;

            SetBackgroundColorToView();
            
            SetNavigationServiceInstancaFromViewModel(_navigationService, _mapService);

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
           _map = MapCustom.GetMap(MapType.Street, _locationOfUserLogged, Map_MapClicked, Map_PropertyChanged);

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
                ImageSource = ImageSource.FromFile("gps_24"),
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
                ImageSource = ImageSource.FromFile("plus_24"),
                BackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),
                CornerRadius = 0,
                HeightRequest = 45,
                WidthRequest = 45,
                FontSize = 30,                              
            };

            stackUpDowButtons.Children.Add(upZoom);

            var downZoom = new Button
            {
                ImageSource = ImageSource.FromFile("minus_24"),
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
            _menuFloatButton = new MenuFloatButtons(iconMainButton: "menu_24",                                                    
                                                    eventMainButton: MainButton_ClickedEvent,
                                                    commandExitButton: _viewModel.ExitCommand,
                                                    commandUserButton: _viewModel.UserDetailCommand,
                                                    commandAddOccurrenceButton: _viewModel.AddOccurrenceCommand,
                                                    commandDetailOccurrenceButton: _viewModel.DetailOccurrenceCommand);

            _menuFloatButton.MainButton.SetBinding(IsEnabledProperty, nameof(_viewModel.MainButtonIsEnabled), BindingMode.TwoWay);

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
            bottomSheetAddOccurrence.TextEditAddress.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.Address));
            bottomSheetAddOccurrence.DateEditDate.SetBinding(DateEdit.DateProperty, nameof(_viewModel.Date));
            bottomSheetAddOccurrence.HourEditHour.SetBinding(TimeEdit.TimeSpanProperty, nameof(_viewModel.Hour));
            bottomSheetAddOccurrence.MultilineEditResume.SetBinding(TextEditBase.TextProperty, nameof(_viewModel.Resume));
            
            grid.AddWithSpan(bottomSheetAddOccurrence, 0);
        }
        
        #endregion

        #region Events
      
        private void MainButton_ClickedEvent(object sender, EventArgs e)
        {
            if (!_viewModel.IsOpenMenu)
            {
                SetTrueValueToIsVisiblePropertyForFloatButtons(x: 0, y: -15);
                _viewModel.IsOpenMenu = true;
            }
            else
            {
                SetFalseValueToIsVisiblePropertyForFloatButtons();
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
        
        private void Map_MapClicked(object sender, MapClickedEventArgs e)
        {
            // EVENTO É DISPARADO QUANDO O USUARIO TOCA NO MAPA.
        }

        private void Map_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_map.VisibleRegion))
            {
                //IDENTIFICA QUANDO O USUARIO MUDA POSICAO - MOVIMENTACAO NO MAPA.       
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
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            EnsurePopupCloses();

            SetsTemporaryContentToView();            
        }

        private void SetsTemporaryContentToView()
        {
            Content = CreateTemporaryContent();
        }

        private void SetNavigationServiceInstancaFromViewModel(INavigationService navigationService, IMapService mapService)
        {
            _viewModel = new HomeMapViewModel(navigationService, mapService);
        }

        private void SetFalseToIsBusyViewModelBase()
        {
            //Set False because the modified Map handler does not recognize.
            (BindingContext as ViewModelBase).IsBusy = false;
        }

        private async Task LoadPinsOfFirebase() => await _viewModel.LoadPinsMock();
       
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

        private void SetsTheFullContentToView()
        {
            Content = BuildHomeMapView;

            _viewModel.Map = _map;
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

        private async void SetTrueValueToIsVisiblePropertyForFloatButtons(double x = 0, double y = 0, uint lenght = 20)
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

        private async void SetFalseValueToIsVisiblePropertyForFloatButtons(double x = 0, double y = 0, uint lenght = 100)
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