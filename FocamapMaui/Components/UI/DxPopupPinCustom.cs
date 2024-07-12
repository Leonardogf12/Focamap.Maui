using DevExpress.Maui.Controls;
using FocamapMaui.Controls.Extensions.Events;
using FocamapMaui.Controls.Resources;
using FocamapMaui.Helpers;
using FocamapMaui.Models.Map;

namespace FocamapMaui.Components.UI
{
    public class DxPopupPinCustom : DXPopup
    {
        private readonly PinDto _pin;
      
        public DxPopupPinCustom(string colorHeader, PinDto pin)
        {
            _pin = pin;

            Grid grid = new()
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new(){ Height = 50 },
                    new(){ Height = GridLength.Auto },
                    new(){ Height = GridLength.Star },
                    new(){ Height = GridLength.Star },
                },                
                RowSpacing = 30,
                BackgroundColor = ControlResources.GetResource<Color>("CLPrimary"),
                MaximumHeightRequest = 400,
                MinimumHeightRequest = 345 * 0.5,
                MaximumWidthRequest = 280,
                MinimumWidthRequest = 280 * 0.5,
            };

            CreateHeaderTitle(grid, pin.Title, colorHeader);
                      
            CreateContent(grid, pin.Content);

            CreateStackIconWithText(grid, pin.Status, pin.Address, pin.FullDate);
           
            CreateSharedButton(grid);
            
            Content = grid;            
        }

        #region UI

        private static void CreateHeaderTitle(Grid grid, string title, string colorHeader)
        {
            var labelTitle = new Label
            {
                HeightRequest = 50,
                Text = title,
                BackgroundColor = ControlResources.GetResource<Color>(colorHeader),
                VerticalOptions = LayoutOptions.Center,
                FontSize = 16,
                FontFamily = "MontserratSemibold",
                TextColor = Colors.White,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            grid.AddWithSpan(labelTitle);
        }

        private static void CreateContent(Grid grid, string content)
        {
            var marginBase = CreateMarginBase();
            var labelContent = CreateLabelBase(content, 14);
            marginBase.Children.Add(labelContent);
            grid.AddWithSpan(marginBase, 1);
        }

        private static void CreateStackIconWithText(Grid grid, int pinStatus, string textAddress, string textFullDate)
        {
            var stackIconsTitles = new VerticalStackLayout
            {
                Margin = new Thickness(10, 0, 10, 0),
                Spacing = 5,
            };

            CreateLineStatus(stackIconsTitles, pinStatus);
            CreateLineAddress(stackIconsTitles, textAddress);
            CreateLineFullDate(stackIconsTitles, textFullDate);

            grid.AddWithSpan(stackIconsTitles, 2);
        }

        private static void CreateLineStatus(VerticalStackLayout stackIconsTitles, int pinStatus)
        {
            string text = PinStatusHelper.GetStatusName(pinStatus);
           
            var lineTextStatus = CreateGridForLineOfIconAndText("status_base_24", $"Risco {text}");

            stackIconsTitles.Children.Add(lineTextStatus);
        }

        private static void CreateLineAddress(VerticalStackLayout stackIconsTitles, string textAddress)
        {
            var lineAddress = CreateGridForLineOfIconAndText("map_24", textAddress);

            stackIconsTitles.Children.Add(lineAddress);
        }

        private static void CreateLineFullDate(VerticalStackLayout stackIconsTitles, string textFullDate)
        {
            var lineTextFullDate = CreateGridForLineOfIconAndText("date_24", textFullDate);

            stackIconsTitles.Children.Add(lineTextFullDate);
        }

        private void CreateSharedButton(Grid grid)
        {
            var sharedImage = new Image
            {
                Margin = new Thickness(10, 30, 10, 3),
                Source = ImageSource.FromFile("shared_24"),
                HeightRequest = 25,
            };
            sharedImage.AddTapGesture(EventTapShared_Tapped);

            grid.AddWithSpan(sharedImage, 3);
        }

        private static Label CreateLabelBase(string text, int fontSize = 18)
        {
            return new Label
            {
                Text = text,
                FontSize = fontSize,
                FontFamily = "MontserratSemibold",
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }

        private static Grid CreateGridForLineOfIconAndText(string iconName, string contentText)
        {
            var grid = new Grid
            {
                HeightRequest = 40,
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new() { Width = 40 },
                    new() { Width = GridLength.Star },
                },
                ColumnSpacing = 5
            };
            var icon = new Image
            {
                HeightRequest = 20,
                Source = ImageSource.FromFile(iconName),
            };

            grid.AddWithSpan(icon);

            var text = new Label
            {
                Text = contentText,
                FontSize = 14,
                TextColor = Colors.White,
                FontFamily = "MontserratRegular",
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                MaxLines = 2,
                LineBreakMode = LineBreakMode.CharacterWrap
            };
            grid.AddWithSpan(text, 0, 1);

            return grid;
        }

        private static StackLayout CreateMarginBase(int valueMargin = 10) => new() { Margin = new Thickness(valueMargin, 0, valueMargin, 0) };

        #endregion


        private async void EventTapShared_Tapped(object sender, TappedEventArgs e) => await OpenMapOnLocation(_pin);
      
        public static async Task OpenMapOnLocation(PinDto pin)
        {
            Location location = new(pin.Latitude, pin.Longitude);

            var options = new MapLaunchOptions { Name = "Microsoft Building 25" };

            try
            {
                await Microsoft.Maui.ApplicationModel.Map.Default.OpenAsync(location, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Ops", "Parece que algo deu errado com o compartilhamento desta ocorrencia. Tente novamente em alguns instantes.", "Ok");
            }
        }
    }
}