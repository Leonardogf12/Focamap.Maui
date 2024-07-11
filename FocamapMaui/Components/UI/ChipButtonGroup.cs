namespace FocamapMaui.Components.UI
{
    public class ChipButtonGroup : VerticalStackLayout
    {
        public Button LowChipButton;

        public Button AverageChipButton;

        public Button HighChipButton;

        public ChipButtonGroup()
        {
            Children.Add(CreatChipStatus);
        }

        private VerticalStackLayout CreatChipStatus
        {
            get
            {
                var vertStackStatus = CreateMainVerticalStackLayout();
                
                vertStackStatus.Children.Add(CreateTitleChipStaus());

                var horiStackStatus = CreateMainHorizontalStackLayout();

                LowChipButton = CreateChipButton("Baixo");
                horiStackStatus.Children.Add(LowChipButton);

                AverageChipButton = CreateChipButton("Medio");
                horiStackStatus.Children.Add(AverageChipButton);

                HighChipButton = CreateChipButton("Alto");
                horiStackStatus.Children.Add(HighChipButton);

                vertStackStatus.Children.Add(horiStackStatus);

                return vertStackStatus;
            }
        }

       
        private static VerticalStackLayout CreateMainVerticalStackLayout()
        {
            return new VerticalStackLayout
            {
                Spacing = 10
            };
        }

        private static Label CreateTitleChipStaus()
        {
            return new Label
            {
                Text = "Gravidade da ocorrencia",
                FontSize = 16,
                FontFamily = "MontserratRegular",
                HorizontalTextAlignment = TextAlignment.Start,
            };
        }

        private static HorizontalStackLayout CreateMainHorizontalStackLayout()
        {
            return new HorizontalStackLayout
            {
                Spacing = 15,
            };
        }

        private static Button CreateChipButton(string text)
        {
            return new Button
            {
                Text = text,
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
        }
    }
}