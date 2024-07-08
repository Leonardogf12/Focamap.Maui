using System.ComponentModel;
using Android.Gms.Maps;
using DevExpress.Maui.Controls;
using DevExpress.Maui.Editors;
using FocamapMaui.Components.UI;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Maps;
using FocamapMaui.Controls.Resources;
using FocamapMaui.Models;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.ViewModels;
using FocamapMaui.Services.Map;
using FocamapMaui.Services.Navigation;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Maps.Handlers;
using Circle = Microsoft.Maui.Controls.Maps.Circle;
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

        public MenuFloatButtons MenuFloatButton;

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
            var locationMock = new Location(-19.391820792436327, -40.049665587965364);

            map = MapCustom.GetMap(MapType.Street, locationMock, Map_MapClicked, Map_PropertyChanged);

            CreateCircleEmentToPinMOCK();

            grid.AddWithSpan(map);
        }

        private void CreateCircleEmentToPinMOCK()
        {
            Circle circleMed = new()
            {
                Center = new Location(-19.394837, -40.049279),
                Radius = new Distance(50),
                StrokeColor = ControlResources.GetResource<Color>("CLTOrangeBorder"),
                StrokeWidth = 8,
                FillColor = ControlResources.GetResource<Color>("CLTOrange"),
            };
            map.MapElements.Add(circleMed);

            Circle circleLow = new()
            {
                Center = new Location(-19.395747, -40.037993),
                Radius = new Distance(50),
                StrokeColor = ControlResources.GetResource<Color>("CLTYellowBorder"),
                StrokeWidth = 8,
                FillColor = ControlResources.GetResource<Color>("CLTYellow"),
            };
            map.MapElements.Add(circleLow);

            Circle circleHi = new()
            {
                Center = new Location(-19.400564, -40.045224),
                Radius = new Distance(50),
                StrokeColor = ControlResources.GetResource<Color>("CLTRedBorder"),
                StrokeWidth = 8,
                FillColor = ControlResources.GetResource<Color>("CLTRed"),
            };
            map.MapElements.Add(circleHi);
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
        
        private static void CreateUpDownButton(Grid grid)
        {
            var stack = new VerticalStackLayout
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
            
            stack.Children.Add(upZoom);

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
            MenuFloatButton = new MenuFloatButtons(iconMainButton: "menu_24", eventExitButton: ExitButton_ClickedEvent,
                                                  eventMainButton: MainButton_ClickedEvent, eventUserButton: UserButton_ClickedEvent,
                                                  eventAddOccurrenceButton: AddOccurrenceButton_ClickedEvent, eventDetailOccurrenceButton: DetailOccurrenceButton_ClickedEvent);
            MenuFloatButton.MainButton.SetBinding(IsEnabledProperty, nameof(ViewModel.MainButtonIsEnabled), BindingMode.TwoWay);

            MenuFloatButton.UserButton.SetBinding(IsVisibleProperty, nameof(ViewModel.IsVisibleUserFloatButton), BindingMode.TwoWay);
            MenuFloatButton.UserButton.SetBinding(IsEnabledProperty, nameof(ViewModel.UserButtonIsEnabled), BindingMode.TwoWay);

            MenuFloatButton.AddOccurrenceButton.SetBinding(IsVisibleProperty, nameof(ViewModel.IsVisibleAddOccurrenceFloatButton), BindingMode.TwoWay);
            MenuFloatButton.AddOccurrenceButton.SetBinding(IsEnabledProperty, nameof(ViewModel.AddButtonIsEnabled), BindingMode.TwoWay);

            MenuFloatButton.OccurrenceDetailButton.SetBinding(IsVisibleProperty, nameof(ViewModel.IsVisibleDetailOccurrenceFloatButton), BindingMode.TwoWay);
            MenuFloatButton.SetBinding(IsEnabledProperty, nameof(ViewModel.OccurrenceButtonIsEnabled), BindingMode.TwoWay);

            MenuFloatButton.ExitButton.SetBinding(IsVisibleProperty, nameof(ViewModel.IsVisibleExitFloatButton), BindingMode.TwoWay);
            MenuFloatButton.ExitButton.SetBinding(IsEnabledProperty, nameof(ViewModel.ExitButtonIsEnabled), BindingMode.TwoWay);

            grid.AddWithSpan(MenuFloatButton, 0);
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
        
        private PinDto FindPinNearClick(Location position)
        {
            // Define a distância máxima para considerar que o clique foi em cima de um pin
            double toleranceInMeters = 0.05; //50 Metros

            // Percorre todos os pins no ViewModel
            foreach (var pin in ViewModel.PinsList)
            {
                var location = new Location(pin.Latitude, pin.Longitude);

                // Calcula a distância entre o clique (position) e o pin atual
                double distance = Location.CalculateDistance(location, position, DistanceUnits.Kilometers);

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
      
        private void MainButton_ClickedEvent(object sender, EventArgs e)
        {
            if (!ViewModel.IsOpenMenu)
            {
                SetTrueValueToIsVisiblePropertyForFloatButtons(x: 0, y: -15);
                ViewModel.IsOpenMenu = true;
            }
            else
            {
                SetFalseValueToIsVisiblePropertyForFloatButtons();
                ViewModel.IsOpenMenu = false;
            }           
        }

        private async void SetTrueValueToIsVisiblePropertyForFloatButtons(double x = 0, double y = 0, uint lenght = 20)
        {
            ViewModel.IsVisibleExitFloatButton = true;
            await MenuFloatButton.ExitButton.TranslateTo(x, y, lenght);
            await MenuFloatButton.ExitButton.TranslateTo(x, y, lenght);

            ViewModel.IsVisibleUserFloatButton = true;
            await MenuFloatButton.UserButton.TranslateTo(x, y, lenght);
            await MenuFloatButton.UserButton.TranslateTo(x, y, lenght);

            ViewModel.IsVisibleAddOccurrenceFloatButton = true;
            await MenuFloatButton.AddOccurrenceButton.TranslateTo(x, y, lenght);
            await MenuFloatButton.AddOccurrenceButton.TranslateTo(x, y, lenght);

            ViewModel.IsVisibleDetailOccurrenceFloatButton = true;
            await MenuFloatButton.OccurrenceDetailButton.TranslateTo(x, y, lenght);   
            await MenuFloatButton.OccurrenceDetailButton.TranslateTo(x, y, lenght);                                                         
        }

        private async void SetFalseValueToIsVisiblePropertyForFloatButtons(double x = 0, double y = 0, uint lenght = 100)
        {
            ViewModel.IsVisibleDetailOccurrenceFloatButton = false;
            ViewModel.IsVisibleAddOccurrenceFloatButton = false;
            ViewModel.IsVisibleUserFloatButton = false;
            ViewModel.IsVisibleExitFloatButton = false;

            await MenuFloatButton.ExitButton.TranslateTo(x, y, lenght);
            await MenuFloatButton.ExitButton.TranslateTo(x, y, lenght);

            await MenuFloatButton.UserButton.TranslateTo(x, y, lenght);
            await MenuFloatButton.UserButton.TranslateTo(x, y, lenght);

            await MenuFloatButton.AddOccurrenceButton.TranslateTo(x, y, lenght);
            await MenuFloatButton.AddOccurrenceButton.TranslateTo(x, y, lenght);

            await MenuFloatButton.OccurrenceDetailButton.TranslateTo(x, y, lenght);
            await MenuFloatButton.OccurrenceDetailButton.TranslateTo(x, y, lenght);                                        
        }

        private void UserButton_ClickedEvent(object sender, EventArgs e)
        {
            ViewModel.OnUserDetailCommand();
        }

        private void AddOccurrenceButton_ClickedEvent(object sender, EventArgs e)
        {
            ViewModel.OnAddOccurrenceCommand();
        }

        private void DetailOccurrenceButton_ClickedEvent(object sender, EventArgs e)
        {
            ViewModel.OnSeeOccurrencesHistoryCommand();
        }

        private void ExitButton_ClickedEvent(object sender, EventArgs e)
        {
            ViewModel.OnExitCommand();
        }

        private void GpsButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsShowingUser = true;
        }

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
              
                mapView.GetMapAsync(new OnPinReadyCallback(ViewModel.PinsList, MainGrid));
            }            
#endif
        }
       
        #endregion
    }
}

