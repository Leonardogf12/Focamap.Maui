using FocamapMaui.Components.UI;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Extensions.Events;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.Services.Navigation;

namespace FocamapMaui.MVVM.Views
{
    public class LoginView : ContentPageBase
	{
        #region Properties

        private readonly INavigationService _navigationService;

        #endregion

        public LoginView(INavigationService navigationService)
		{
            _navigationService = navigationService;

			BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

			Content = BuildLoginView;
		}

        #region UI

        private View BuildLoginView
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
            var stackInputs = CommomBasic.GetStackLayoutBasic(spacing: 20, useMargin: true);

            var emailInput = new TextEditCustom(icon: "email_24", placeholder: "Email");
            stackInputs.Children.Add(emailInput);
           
            var passwordInput = new PasswordEditCustom(icon: "password_24", placeholder: "Senha");
            stackInputs.Children.Add(passwordInput);
            
            grid.AddWithSpan(stackInputs, 1);
        }

        private void CreateButtons(Grid grid)
        {            
            var mainStackButtons = CommomBasic.GetStackLayoutBasic(spacing: 20);

            var stackButtons = CommomBasic.GetStackLayoutBasic();

            var enterButton = new PrimaryButtonCustom(text: "Entrar", textColor: "CLPrimary", backgroundColor: "CLPrimaryOrange");            

            stackButtons.Children.Add(enterButton);

            var seeMapButton = new PrimaryButtonCustom(text: "Ver Mapa", textColor: "CLPrimary", backgroundColor: "CLPrimaryWhite");

            stackButtons.Children.Add(seeMapButton);            

            var stackLabelButtons = CommomBasic.GetStackLayoutBasic(spacing: 5);
          
            var forgotPasswordLabel = GetLabelBasic(text: "Esqueceu sua senha?");
            forgotPasswordLabel.AddTapGesture(ForgotPasswordLabelTapGestureRecognizer_Tapped);

            stackLabelButtons.Children.Add(forgotPasswordLabel);

            var registerLabel = GetLabelBasic(text: "Cadastrar-se");
            registerLabel.AddTapGesture(RegisterTapGestureRecognizer_Tapped);

            stackLabelButtons.Children.Add(registerLabel);

            mainStackButtons.Children.Add(stackButtons);
            mainStackButtons.Children.Add(stackLabelButtons);
            
            grid.AddWithSpan(mainStackButtons, 2);            
        }

        private async void ForgotPasswordLabelTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is View element)
            {
                await element.FadeAnimation();

                //todo - redirect to ForgotPasswordView;
            }
        }
        
        public static Label GetLabelBasic(string text)
        {
            return new Label
            {
                Text = text,
                TextColor = ControlResources.GetResource<Color>("CLWhite"),
                FontSize = 16,
                HorizontalOptions = LayoutOptions.Center
            };           
        }
       
        #endregion

        #region Events

        private async void ForgotPasswordTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if(sender is View element)
            {
               await element.FadeAnimation();

               //todo - redirect to ForgotPasswordView;
            }
        }
      
        private async void RegisterTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is View element)
            {
                await element.FadeAnimation();

                await _navigationService.NavigationWithParameter<RegisterView>();
            }
        }

        #endregion

        #region Actions
        

        #endregion
    }
}

