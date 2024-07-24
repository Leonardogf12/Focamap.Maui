using System.Windows.Input;
using DevExpress.Maui.Controls;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
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

        public BottomSheetAddOccurrenceCustom(ICommand commandButtonSave)
		{           
            BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");
            GrabberColor = ControlResources.GetResource<Color>("CLPrimaryOrange");
            Content = BuildBottomSheet(commandButtonSave);
		}

        #region UI

        private Grid BuildBottomSheet(ICommand command)
        {
            var grid = CreateMainGrid();

            CreateTitle(grid);

            CreateInputsGroup(grid);

            CreateSaveButton(grid, command);

            return grid;
        }

        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() { Height = GridLength.Auto },
                    new() { Height = GridLength.Auto },
                    new() { Height = GridLength.Auto },
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
            var stackVerticalInputs = new VerticalStackLayout
            {
                Spacing = 10
            };

            TextEditAddress = new TextEditCustom(startIcon: "map_24", endIcon: "button_map_24", placeholder: "Endereço do ocorrido");
            stackVerticalInputs.Add(TextEditAddress);

            var dateAndTime = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new() { Width = GridLength.Star },
                    new() { Width = GridLength.Star },                    
                },
                ColumnSpacing = 8,               
            };

            DateEditDate = new DateEditCustom(icon: "date_24", placeholder: "Data", useNativePicker: true);           
            dateAndTime.Add(DateEditDate, 0, 0);

            HourEditHour = new HourEditCustom(icon: "time_24", placeholder: "Hora");
            dateAndTime.AddWithSpan(HourEditHour, 0, 1);

            stackVerticalInputs.Add(dateAndTime);

            TextEditTitle = new TextEditCustom(startIcon: "title_24", endIcon: null, placeholder: "Título do ocorrido");
            stackVerticalInputs.Add(TextEditTitle);

            MultilineEditResume = new MultilineEditCustom(icon: "comment_24", placeholder: "Descreva o que houve",
                                minimumHeightRequest: 50, maximumHeigthRequest: 150, maxCharacterCount: 150, maxLineCount: 4);
            stackVerticalInputs.Add(MultilineEditResume);
           
            ChipButtonGroup = new ChipButtonGroup();
            stackVerticalInputs.Add(ChipButtonGroup);
            
            grid.AddWithSpan(stackVerticalInputs, 1);
        }

        private static void CreateSaveButton(Grid grid, ICommand command)
        {
            var SaveButton = new PrimaryButtonCustom(text: "Salvar", textColor: "CLPrimary", backgroundColor: "CLPrimaryWhite")
            {
                Command = command
            };

            grid.AddWithSpan(SaveButton, 4);
        }

        #endregion       
    }    
}