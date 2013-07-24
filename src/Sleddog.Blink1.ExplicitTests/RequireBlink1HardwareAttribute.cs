using System.Linq;

namespace Sleddog.Blink1.ExplicitTests
{
	public sealed class RequireBlink1HardwareAttribute : ExplicitFactAttribute
	{
		public RequireBlink1HardwareAttribute()
		{
			if (!new Blink1Connector().Scan().Any())
				Skip = "No Blink1 units connected";
		}
	}
}