using DevExpress.Maui.Controls;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class DxPopupCustom : DXPopup
	{
        public DxPopupCustom(string title, string content)
        {
            IsOpen = true;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),
                HeightRequest = 320,
                WidthRequest = 250,               
                Children =
                {
                    new VerticalStackLayout
                    {
                        Margin = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Spacing = 10,
                        Children =
                        {
                            new Label
                            {
                                Text = title,
                                FontSize = 18,
                                TextColor = Colors.White,
                                HorizontalTextAlignment = TextAlignment.Center,
                                FontFamily = "MontserratSemibold",
                            },
                            new Label
                            {
                                Text = content,
                                FontSize = 16,
                                TextColor = Colors.White,
                                HorizontalTextAlignment = TextAlignment.Center,
                                FontFamily = "MontserratRegular",
                            },
                        }
                    },
                    
                }
            };
        }
	}
}

