using System;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;

namespace FocamapMaui.MVVM.Views
{
	public class UserDetailView : ContentPageBase
	{
		public UserDetailView()
		{
			BackgroundColor = ControlResources.GetResource<Color>("CLSecondary");
			Content = BuildUserDetailView;
        }

		public View BuildUserDetailView
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
                            Text = "Em construção - UserDetailView"
                        }
                    }
                };
            }
		}

    }
}

