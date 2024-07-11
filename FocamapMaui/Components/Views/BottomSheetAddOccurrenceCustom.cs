using DevExpress.Maui.Controls;
using DevExpress.Maui.Editors;
using FocamapMaui.Components.UI;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.Views
{
    public class BottomSheetAddOccurrenceCustom : BottomSheet
    {
        #region Properties

        public TextEditCustom TextEditTitle;

        public TextEditCustom TextEditAddress;

        public DateEditCustom DateEditDate;

        public HourEditCustom HourEditHour;

        public MultilineEditCustom MultilineEditResume;

        public ChipButtonGroup ChipButtonGroup;

        #endregion

        public BottomSheetAddOccurrenceCustom(EventHandler eventHandler)
		{           
            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");
            GrabberColor = ControlResources.GetResource<Color>("CLPrimaryOrange");
            Content = BuildBottomSheet(eventHandler);
		}

        #region UI

        private Grid BuildBottomSheet(EventHandler eventHandler)
        {
            var grid = CreateMainGrid();

            CreateTitle(grid);

            CreateInputsGroup(grid);

            CreateSaveButton(grid, eventHandler);

            return grid;
        }

        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new(){Height = GridLength.Auto},
                    new(){Height = GridLength.Auto},
                    new(){Height = GridLength.Auto},
                },
                RowSpacing = 10,
                Margin = new Thickness(10, 0, 10, 10)
            };
        }

        private static void CreateTitle(Grid grid)
        {
            var title = CommomBasic.GetLabelTitleBasic(title: "Nova Ocorrência", fontSize: 16);

            grid.AddWithSpan(title);
        }

        private void CreateInputsGroup(Grid grid)
        {
            var gridInputs = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new() {Width = GridLength.Star},
                    new() {Width = GridLength.Star},
                },
                RowDefinitions = new RowDefinitionCollection
                {
                    new(){Height = GridLength.Auto},
                    new(){Height = GridLength.Auto},
                    new(){Height = GridLength.Auto},
                    new(){Height = GridLength.Auto},
                    new(){Height = GridLength.Auto},
                },
                RowSpacing = 10,
                ColumnSpacing = 8,
            };
           
            TextEditAddress = new TextEditCustom(icon: "map_24", placeholder: "Endereço do ocorrido");
            gridInputs.AddWithSpan(TextEditAddress, 0, 0, 1, 2);

            DateEditDate = new DateEditCustom(icon: "date_24", placeholder: "Data", useNativePicker: true);
            gridInputs.AddWithSpan(DateEditDate, 1, 0, 1, 1);

            HourEditHour = new HourEditCustom(icon: "time_24", placeholder: "Hora");
            gridInputs.AddWithSpan(HourEditHour, 1, 1, 1, 1);

            TextEditTitle = new TextEditCustom(icon: "title_24", placeholder: "Título do ocorrido");
            gridInputs.AddWithSpan(TextEditTitle, 2, 0, 1, 2);

            MultilineEditResume = new MultilineEditCustom(icon: "comment_24", placeholder: "Descreva o que houve",
                                minimumHeightRequest: 50, maximumHeigthRequest: 150, maxCharacterCount: 150, maxLineCount: 4);
            gridInputs.AddWithSpan(MultilineEditResume, 3, 0, 1, 2);

            ChipButtonGroup = new ChipButtonGroup();           
            gridInputs.AddWithSpan(ChipButtonGroup, 4, 0, 1, 2);
            
            grid.AddWithSpan(gridInputs, 1);
        }

        private static VerticalStackLayout CreatChipStatus()
        {
            var vertStackStatus = new VerticalStackLayout
            {
                Spacing = 10
            };

            var titleChipStaus = new Label
            {
                Text = "Gravidade da ocorrencia",
                FontSize = 16,
                FontFamily = "MontserratRegular",
                HorizontalTextAlignment = TextAlignment.Start,
            };
            vertStackStatus.Children.Add(titleChipStaus);

            var horiStackStatus = new HorizontalStackLayout
            {
                Spacing = 15,                
            };

            var lowButton = new Button
            {
                Text = "Baixo",
                TextColor = Colors.Gray,
                BorderColor = Colors.Gray,
                BackgroundColor = Colors.Transparent,
                IsEnabled = true,
                BorderWidth = 1,
                CornerRadius = 5,
                HeightRequest = 40,
                FontSize = 14,
                FontFamily = "MontserratRegular"
            };
            horiStackStatus.Children.Add(lowButton);

            var averageButton = new Button
            {
                Text = "Médio",
                TextColor = Colors.Gray,
                BorderColor = Colors.Gray,
                BackgroundColor = Colors.Transparent,      
                BorderWidth = 1,
                CornerRadius = 5,
                HeightRequest = 40,
                FontSize = 14,
                FontFamily = "MontserratRegular"
            };
            horiStackStatus.Children.Add(averageButton);

            var highButton = new Button
            {
                Text = "Alto",
                TextColor = ControlResources.GetResource<Color>("CLWhite"),
                BorderColor = ControlResources.GetResource<Color>("CLPrimaryOrange"),
                BackgroundColor = ControlResources.GetResource<Color>("CLSecondary"),
                IsEnabled = true,
                BorderWidth = 1,
                CornerRadius = 5,
                HeightRequest = 40,
                FontSize = 14,
                FontFamily = "MontserratRegular"
            };
            horiStackStatus.Children.Add(highButton);

            vertStackStatus.Children.Add(horiStackStatus);

            return vertStackStatus;
        }
        
        private static void CreateSaveButton(Grid grid, EventHandler eventHandler)
        {
            var SaveButton = new PrimaryButtonCustom(text: "Salvar", textColor: "CLPrimary", backgroundColor: "CLPrimaryWhite");
            SaveButton.Clicked += eventHandler;

            grid.AddWithSpan(SaveButton, 4);
        }

        #endregion       
    }    
}