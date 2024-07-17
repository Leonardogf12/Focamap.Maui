using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Extensions.Events;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class HeaderWithIconAndImage : Grid
    {
        public Label LabelNameUser;

        public HeaderWithIconAndImage(string iconName, EventHandler iconGoBackEventHandler, EventHandler<TappedEventArgs> imageNameEventHandler)
		{
            var grid = CreateMainGrid();

            CreateGoBackIcon(grid, iconName, iconGoBackEventHandler);
                        
            CreateImageLetterWithEditAction(grid, imageNameEventHandler);

            CreateStackEmpty(grid);
           
            Children.Add(grid);
        }

        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new(){Width = GridLength.Star},
                    new(){Width = GridLength.Star},
                    new(){Width = GridLength.Star},
                },                
            };
        }

        private static void CreateGoBackIcon(Grid grid, string iconName, EventHandler iconGoBackEventHandler)
        {
            var buttonGoBack = CommomBasic.GetGoBackButton(iconName, iconGoBackEventHandler);
            buttonGoBack.FontSize = 50;

            grid.AddWithSpan(buttonGoBack);
        }

        private void CreateImageLetterWithEditAction(Grid grid, EventHandler<TappedEventArgs> iconEditEvent)
        {            
            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = -4
            };

            var frame = new Frame
            {
                BackgroundColor = ControlResources.GetResource<Color>("CLRoundImageLetterName"),
                BorderColor = Colors.Transparent,                
                WidthRequest = 100,
                HeightRequest = 100,
                CornerRadius = 50,
            };

            LabelNameUser = new Label
            {
                FontFamily = "MontserratBold",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 50,
                TextColor = Colors.White,
                HorizontalTextAlignment = TextAlignment.Center
            };

            frame.Content = LabelNameUser;
                                 
            var iconEdit = new Image
            {
                Source = ControlResources.GetImage("edit_24"),
                HeightRequest = 24,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0, -30, 0, 0),
            };
            iconEdit.AddTapGesture(iconEditEvent);

            stack.Children.Add(frame);
            stack.Children.Add(iconEdit);

            grid.AddWithSpan(stack,0, 1);
        }

        private static void CreateStackEmpty(Grid grid)
        {
            var empty = new StackLayout();

            grid.AddWithSpan(empty, 0, 2);
        }
    }
}

