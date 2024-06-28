namespace FocamapMaui.Services.Navigation
{
	public class NavigationService : INavigationService
	{
		private bool _isBrowsing;

        public async Task GoBack()
        {
            if (_isBrowsing) return;

            try
            {
                _isBrowsing = true;
                await Shell.Current.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _isBrowsing = false;
            }                        
        }

        public async Task NavigationWithParameter<T>(IDictionary<string, object> parameter = null, View view = null) where T : IView
		{
            if (_isBrowsing) return;

            try
            {
                _isBrowsing = true;

                var typeView = typeof(T);

                if (parameter is not null)
                {
                    await Shell.Current.GoToAsync(typeView.Name, parameter);
                }
                else
                {
                    await Shell.Current.GoToAsync($"{typeView.Name}");
                }
            }
            catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
			}
			finally
			{
                _isBrowsing = false;
            }						
		}
	}
}

