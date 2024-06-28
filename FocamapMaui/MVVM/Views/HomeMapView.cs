using System.Reflection;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Maps.Handlers;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace FocamapMaui.MVVM.Views
{
	public class HomeMapView : ContentPageBase
	{
        #region Properties

        public HomeMapViewModel ViewModel = new();

        public Map map = new();

		#endregion

        public HomeMapView()
		{
            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

            Content = BuildHomeMapView;

			BindingContext = ViewModel;
        }


		public View BuildHomeMapView
		{
			get
			{
				var grid = new Grid();

                map = new Map
                {
                    MapType = MapType.Street,
                    IsScrollEnabled = true,
                    IsTrafficEnabled = false,
                    IsZoomEnabled = true,
                    IsShowingUser = true,
                    BindingContext = ViewModel,
                };

                map.MapClicked += Map_MapClicked;

                grid.AddWithSpan(map);

                return grid;
            }
		}

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


        #region Actions

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.Map = map;

            ViewModel.UpdateMapPins();
        }

        protected override void OnHandlerChanged()
        {
            // Handler para nao deixar o mapa Null apos a construcao da pagina.
            base.OnHandlerChanged();

#if IOS
            (map.Handler.PlatformView as MauiMkMapView).OverrideUserInterfaceStyle = UIKit.UIUserInterfaceStyle.Dark;

#elif ANDROID

            var mapHandler = map.Handler as MapHandler;

            if (mapHandler != null && mapHandler.PlatformView is MapView mapView)
            {
                mapView.GetMapAsync(new OnMapReadyCallback());
            }
#endif
        }

        private class OnMapReadyCallback : Java.Lang.Object, IOnMapReadyCallback
        {
            public void OnMapReady(GoogleMap googleMap)
            {
                var assembly = Assembly.GetExecutingAssembly();

                // Busca o tema direto da pasta GoogleMapsSampleMaui.Resources.Maps.Styles
                using var stream = assembly.GetManifestResourceStream("FocamapMaui.Resources.Maps.night_map_style.json");

                string json = string.Empty;
                using (var reader = new StreamReader(stream!))
                {
                    json = reader.ReadToEnd();
                }

                // Aplica o tema.
                googleMap.SetMapStyle(new MapStyleOptions(json));
            }
        }

        #endregion
    }
}

