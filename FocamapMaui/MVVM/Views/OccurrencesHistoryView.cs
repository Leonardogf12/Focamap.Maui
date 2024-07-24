using FocamapMaui.Components.UI;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.Services.Navigation;

namespace FocamapMaui.MVVM.Views
{
    public class OccurrencesHistoryView : ContentPageBase
	{
        private readonly INavigationService _navigationService;

        public OccurrencesHistoryView(INavigationService navigationService)
		{
            _navigationService = navigationService;

            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

			Content = BuildOccurrencesHistoryView;
        }

        public View BuildOccurrencesHistoryView
		{
			get
			{
                var grid = CreateMainGrid();

                CreateHeader(grid);

                CreateTextInProgress(grid);

                return grid;
            }
		}

        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() {Height = 80},
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                },
                Margin = 10,
                RowSpacing = 30
            };
        }

        private void CreateHeader(Grid grid)
        {
            var header = new HeaderWithIconAndTitle(iconName: "arrow_back_24",
                            textTitle: "Ocorrências", iconEventHandler: GoBackButton_Clicked);

            grid.AddWithSpan(header);
        }

        private static void CreateTextInProgress(Grid grid)
        {
            var text = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,                
                Text = "Em breve",
            };

            grid.AddWithSpan(text, 1,0,3,1);
        }

        private async void GoBackButton_Clicked(object sender, EventArgs e) => await _navigationService.GoBack();
    }
}