using FocamapMaui.Models.Map;

namespace FocamapMaui.MVVM.Models
{
    public class UserModel
	{		
		public string LocalIdFirebase { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public City City { get; set; }
	}
}