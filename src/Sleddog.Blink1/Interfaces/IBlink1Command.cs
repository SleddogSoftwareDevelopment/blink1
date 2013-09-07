namespace Sleddog.Blink1.Interfaces
{
	internal interface IBlink1Command
	{
		byte[] ToHidCommand();
	}
}