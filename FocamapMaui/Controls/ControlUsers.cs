namespace FocamapMaui.Controls
{
    public static class ControlUsers
	{
        public static void SetLocalIdByUserLogged()
        {
            App.FirebaseUserLocalIdKey = ControlPreferences.GetKeyOfPreferences(StringConstants.FIREBASE_USER_LOCAL_ID_KEY);
        }

        public static async void CheckUserHasLogged()
        {
            var value = ControlPreferences.GetKeyOfPreferences(StringConstants.FIREBASE_AUTH_TOKEN_KEY);

            if (string.IsNullOrEmpty(value)) return;

            SetLocalIdByUserLogged();

            await Shell.Current.GoToAsync(StringConstants.HOMEMAPVIEW_ROUTE);         
        }
    }
}