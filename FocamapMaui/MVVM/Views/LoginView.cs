using FocamapMaui.Components.UI;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Extensions.Events;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;

namespace FocamapMaui.MVVM.Views
{
    public class LoginView : ContentPageBase
	{        
        public LoginView()
		{
			BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

			Content = BuildLoginView;
		}

        #region UI

        private static View BuildLoginView
        {
            get
            {
                var grid = CreateMainGrid();

                CreateLogo(grid);

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
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},                    
                },               
                Margin = 10
            };
        }

        private static void CreateLogo(Grid grid)
        {            
            var logo = new Image
            {
                Source = ImageSource.FromFile("logo.png"),
                Margin = new Thickness(30,30,30,50),
                HorizontalOptions = LayoutOptions.Center
            };
           
            grid.AddWithSpan(logo);
        }

        private static void CreateInputs(Grid grid)
        {            
            var stackInputs = GetStackLayoutBasic(spacing: 20, useMargin: true);

            var emailInput = new TextEditCustom(icon: "email_24", placeholder: "Email");
            stackInputs.Children.Add(emailInput);
           
            var passwordInput = new PasswordEditCustom(icon: "password_24", placeholder: "Senha");
            stackInputs.Children.Add(passwordInput);
            
            grid.AddWithSpan(stackInputs, 1);
        }

        private static void CreateButtons(Grid grid)
        {            
            var mainStackButtons = GetStackLayoutBasic(spacing: 20);

            var stackButtons = GetStackLayoutBasic();

            var enterButton = new ButtonCustom(text: "Entrar", textColor: "CLPrimary", backgroundColor: "CLPrimaryOrange");            

            stackButtons.Children.Add(enterButton);

            var seeMapButton = new ButtonCustom(text: "Ver Mapa", textColor: "CLPrimary", backgroundColor: "CLPrimaryWhite");

            stackButtons.Children.Add(seeMapButton);            

            var stackLabelButtons = GetStackLayoutBasic(spacing: 5);
           
            var forgotPasswordLabel = GetLabelBasic(text: "Esqueceu sua senha?");
            forgotPasswordLabel.AddTapGesture(ForgotPasswordTapGestureRecognizer_Tapped);
            stackLabelButtons.Children.Add(forgotPasswordLabel);

            var registerLabel = GetLabelBasic(text: "Cadastrar-se");
            registerLabel.AddTapGesture(RegisterTapGestureRecognizer_Tapped);
            stackLabelButtons.Children.Add(registerLabel);

            mainStackButtons.Children.Add(stackButtons);
            mainStackButtons.Children.Add(stackLabelButtons);
            
            grid.AddWithSpan(mainStackButtons, 2);            
        }
        
        public static StackLayout GetStackLayoutBasic(int spacing = 15, bool useMargin = false)
        {
            return new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = spacing,
                Margin = useMargin ? new Thickness(0, 0, 0, 50) : 0
            };
        }

        public static Label GetLabelBasic(string text, string textColor = "CLWhite")
        {
            return new Label
            {
                Text = text,
                TextColor = ControlResources.GetResource<Color>(textColor),
                FontSize = 16,
                HorizontalOptions = LayoutOptions.Center
            };
        }

        #endregion

        #region Events

        private static async void ForgotPasswordTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if(sender is View element)
            {
               await element.FadeAnimation();

               //todo - redirect to ForgotPasswordView;
            }
        }

        private static async void RegisterTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is View element)
            {
                await element.FadeAnimation();

                //todo - redirect to RegisterView;
            }
        }

        #endregion
    }
}

