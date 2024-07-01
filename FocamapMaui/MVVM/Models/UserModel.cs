using System.ComponentModel.DataAnnotations.Schema;

namespace FocamapMaui.MVVM.Models
{
    public class UserModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

        [ForeignKey(nameof(Region))]
        public int RegionId { get; set; }        
	}
}

