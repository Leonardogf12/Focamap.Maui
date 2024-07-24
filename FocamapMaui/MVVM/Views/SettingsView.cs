using FocamapMaui.Components.UI;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Extensions.Animations;
using FocamapMaui.Controls.Extensions.Events;
using FocamapMaui.Controls.Resources;
using FocamapMaui.MVVM.Base;
using FocamapMaui.MVVM.ViewModels;
using FocamapMaui.Services.Navigation;
using Microsoft.Maui.Controls.Shapes;

namespace FocamapMaui.MVVM.Views
{
    public class SettingsView : ContentPageBase
	{
        public SettingsViewModel _viewModel;

        private readonly INavigationService _navigationService;

        public SettingsView(INavigationService navigationService)
        {
            _navigationService = navigationService;

            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");
            
            Content = BuildSettingsView;

            _viewModel = new(_navigationService);

            BindingContext = _viewModel;
        }

        #region UI

        public View BuildSettingsView
        {
            get
            {
                var grid = CreateMainGrid();

                CreateHeader(grid);

                CreateFrameMenuButtons(grid);

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
                },
                Margin = 10,
                RowSpacing = 30
            };
        }

        private void CreateHeader(Grid grid)
        {
            var header = new HeaderWithIconAndTitle(iconName: "arrow_back_24",
                            textTitle: "Configurações", iconEventHandler: GoBackButton_Clicked);

            grid.AddWithSpan(header);
        }

        private void CreateFrameMenuButtons(Grid grid)
        {
            var mainBorder = new Border
            {
                HeightRequest = 150,
                BackgroundColor = ControlResources.GetResource<Color>("CLSecondary"),
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 10
                },
                StrokeThickness = 0,
            };

            var contentGridMainBorder = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() { Height = GridLength.Star },
                    new() { Height = GridLength.Auto },
                    new() { Height = GridLength.Star }
                },                
            };

            CreateBorderButtonProfile(contentGridMainBorder);

            CreateSeparator(contentGridMainBorder);

            CreateBorderButtonLogOff(contentGridMainBorder);
           
            mainBorder.Content = contentGridMainBorder;

            grid.AddWithSpan(mainBorder, 1);
        }      

        private void CreateBorderButtonProfile(Grid contentGridMainBorder)
        {
            var borderProfile = new Border
            {             
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(10, 10, 0, 0),
                },
                StrokeThickness = 0,
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {

                        CreateHorizontalStackProfile()
                    }
                }
            };
            borderProfile.AddTapGesture(ProfileBorderButton);

            contentGridMainBorder.AddWithSpan(borderProfile,0);
        }
       
        private static void CreateSeparator(Grid contentGridMainBorder)
        {
            var line = new BoxView
            {
                HeightRequest = 1,                     
                BackgroundColor = Colors.Gray,
                Margin = new Thickness(30,0,10,0)
            };

            contentGridMainBorder.AddWithSpan(line,1);
        }

        private void CreateBorderButtonLogOff(Grid contentGridMainBorder)
        {
            var borderLogoff = new Border
            {              
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(0, 0, 10, 10),
                },
                StrokeThickness = 0,
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {

                        CreateHorizontalStackLogoff()
                    }
                }
            };
            borderLogoff.AddTapGesture(LogoffBorderButton);

            contentGridMainBorder.AddWithSpan(borderLogoff, 2);
        }
       
        private static HorizontalStackLayout CreateHorizontalStackProfile()
        {
            return new HorizontalStackLayout
            {                       
                Margin = new Thickness(10,0,0,0),
                Spacing = 10,
                Children = {
                     CreateIconToBorderButton("user_24"),
                     CreateTextsToBorderButton("Perfil", "Gerenciar Usuário")
                }
            };
        }

        private static HorizontalStackLayout CreateHorizontalStackLogoff()
        {
            return new HorizontalStackLayout
            {
                Margin = new Thickness(10, 0, 0, 0),
                Spacing = 10,
                Children = {
                    CreateIconToBorderButton("exit_24"),
                    CreateTextsToBorderButton("Sair", "Deslogar da conta")
                }
            };
        }

        private static VerticalStackLayout CreateTextsToBorderButton(string textTitle, string textSubtitle )
        {
            var verticalStack = new VerticalStackLayout
            {
                VerticalOptions = LayoutOptions.Center,               
            };

            var title = CommomBasic.GetLabelTitleBasic(title: textTitle);
            title.HorizontalOptions = LayoutOptions.Start;

            verticalStack.Children.Add(title);

            var subtitle = CommomBasic.GetLabelTitleBasic(title: textSubtitle, fontSize: 12, fontFamily: "MontserratRegular");
            subtitle.HorizontalOptions = LayoutOptions.Start;

            verticalStack.Children.Add(subtitle);

            return verticalStack;
        }

        private static Image CreateIconToBorderButton(string nameIcon)
        {
            return new Image
            {                
                Source = ControlResources.GetImage(nameIcon),
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 30,
                WidthRequest = 30
            };
        }

        #endregion

        #region Events

        private async void GoBackButton_Clicked(object sender, EventArgs e)
        {
            await _navigationService.GoBack();
        }

        private async void ProfileBorderButton(object sender, TappedEventArgs e)
        {
            if (sender is Border element)
            {
                await element.FadeAnimation();

                await _viewModel.GotoUserDetailView();
            }
        }

        private async void LogoffBorderButton(object sender, TappedEventArgs e)
        {
            if(sender is Border element)
            {
                await element.FadeAnimation();

                await _viewModel.LogOff();
            }
        }

        #endregion
    }
}