using System;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI.Basics
{
	public static class CommomBasic
	{
        public static StackLayout GetStackLayoutBasic(int spacing = 15, bool useMargin = false)
        {
            return new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = spacing,
                Margin = useMargin ? new Thickness(0, 0, 0, 50) : 0
            };
        }

        public static Label GetLabelTitleBasic(string title)
        {
            return new Label
            {
                Text = title,
                FontSize = 18,
                FontFamily = "MontserratSemibold",
                TextColor = ControlResources.GetResource<Color>("CLWhite"),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center
            };
        }
    }
}

