using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1
{
	public class Blink1Mk3 : Blink1Mk2, IBlink1Mk3
	{
		internal Blink1Mk3(Blink1CommandBus commandBus) : base(commandBus)
		{
		}
	}
}