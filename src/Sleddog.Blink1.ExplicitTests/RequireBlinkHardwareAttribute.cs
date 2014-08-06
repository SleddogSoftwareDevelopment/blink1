using System.Linq;

namespace Sleddog.Blink1.ExplicitTests
{
    public class RequireBlinkHardwareAttribute : BlinkHardwareScannerAttribute
    {
        public RequireBlinkHardwareAttribute()
        {
            if (!devices.Any())
            {
                Skip = "No Blink devices connected";
            }
        }
    }
}