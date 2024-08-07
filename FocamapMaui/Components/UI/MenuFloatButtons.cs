﻿using System.Windows.Input;

namespace FocamapMaui.Components.UI
{
    public class MenuFloatButtons : AbsoluteLayout
	{
		public Button MainButton;
        public Button SettingsButton;
        public Button AddOccurrenceButton;
        public Button DetailOccurrenceButton;      

        public MenuFloatButtons(EventHandler eventMainButton,                              
                                ICommand commandSettingsButton,
                                ICommand commandAddOccurrenceButton,
                                ICommand commandDetailOccurrenceButton)
        {
            Margin = new Thickness(10, 0, 0, 10);
            VerticalOptions = LayoutOptions.End;
            HorizontalOptions = LayoutOptions.Start;
            
            var mainGrid = CreateMainGrid();

            MainButton = CreateMainButton(eventMainButton);
            mainGrid.AddWithSpan(MainButton, 1);
          
            var detailButton = CreateDetailButtons(commandSettingsButton, commandAddOccurrenceButton, commandDetailOccurrenceButton);

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
        
        private Grid CreateDetailButtons(ICommand commandSettingsBtn,
                                         ICommand commandAddBtn, ICommand commandDetailBtn)
        {
            var gridDetailButtons = CreateDetailsButtonGrid();

            DetailOccurrenceButton = RoundButton.GetRoundButton(iconName: "occurrence_24", eventHandler: null, command: commandDetailBtn);
            gridDetailButtons.AddWithSpan(DetailOccurrenceButton);

            AddOccurrenceButton = RoundButton.GetRoundButton(iconName: "add_24", eventHandler: null, command: commandAddBtn);
            gridDetailButtons.AddWithSpan(AddOccurrenceButton, 1);

            SettingsButton = RoundButton.GetRoundButton(iconName: "settings_32", eventHandler: null, command: commandSettingsBtn);
            SettingsButton.FontSize = 30;

            gridDetailButtons.AddWithSpan(SettingsButton, 2);          
                                             
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