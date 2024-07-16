namespace FocamapMaui.Controls.Extensions.Events
{
    public static class ControlEvents
	{
        public static void AddTapGesture(this View element, EventHandler<TappedEventArgs> tappedEventHandler)
        {
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };

            tapGestureRecognizer.Tapped += tappedEventHandler;                      
            element.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}