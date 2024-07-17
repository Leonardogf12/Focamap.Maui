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

        public static Label GetLabelTitleBasic(string title, int fontSize = 18)
        {
            return new Label
            {
                Text = title,
                FontSize = fontSize,
                FontFamily = "MontserratSemibold",
                TextColor = ControlResources.GetResource<Color>("CLWhite"),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center
            };
        }

        public static Button GetGoBackButton(string iconName, EventHandler iconGoBackEventHandler)
        {
            var button = new Button
            {
                ImageSource = ControlResources.GetImage(iconName),
                HeightRequest = 50,
                WidthRequest = 50,
                CornerRadius = 50,
                Padding = 1,
                FontSize = 35,
                BackgroundColor = Colors.Transparent,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(5, 0, 0, 0),
            };
            button.Clicked += iconGoBackEventHandler;

            return button;
        }
    }
}

