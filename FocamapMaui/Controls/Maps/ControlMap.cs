namespace FocamapMaui.Controls.Maps
{
    public static class ControlMap
	{		
		public static Location GetRegion(string state)
		{
            var location = new Location();

            if (state.Equals(StringConstants.ES))
			{
                location = new Location(-20.278755549759843, -40.294870435936254);
            }

            if (state.Equals(StringConstants.MG))
            {
                location = new Location(-19.82839664683618, -44.014295168581384);
            }

            return location;
        }
    }
}

