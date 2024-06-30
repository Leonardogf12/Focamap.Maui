namespace FocamapMaui.Helpers
{
    public static class ParameterHelper
	{
        public static Dictionary<string, object> SetParameter(string keyString, bool valueObject)
        {
            return new Dictionary<string, object>
            {
                { keyString , valueObject }
            };
        }
    }
}

