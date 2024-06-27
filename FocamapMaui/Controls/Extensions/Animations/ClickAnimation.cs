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
    }
}

