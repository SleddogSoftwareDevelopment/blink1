namespace Sleddog.Blink1.Interfaces
{
	internal interface IBlink1Query : IBlink1Command
	{
	}

	internal interface IBlink1Query<out T> : IBlink1Query where T : class
	{
		T ToResponseType(byte[] responseData);
	}
}