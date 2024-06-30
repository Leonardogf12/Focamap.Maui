using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;

namespace FocamapMaui.MVVM.Views
{
    public class OccurrencesHistoryView : ContentPageBase
	{
		public OccurrencesHistoryView()
		{
			BackgroundColor = ControlResources.GetResource<Color>("CLSecondary");

			Content = BuildOccurrencesHistoryView;
        }

        public View BuildOccurrencesHistoryView
		{
			get
			{
				return new VerticalStackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					HorizontalOptions = LayoutOptions.Center,
					Children =
					{
						new Label
						{
							FontSize = 12,
							Text = "Em construção - OccurrencesHistoryView"
                        }
					}
				};
			}
		}
    }
}

