using System.ComponentModel.DataAnnotations.Schema;
using FocamapMaui.Models;

namespace FocamapMaui.MVVM.Models
{
    public class UserModel
	{
		public int Id { get; set; }

		public string LocalIdFirebase { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public string City { get; set; }

		public string State { get; set; }
	}
}

