using Android.Gms.Maps;
using DevExpress.Maui.Controls;
using FocamapMaui.Components.UI;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Maps;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.ViewModels;
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

        public HomeMapViewModel ViewModel;

        public Map map = new();

		#endregion

        public HomeMapView(INavigationService navigationService)
		{
            _navigationService = navigationService;

            SetNavigationServiceInstancaByViewModel();
            
            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

            Content = BuildHomeMapView;

            BindingContext = ViewModel;
        }
        
        #region UI

        public View BuildHomeMapView
        {
            get
            {
                var grid = CreateMainGrid();
               
                CreateMap(grid);

                CreateLockUnlockButton(grid);

                CreateGroupButtons(grid);

                CreateBottomSheetAddOccurrence(grid);
               
                return grid;
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
            map = new Map
            {                
                MapType = MapType.Street,
                IsScrollEnabled = true,
                IsTrafficEnabled = false,
                IsZoomEnabled = true,
                IsShowingUser = true,                
                BindingContext = ViewModel,
            };

            //map.MapClicked += Map_MapClicked;

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
        
        private void CreateGroupButtons(Grid grid)
        {
            var stackGroup = new StackLayout
            {
                Margin = new Thickness(10,0,0,100),
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

            grid.AddWithSpan(stackGroup, 0);
        }

        private void CreateBottomSheetAddOccurrence(Grid grid)
        {
            var bottomSheet = new BottomSheetAddOccurrenceCustom(eventHandler: CloseButtonBottomSheetTapGestureRecognizer_Tapped);            

            bottomSheet.SetBinding(BottomSheet.StateProperty, nameof(ViewModel.BottomSheetAddOccurrenceState), BindingMode.TwoWay);

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

        private void Map_MapClicked(object sender, MapClickedEventArgs e)
        {
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

        #endregion

        #region Actions

        private void SetNavigationServiceInstancaByViewModel()
        {
            ViewModel = new HomeMapViewModel(_navigationService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.Map = map;
           
            ViewModel.UpdateMapPins();
        }

        // Handler para nao deixar o mapa Nulo apos a construcao da pagina.
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
        
        #endregion
    }
}

