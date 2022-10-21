namespace Sleddog.Blink1.ExplicitTests
{
    public class RequireNoBlinkHardwareAttribute : BlinkHardwareScannerAttribute
    {
        public RequireNoBlinkHardwareAttribute()
        {
            if (devices.Any())
            {
                Skip = "Blink1 devices connected";
            }
        }
    }

}
