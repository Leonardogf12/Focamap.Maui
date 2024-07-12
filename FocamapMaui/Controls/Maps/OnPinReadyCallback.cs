using System.Collections.ObjectModel;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using FocamapMaui.Components.UI;
using FocamapMaui.Helpers;
using FocamapMaui.Models.Map;

namespace FocamapMaui.Controls.Maps
{
    public class OnPinReadyCallback : Java.Lang.Object, IOnMapReadyCallback
    {
        public Grid mainGrid;     
        private readonly ObservableCollection<PinDto> pinsDto;
        private readonly Dictionary<string, PinDto> pinDtoDictionary;
        
        public OnPinReadyCallback(ObservableCollection<PinDto> pinsDto, Grid mainGrid)
        {
            this.mainGrid = mainGrid;          
            this.pinsDto = pinsDto;           
            this.pinDtoDictionary = new Dictionary<string, PinDto>();
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            //Clear original markers
            googleMap.Clear();

            foreach (var pin in pinsDto)
            {                
                var markerOptions = new MarkerOptions();
                markerOptions.SetPosition(new LatLng(pin.Latitude, pin.Longitude));

                //Load icon using drawable resource.
                var markerOptionsWithIcon = CreateIconWithMarkerOption(markerOptions, pin);

                var marker = googleMap.AddMarker(markerOptionsWithIcon);

                //Associates PinDto with the marker ID in the dictionary.
                pinDtoDictionary[marker.Id] = pin;                
            }

            AddEventHandlersOnPins(googleMap);            
        }
       
        private static MarkerOptions CreateIconWithMarkerOption(MarkerOptions markerOptions, PinDto pin)
        {            
            string icon = pin.Status switch
            {
                (int)PinStatus.Baixo => "marker_y31",
                (int)PinStatus.Medio => "marker_o31",
                _ => "marker_r31",
            };

            var context = Android.App.Application.Context;

            var resourceId = context.Resources.GetIdentifier(icon, "drawable", context.PackageName);

            if (resourceId != 0)
            {
                var bitmap = BitmapFactory.DecodeResource(context.Resources, resourceId);
                if (bitmap != null)
                {
                    markerOptions.SetIcon(BitmapDescriptorFactory.FromBitmap(bitmap));
                }
            }

            return markerOptions;
        }

        private void AddEventHandlersOnPins(GoogleMap googleMap)
        {
            googleMap.MarkerClick += (sender, args) =>
            {
                var clickedMarker = args.Marker;

                if (pinDtoDictionary.TryGetValue(clickedMarker.Id, out var pin))
                {
                    //This ensures that the default window will not be displayed
                    args.Handled = true;
                   
                    PinDto newPin = CreateModelToDxPopupPinCustom(pin);

                    string colorPopupHeader = PinStatusHelper.GetColorName(pin.Status);

                    var popup = new DxPopupPinCustom(colorHeader: colorPopupHeader, pin: newPin);

                    mainGrid.AddWithSpan(popup);

                    popup.IsOpen = true;
                }
            };
        }
      
        private static PinDto CreateModelToDxPopupPinCustom(PinDto pin)
        {
            return new()
            {
                Title = pin.Title,
                Content = pin.Content,
                Status = pin.Status,
                Address = pin.Address,
                FullDate = pin.FullDate,
                Latitude = pin.Latitude,
                Longitude = pin.Longitude
            };
        }
    }
}