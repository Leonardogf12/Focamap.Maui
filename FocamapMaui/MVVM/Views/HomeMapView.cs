using System.Collections.ObjectModel;
using System.ComponentModel;
using Android.Gms.Maps;
using DevExpress.Maui.Controls;
using DevExpress.Maui.Editors;
using FocamapMaui.Components.UI;
using FocamapMaui.Controls;
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

        public HomeMapViewModel ViewModel;

        public Map map = new();

        public Grid MainGrid;

        #endregion

        public HomeMapView(INavigationService navigationService, IMapService mapService)
		{
            _navigationService = navigationService;
            _mapService = mapService;
            
            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

            SetNavigationServiceInstancaFromViewModel(_navigationService, _mapService);

            Content = BuildHomeMapView;
            
            BindingContext = ViewModel;
        }
        
        #region UI

        public View BuildHomeMapView
        {
            get
            {
                MainGrid = CreateMainGrid();
                
                CreateMap(MainGrid);

                CreateLockUnlockButton(MainGrid);

                CreateMapGpsButton(MainGrid);

                CreateUpDownButton(MainGrid);

                CreateGroupButtons(MainGrid);

                CreateBottomSheetAddOccurrence(MainGrid);
                
                return MainGrid;
            }
        }

        private static Grid CreateMainGrid()
        {
            var grid = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() {Height = GridLength.Star},
                    new() {Height = GridLength.Auto},
                }
            };

            return grid;
        }

        private void CreateMap(Grid grid)
        {
            map = new Map
            {                
                MapType = MapType.Street,
                IsScrollEnabled = true,
                IsTrafficEnabled = false,
                IsZoomEnabled = true,
                IsShowingUser = true,                
                BindingContext = ViewModel,
            };
            map.MoveToRegion(MapSpan.FromCenterAndRadius(ControlMap.GetRegion(StringConstants.MG), Distance.FromKilometers(200)));
            //map.SetBinding(Map.IsShowingUserProperty, nameof(ViewModel.IsShowingUser), BindingMode.TwoWay);            
            map.MapClicked += Map_MapClicked;            
            map.PropertyChanged += Map_PropertyChanged;
                      
            grid.AddWithSpan(map);
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
            lockUnlockButton.SetBinding(Button.ImageSourceProperty, nameof(ViewModel.LockUnlockImage), BindingMode.TwoWay);
            lockUnlockButton.SetBinding(IsEnabledProperty, nameof(ViewModel.LockUnlockButtonIsEnabled), BindingMode.TwoWay);
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

        private void GpsButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsShowingUser = true;
        }
        
        private void CreateUpDownButton(Grid grid)
        {
            var stack = new VerticalStackLayout
            {             
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 5, 5),
            };

            var up = new Button
            {
                ImageSource = ImageSource.FromFile("plus_24"),
                BackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),
                CornerRadius = 0,
                HeightRequest = 45,
                WidthRequest = 45,
                FontSize = 30,                              
            };
            
            stack.Children.Add(up);

            var down = new Button
            {
                ImageSource = ImageSource.FromFile("minus_24"),
                BackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),
                CornerRadius = 0,
                HeightRequest = 45,
                WidthRequest = 45,
                FontSize = 30,
            };
            stack.Children.Add(down);

            grid.AddWithSpan(stack, 0);
        }

        private void CreateGroupButtons(Grid grid)
        {
            var stackGroup = new StackLayout
            {
                Margin = new Thickness(10, 0, 0, 10),
                Spacing = 10,
                Orientation = StackOrientation.Vertical,               
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start
            };

            var occurrenceButton = RoundButton.GetRoundButton(iconName: "occurrence_24");
            occurrenceButton.Command = ViewModel.SeeOccurrencesHistoryCommand;
            occurrenceButton.SetBinding(IsEnabledProperty, nameof(ViewModel.OccurrenceButtonIsEnabled), BindingMode.TwoWay);
            stackGroup.Children.Add(occurrenceButton);

            var addButton = RoundButton.GetRoundButton(iconName: "add_24");
            addButton.Command = ViewModel.AddOccurrenceCommand;
            addButton.SetBinding(IsEnabledProperty, nameof(ViewModel.AddButtonIsEnabled), BindingMode.TwoWay);
            stackGroup.Children.Add(addButton);

            var userButton = RoundButton.GetRoundButton("user_24");
            userButton.Command = ViewModel.UserDetailCommand;
            userButton.SetBinding(IsEnabledProperty, nameof(ViewModel.UserButtonIsEnabled), BindingMode.TwoWay);
            stackGroup.Children.Add(userButton);

            var exitButton = RoundButton.GetRoundButton("exit_24");
            exitButton.Command = ViewModel.ExitCommand;
            exitButton.SetBinding(IsEnabledProperty, nameof(ViewModel.ExitButtonIsEnabled), BindingMode.TwoWay);
            stackGroup.Children.Add(exitButton);

            grid.AddWithSpan(stackGroup, 0);
        }

        private void CreateBottomSheetAddOccurrence(Grid grid)
        {
            var bottomSheet = new BottomSheetAddOccurrenceCustom(eventHandler: SaveButton_Clicked);
            bottomSheet.SetBinding(BottomSheet.StateProperty, nameof(ViewModel.BottomSheetAddOccurrenceState), BindingMode.TwoWay);
            bottomSheet.TextEditAddress.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Address));
            bottomSheet.DateEditDate.SetBinding(DateEdit.DateProperty, nameof(ViewModel.Date));
            bottomSheet.HourEditHour.SetBinding(TimeEdit.TimeSpanProperty, nameof(ViewModel.Hour));
            bottomSheet.MultilineEditResume.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Resume));
            
            grid.AddWithSpan(bottomSheet, 0);
        }
        
        private Pin FindPinNearClick(Location position)
        {
            // Define a distância máxima para considerar que o clique foi em cima de um pin
            double toleranceInMeters = 0.05; //50 Metros

            // Percorre todos os pins no ViewModel
            foreach (var pin in ViewModel.PinsList)
            {
                // Calcula a distância entre o clique (position) e o pin atual
                double distance = Location.CalculateDistance(pin.Location, position, DistanceUnits.Kilometers);

                // Se a distância for menor que a tolerância, retorna o pin
                if (distance <= toleranceInMeters)
                {
                    return pin;
                }
            }

            // Retorna null se nenhum pin estiver próximo ao clique
            return null;
        }

        #endregion

        #region Events
        
        private void LockUnlockButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button element)
            {
                var nameImageSource = element.ImageSource.GetValue;

                var name = nameImageSource.Target;

                ViewModel.ChangeLockUnlokImage(name);
            }
        }

        private async void CloseButtonBottomSheetTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if(sender is Image element)
            {
                await ClickAnimation.FadeAnimation(element);

                ViewModel.BottomSheetAddOccurrenceState = BottomSheetState.Hidden;
            }            
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            await ViewModel.SaveOccurrence();
        }

        private void Map_MapClicked(object sender, MapClickedEventArgs e)
        {
            // EVENTO É DISPARADO QUANDO O USUARIO TOCA NO MAPA.

            /* 
             * TODO - Comentado para testes
             * 
             
            // Verifica se o clique está próximo a um pin existente
            var pinToRemove = FindPinNearClick(e.Location);

            if (pinToRemove != null)
            {
                // Remove o pin da lista de pins no ViewModel
                ViewModel.PinsList?.Remove(pinToRemove);

                // Atualiza os pins no mapa
                ViewModel.UpdateMapPins();

                return;
            }

            // Insere pin de teste.
            ViewModel.PinsList.Add(new Pin()
            {
                Label = "Pin Teste",
                Address = "Este pin é inserido toda vez que se clica no mapa.",
                Type = PinType.Place,
                Location = new Location(e.Location.Latitude, e.Location.Longitude)
            });

            ViewModel.UpdateMapPins();
            
            */
        }

        private void Map_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(map.VisibleRegion))
            {
                //IDENTIFICA QUANDO O USUARIO MUDA SE MOVIMENTACAO NO MAPA.       
            }
        }
       
        #endregion

        #region Actions

        private void SetNavigationServiceInstancaFromViewModel(INavigationService navigationService, IMapService mapService)
        {
            ViewModel = new HomeMapViewModel(navigationService, mapService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.Map = map;
           
            LoadPinsMockWithEvents();

            ViewModel.UpdateMapPins();
        }
       
        // Handler para NAO deixar o mapa Nulo apos a construcao da pagina.
        protected override void OnHandlerChanged()
        {
            
            base.OnHandlerChanged();

#if IOS
            (map.Handler.PlatformView as MauiMkMapView).OverrideUserInterfaceStyle = UIKit.UIUserInterfaceStyle.Dark;

#elif ANDROID

            if (map.Handler is MapHandler mapHandler && mapHandler.PlatformView is MapView mapView)
            {
                mapView.GetMapAsync(new OnMapReadyCallback());
            }
#endif
        }

        private void LoadPinsMockWithEvents()
        {
            CreatePinForMapComponent(ViewModel.PinsList);
        }

        private void CreatePinForMapComponent(ObservableCollection<Pin> list)
        {
            var listUpdatedWithEvent = new List<Pin>();

            foreach (var pinData in list)
            {
                var pin = new Pin
                {
                    Label = pinData.Label,
                    Address = pinData.Address,
                    Type = pinData.Type,
                    Location = new Location(pinData.Location.Latitude, pinData.Location.Longitude)
                };
                AddEventHandlersOnPins(pin);

                listUpdatedWithEvent.Add(pin);
            }

            ViewModel.PinsList = new ObservableCollection<Pin>(listUpdatedWithEvent);
        }

        private void AddEventHandlersOnPins(Pin pin)
        {
            pin.MarkerClicked += (s, args) =>
            {
                args.HideInfoWindow = true;
                string title = ((Pin)s).Label;
                string content = ((Pin)s).Address;

                var popup = new DxPopupCustom(title, content);

                MainGrid.AddWithSpan(popup);
                popup.IsOpen = true;
            };
        }

        #endregion
    }
}

