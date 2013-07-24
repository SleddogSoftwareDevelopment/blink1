using System.Collections.Generic;

namespace Sleddog.Blink1.Commands
{
	internal interface IBlink1MultiCommand
	{
		IEnumerable<byte[]> ToHidCommands();
	}
}