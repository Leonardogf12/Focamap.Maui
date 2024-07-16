namespace FocamapMaui.Controls.Resources
{
    public class ControlResources
	{
		public static T GetResource<T>(string name)
		{
			if(App.Current.Resources.TryGetValue(name, out var resourceValue) && resourceValue is T typeResource)
			{
				return typeResource;
			}

			return default;
		}

        public static ImageSource GetImage(string name)
        {
            ImageSource imageSource = ImageSource.FromFile(name);

            return imageSource;
        }
    }
}

