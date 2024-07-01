using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Extensions.Events;

namespace FocamapMaui.Components.UI
{
    public class HeaderWithIconAndTitle : Grid
	{
		public HeaderWithIconAndTitle(string iconName, string textTitle, EventHandler<TappedEventArgs> iconEventHandler )
		{
            var gridHeader = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new(){Width = GridLength.Star},
                    new(){Width = GridLength.Star},
                    new(){Width = GridLength.Star},
                },
            };

            var icon = new Image
            {
                Source = ImageSource.FromFile(iconName),
                HeightRequest = 24,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(10, 0, 0, 0),
            };
            icon.AddTapGesture(iconEventHandler);
            gridHeader.AddWithSpan(icon, 0, 0);

            var title = CommomBasic.GetLabelTitleBasic(title: textTitle);
            gridHeader.AddWithSpan(title, 0, 1);

            var empty = new StackLayout();
            gridHeader.AddWithSpan(empty, 0, 2);

            Children.Add(gridHeader);
        }
	}
}

