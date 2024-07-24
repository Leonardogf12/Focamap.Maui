using System.Reflection;
using Newtonsoft.Json.Linq;

namespace FocamapMaui.Controls
{
    public class ControlFiles
	{
		public static string GetValueFromFilePropertyJson(string filePath, string mainPropertyName, string childPropertyName)
		{
            var assembly = Assembly.GetExecutingAssembly();

            using var stream = assembly.GetManifestResourceStream(StringConstants.FILE_PATH_GLOBAL + filePath);

            string json = string.Empty;

            using (var reader = new StreamReader(stream!))
            {
                json = reader.ReadToEnd();
            }

            var jsonObject = JObject.Parse(json);
           
           return jsonObject[mainPropertyName]?[childPropertyName]?.ToString();
        }       
    }
}