using System.Linq;

namespace Sleddog.Blink1.ExplicitTests
{
	public class RequireBlinkHardwareAttribute : BlinkHardwareScannerAttribute
	{
		public RequireBlinkHardwareAttribute()
		{
			if (!Devices.Any())
			{
				Skip = "No Blink devices connected";
			}
		}
	}
}