using System.Windows.Input;

namespace FocamapMaui.Components.UI
{
    public class MenuFloatButtons : AbsoluteLayout
	{
		public Button MainButton;
        public Button UserButton;
        public Button AddOccurrenceButton;
        public Button DetailOccurrenceButton;
        public Button ExitButton;

        public MenuFloatButtons(EventHandler eventMainButton,
                                ICommand commandExitButton,
                                ICommand commandUserButton,
                                ICommand commandAddOccurrenceButton,
                                ICommand commandDetailOccurrenceButton)
        {
            Margin = new Thickness(10, 0, 0, 10);
            VerticalOptions = LayoutOptions.End;
            HorizontalOptions = LayoutOptions.Start;
            
            var mainGrid = CreateMainGrid();

            MainButton = CreateMainButton(eventMainButton);
            mainGrid.AddWithSpan(MainButton, 1);
          
            var detailButton = CreateDetailButtons(commandExitButton,
                                                   commandUserButton,
                                                   commandAddOccurrenceButton,
                                                   commandDetailOccurrenceButton);

            mainGrid.AddWithSpan(detailButton, 0);

            Children.Add(mainGrid);
        }

        private static Grid CreateMainGrid()
        {
            return new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new(){Height = GridLength.Star},
                    new(){Height = GridLength.Star},
                },
                Margin = new Thickness(0, 0, 0, 20),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                RowSpacing = 0,
            };
        }

        private static Button CreateMainButton(EventHandler eventMainButton)
        {
            var roundMainButton = RoundButton.GetRoundButton(iconName: "", eventHandler: eventMainButton);
            roundMainButton.FontSize = 24;

            return roundMainButton;
        }
        
        private Grid CreateDetailButtons(ICommand commandExitBtn,
                                         ICommand commandUserBtn,
                                         ICommand commandAddBtn,
                                         ICommand commandDetailBtn)
        {
            var gridDetailButtons = CreateDetailsButtonGrid();

            DetailOccurrenceButton = RoundButton.GetRoundButton(iconName: "occurrence_24", eventHandler: null, command: commandDetailBtn);
            gridDetailButtons.AddWithSpan(DetailOccurrenceButton);

            AddOccurrenceButton = RoundButton.GetRoundButton(iconName: "add_24", eventHandler: null, command: commandAddBtn);
            gridDetailButtons.AddWithSpan(AddOccurrenceButton, 1);

            UserButton = RoundButton.GetRoundButton(iconName: "user_24", eventHandler: null, command: commandUserBtn);
            gridDetailButtons.AddWithSpan(UserButton, 2);

            ExitButton = RoundButton.GetRoundButton(iconName: "exit_24", eventHandler: null, command: commandExitBtn);
            gridDetailButtons.AddWithSpan(ExitButton, 3);
                                             
            return gridDetailButtons;
        }

        private static Grid CreateDetailsButtonGrid()
        {
            return new Grid
            {                
                RowDefinitions = new RowDefinitionCollection
                {
                    new(){Height = GridLength.Star},
                    new(){Height = GridLength.Star},
                    new(){Height = GridLength.Star},
                    new(){Height = GridLength.Star},
                },               
                RowSpacing = 10
            };
        }
    }
}