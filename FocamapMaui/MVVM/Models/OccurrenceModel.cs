using FocamapMaui.Models.Map;

namespace FocamapMaui.MVVM.Models
{
    public class OccurrenceModel
	{
        public string Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

		public string Date { get; set; }

		public string Hour { get; set; }

		public string Resume { get; set; }

        public int Status { get; set; }
       
        public LocationOccurrence Location { get; set; }

        public UserModel User { get; set; }
    }
}