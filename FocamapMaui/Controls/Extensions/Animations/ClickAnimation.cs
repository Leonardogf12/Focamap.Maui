using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Controls.Extensions.Animations
{
    public static class ClickAnimation
	{
		public static async Task FadeAnimation(this View element)
		{
			await element.FadeTo(opacity: 0.5);
            await Task.Delay(25);
            await element.FadeTo(opacity: 1, 100);
        }

        public static async Task BrightnessAnimation(this View element)
        {
            element.BackgroundColor = ControlResources.GetResource<Color>("CLBlack");
            element.Opacity = 0.3;
            await Task.Delay(220);
            element.BackgroundColor = ControlResources.GetResource<Color>("CLSecondary");
            element.Opacity = 1;
        }
    }
}