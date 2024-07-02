namespace FocamapMaui.Services.Navigation
{
    public interface INavigationService
	{
        Task NavigationWithParameter<T>(IDictionary<string, object> parameter = null, View view = null) where T : IView;

        Task NavigationWithRoute(string route);

        Task GoBack();
    }
}

