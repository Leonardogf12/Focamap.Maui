using DevExpress.Maui.Controls;
using FocamapMaui.Controls.Resources;

namespace FocamapMaui.Components.UI
{
    public class BottomSheetAddOccurrenceCustom : BottomSheet
    {
		public BottomSheetAddOccurrenceCustom()
		{
			BackgroundColor = ControlResources.GetResource<Color>("CLSecondary");

			Content = BuildBottomSheet;
		}

		private View BuildBottomSheet
		{
			get
			{				
				return new Grid();
			}
		}

    }
}

