using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;

namespace FocamapMaui.MVVM.Views
{
    public class LoginView : ContentPageBase
	{
		public LoginView()
		{
			BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

            Content = new VerticalStackLayout()
			{				
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Children =
				{
					new Label
					{
						Text = "Login"
					}
				}
			};
		}
	}
}

