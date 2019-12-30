using System.Linq;

namespace Sleddog.Blink1.ExplicitTests
{
	public class RequireNoBlinkHardwareAttribute : BlinkHardwareScannerAttribute
	{
		public RequireNoBlinkHardwareAttribute()
		{
			if (Devices.Any())
			{
				Skip = "Blink1 devices connected";
			}
		}
	}
}