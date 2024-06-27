using FocamapMaui.Components.UI;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Extensions.Events;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;

namespace FocamapMaui.MVVM.Views
{
    public class RegisterView : ContentPageBase
	{
		public RegisterView()
		{
            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

            Content = BuildRegisterView;
        }

        #region UI

        public View BuildRegisterView
        {
            get
            {
                var grid = CreateMainGrid();

                CreateHeader(grid);

                CreateInputs(grid);

                CreateButtons(grid);

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
            var gridHeader = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new(){Width = GridLength.Star},
                    new(){Width = GridLength.Star},
                    new(){Width = GridLength.Star},
                },
            };

            var icon = new Image
            {
                Source = ImageSource.FromFile("back_24"),
                HeightRequest = 24,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(10, 0, 0, 0),
            };
            icon.AddTapGesture(BackButtonTapGestureRecognizer_Tapped);
            gridHeader.AddWithSpan(icon, 0, 0);

            var title = new Label
            {
                Text = "Registro",
                FontSize = 18,
                FontFamily = "MontserratSemibold",
                TextColor = ControlResources.GetResource<Color>("CLWhite"),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center
            };
            gridHeader.AddWithSpan(title, 0, 1);

            var empty = new StackLayout();
            gridHeader.AddWithSpan(empty, 0, 2);

            grid.AddWithSpan(gridHeader);
        }

        private static void CreateInputs(Grid grid)
        {
            var stackInputs = CommomBasic.GetStackLayoutBasic(spacing: 20);

            var emailInput = new TextEditCustom(icon: "email_24", placeholder: "Email");
            stackInputs.Children.Add(emailInput);

            var passwordInput = new PasswordEditCustom(icon: "password_24", placeholder: "Senha");
            stackInputs.Children.Add(passwordInput);

            var repasswordInput = new PasswordEditCustom(icon: "password_24", placeholder: "Repita a senha");
            stackInputs.Children.Add(repasswordInput);

            grid.AddWithSpan(stackInputs, 1);
        }

        private static void CreateButtons(Grid grid)
        {
            var enterButton = new PrimaryButtonCustom(text: "Registrar", textColor: "CLPrimary", backgroundColor: "CLPrimaryOrange");

            grid.AddWithSpan(enterButton, 2);
        }

        #endregion

        #region Events

        private async void BackButtonTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is Image element)
            {
                await element.FadeAnimation();

                await Shell.Current.Navigation.PopAsync();
            }
        }

        #endregion
    }
}

