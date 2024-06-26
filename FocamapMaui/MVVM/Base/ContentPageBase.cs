namespace FocamapMaui.MVVM.Base
{
    public class ContentPageBase : ContentPage
	{
		public ContentPageBase()
		{
			Shell.SetNavBarIsVisible(this, false);			
			Content = new Grid();			
		}
	}
}

