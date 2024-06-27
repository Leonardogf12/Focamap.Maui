using System;
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
    }
}

