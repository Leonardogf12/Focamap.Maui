using DevExpress.Maui.Controls;
using FocamapMaui.Components.UI.Basics;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class BottomSheetAddOccurrenceCustom : BottomSheet
    {
		public BottomSheetAddOccurrenceCustom(EventHandler<TappedEventArgs> eventHandler)
		{
			BackgroundColor = ControlResources.GetResource<Color>("CLPrimary");
            GrabberColor = ControlResources.GetResource<Color>("CLPrimaryOrange");
            Content = BuildBottomSheet(eventHandler);
		}

		private Grid BuildBottomSheet(EventHandler<TappedEventArgs> eventHandler)
		{			
			var grid = CreateMainGrid();

            CreateTitle(grid);

            CreateInputsGroup(grid);

			CreateSaveButton(grid);

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
                },
                RowSpacing = 10,
                ColumnSpacing = 8,
            };

            var addressInput = new TextEditCustom(icon: "map_24", placeholder: "Endereço");
            //addressInput.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Address));
            gridInputs.AddWithSpan(addressInput, 0, 0, 1, 2);

            var dateInput = new DateEditCustom(icon: "date_24", placeholder: "Data", useNativePicker: true);
            //dateInput.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Date));
            gridInputs.AddWithSpan(dateInput, 1, 0, 1, 1);
          
            var hourInput = new HourEditCustom(icon: "time_24", placeholder: "Hora");
            //hourInput.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Hour));
            gridInputs.AddWithSpan(hourInput, 1, 1, 1, 1);

            var resumeInput = new MultilineEditCustom(icon: "comment_24", placeholder: "Breve resumo",
                                minimumHeightRequest: 50, maximumHeigthRequest: 150, maxCharacterCount:150, maxLineCount: 4);
            //resumeInput.SetBinding(TextEditBase.TextProperty, nameof(ViewModel.Resume));           
            gridInputs.AddWithSpan(resumeInput, 2, 0, 1, 2);

            grid.AddWithSpan(gridInputs, 1);
        }

        private void CreateSaveButton(Grid grid)
        {
            var SaveButton = new PrimaryButtonCustom(text: "Salvar", textColor: "CLPrimary", backgroundColor: "CLPrimaryWhite");
            SaveButton.Clicked += SaveButton_Clicked;

            grid.AddWithSpan(SaveButton, 2);
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}

