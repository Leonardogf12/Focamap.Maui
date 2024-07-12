namespace FocamapMaui.Models.Map
{
    public class PinDto
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

        public int Status { get; set; }

        public string Address { get; set; }

        public string FullDate { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }   
}