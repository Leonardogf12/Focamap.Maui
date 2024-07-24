using FocamapMaui.Components.UI.Basics;

namespace FocamapMaui.Components.UI
{
    public class HeaderWithIconAndTitle : Grid
	{
		public HeaderWithIconAndTitle(string iconName, string textTitle, EventHandler iconEventHandler )
		{
            var gridHeader = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new(){Width = GridLength.Star},
                    new(){Width = GridLength.Auto},
                    new(){Width = GridLength.Star},
                },
            };

            var button = CommomBasic.GetGoBackButton(iconName, iconEventHandler);

            gridHeader.AddWithSpan(button, 0, 0);

            var title = CommomBasic.GetLabelTitleBasic(title: textTitle);
            gridHeader.AddWithSpan(title, 0, 1);

            var empty = new StackLayout();
            gridHeader.AddWithSpan(empty, 0, 2);

            Children.Add(gridHeader);
        }
	}
}