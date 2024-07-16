using DevExpress.Maui.Editors;
using FocamapMaui.Components.UI;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Extensions.Events;
using FocamapMaui.Controls.Resources;
using FocamapMaui.Helpers;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.ViewModels;
using FocamapMaui.Services.Authentication;
using FocamapMaui.Services.Navigation;

namespace FocamapMaui.MVVM.Views
{
    public class LoginView : ContentPageBase
	{
        #region Properties

        private readonly IAuthenticationService _authenticationService;

        private readonly INavigationService _navigationService;

        public LoginViewModel ViewModel;

        public TextEditCustom EmailTextEdit;

        public PasswordEditCustom PasswordTextEdit;

        #endregion

        public LoginView(INavigationService navigationService, IAuthenticationService authenticationService)
		{
            _navigationService = navigationService;
            _authenticationService = authenticationService;

            ViewModel = new(_authenticationService);

            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");

            Content = BuildLoginView;

            CreateLoadingPopupView(this, ViewModel);

            BindingContext = ViewModel;
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
                Source = ControlResources.GetImage("logo.png"),
                Margin = new Thickness(30,30,30,50),
                HorizontalOptions = LayoutOptions.Center
            };
           
            grid.AddWithSpan(logo);
        }

        private void CreateInputs(Grid grid)
        {            
            var stackInputs = CommomBasic.GetStackLayoutBasic(spacing: 20, useMargin: true);

            EmailTextEdit = new TextEditCustom(startIcon: "email_24", endIcon: null, placeholder: "Email", keyboard: Keyboard.Email);
            EmailTextEdit.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Email));
            EmailTextEdit.SetBinding(EditBase.BorderColorProperty, nameof(ViewModel.BorderColorEmailInput), BindingMode.TwoWay);
            //EmailTextEdit.TextChanged += EmailInput_TextChanged;
            stackInputs.Children.Add(EmailTextEdit);

            PasswordTextEdit = new PasswordEditCustom(icon: "password_24", placeholder: "Senha");
            PasswordTextEdit.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Password));
            PasswordTextEdit.SetBinding(EditBase.BorderColorProperty, nameof(ViewModel.BorderColorPasswordInput), BindingMode.TwoWay);
            //PasswordTextEdit.TextChanged += PasswordInput_TextChanged;
            stackInputs.Children.Add(PasswordTextEdit);
            
            grid.AddWithSpan(stackInputs, 1);
        }

      
        private void CreateButtons(Grid grid)
        {            
            var mainStackButtons = CommomBasic.GetStackLayoutBasic(spacing: 20);

            var stackButtons = CommomBasic.GetStackLayoutBasic();

            var enterButton = new PrimaryButtonCustom(text: "Entrar", textColor: "CLPrimary", backgroundColor: "CLPrimaryOrange");
            enterButton.Clicked += EnterButton_Clicked;
            stackButtons.Children.Add(enterButton);

            var seeMapButton = new PrimaryButtonCustom(text: "Ver Mapa", textColor: "CLPrimary", backgroundColor: "CLPrimaryWhite");            
            seeMapButton.Clicked += SeeMapButton_Clicked;

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

        private void EmailInput_TextChanged(object sender, EventArgs e) => ViewModel.CheckIfInputsAreOk();        

        private void PasswordInput_TextChanged(object sender, EventArgs e) => ViewModel.CheckIfInputsAreOk();

        private async void EnterButton_Clicked(object sender, EventArgs e)
        {
            EmailTextEdit.Unfocus();
            PasswordTextEdit.Unfocus();

            await ViewModel.Login();
        }

        private async void SeeMapButton_Clicked(object sender, EventArgs e)
        {           
            var param = ParameterHelper.SetParameter(StringConstants.ANONYMOUS_ACCESS, true);

            await _navigationService.NavigationWithParameter<HomeMapView>(parameter: param);        
        }

        private async void ForgotPasswordLabelTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is View element)
            {
                await element.FadeAnimation();

                await _navigationService.NavigationWithParameter<ForgotPasswordView>();
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
    }
}