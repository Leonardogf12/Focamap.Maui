using Microsoft.Maui.Controls.Maps;

namespace FocamapMaui.Models
{
    public class PinDto
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

        public string Status { get; set; }

        public string Address { get; set; }

        public string FullDate { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // Evento de clique do marcador
        public event EventHandler<MarkerClickedEventArgs> MarkerClicked;

        // Dispara o evento
        protected virtual void OnMarkerClicked(MarkerClickedEventArgs e)
        {
            MarkerClicked?.Invoke(this, e);
        }

        // Chama o evento de clique do marcador
        public void RaiseMarkerClickedEvent(MarkerClickedEventArgs e)
        {
            OnMarkerClicked(e);
        }
    }

    public class MarkerClickedEventArgs : EventArgs
    {
        public bool HideInfoWindow { get; set; }
    }
}

