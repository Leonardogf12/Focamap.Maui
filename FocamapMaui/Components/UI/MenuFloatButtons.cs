namespace FocamapMaui.Components.UI
{
    public class MenuFloatButtons : AbsoluteLayout
	{
		public Button MainButton;
        public Button UserButton;
        public Button AddOccurrenceButton;
        public Button OccurrenceDetailButton;
        public Button ExitButton;

        public MenuFloatButtons(string iconMainButton, EventHandler eventMainButton, EventHandler eventExitButton,
                                EventHandler eventUserButton, EventHandler eventAddOccurrenceButton,
                                EventHandler eventDetailOccurrenceButton)
        {
            Margin = new Thickness(10, 0, 0, 10);
            VerticalOptions = LayoutOptions.End;
            HorizontalOptions = LayoutOptions.Start;
            
            var mainGrid = CreateMainGrid();

            MainButton = CreateMainButton(iconMainButton, eventMainButton);
            mainGrid.AddWithSpan(MainButton, 1);
          
            var detailButton = CreateDetailButtons(eventExitButton, eventUserButton, eventAddOccurrenceButton, eventDetailOccurrenceButton);
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

        private static Button CreateMainButton(string iconMainButton, EventHandler eventMainButton)
        {
            var roundMainButton = RoundButton.GetRoundButton(iconMainButton, eventMainButton);            

            return roundMainButton;
        }
        
        private Grid CreateDetailButtons(EventHandler eventExitBtn, EventHandler eventUserBtn, EventHandler eventAddBtn, EventHandler eventDetailBtn)
        {
            var gridDetailButtons = CreateDetailsButtonGrid();

            OccurrenceDetailButton = RoundButton.GetRoundButton("occurrence_24", eventDetailBtn);
            gridDetailButtons.AddWithSpan(OccurrenceDetailButton);

            AddOccurrenceButton = RoundButton.GetRoundButton("add_24", eventAddBtn);
            gridDetailButtons.AddWithSpan(AddOccurrenceButton, 1);

            UserButton = RoundButton.GetRoundButton("user_24", eventUserBtn);
            gridDetailButtons.AddWithSpan(UserButton, 2);

            ExitButton = RoundButton.GetRoundButton("exit_24", eventExitBtn);
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

