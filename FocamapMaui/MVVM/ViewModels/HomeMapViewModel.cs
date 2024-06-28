using System.Collections.ObjectModel;
using FocamapMaui.MVVM.Base;
using Microsoft.Maui.Controls.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace FocamapMaui.MVVM.ViewModels
{
    public class HomeMapViewModel : ViewModelBase
	{
        #region Properties

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

        #endregion


        public HomeMapViewModel()
		{
            LoadPins();
        }

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

    }
}

