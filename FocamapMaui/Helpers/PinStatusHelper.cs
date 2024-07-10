using FocamapMaui.Controls;
using FocamapMaui.Controls.Maps;

namespace FocamapMaui.Helpers
{
    public static class PinStatusHelper
	{
        public static string GetColorName(int statusPin)
        {
            return statusPin switch
            {
                (int)PinStatus.Baixo => "CLPopupRiskLow",
                (int)PinStatus.Medio => "CLPopupRiskMedium",
                _ => "CLPopupRiskHigh",
            };
        }

        public static string GetStatusName(int statusPin)
        {
            return statusPin switch
            {
                (int)PinStatus.Baixo => StringConstants.LOW,
                (int)PinStatus.Medio => StringConstants.AVERAGE,
                _ => StringConstants.HIGH,
            };
        }
    }
}

