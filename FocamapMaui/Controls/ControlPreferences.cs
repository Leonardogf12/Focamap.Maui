using Newtonsoft.Json;

namespace FocamapMaui.Controls
{
    public static class ControlPreferences
    {
        public static string GetKeyOfPreferences(string key)
        {
            return Preferences.Get(key, "");
        }

        public static void AddKeyOnPreferences(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public static void AddKeyObjectOnPreferences(string key, object contentOfObject)
        {
            var serializeContent = JsonConvert.SerializeObject(contentOfObject);

            Preferences.Set(key, serializeContent);
        }

        public static void RemoveKeyFromPreferences(string key)
        {
            if (Preferences.ContainsKey(key))
            {
                Preferences.Remove(key);
            }
        }

        public static void UpdateKeyFromPreference(string key, string valueString, object contentObject = null)
        {
            RemoveKeyFromPreferences(key);

            if (contentObject != null)
            {
                AddKeyObjectOnPreferences(key, contentObject);
            }
            else
            {
                AddKeyOnPreferences(key, valueString);
            }           
        }
    }
}

