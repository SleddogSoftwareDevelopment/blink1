using System.Diagnostics;
using Xunit;

namespace Sleddog.Blink1.ExplicitTests
{
	public class ExplicitFactAttribute : FactAttribute
	{
		protected ExplicitFactAttribute()
		{
			if (!Debugger.IsAttached)
			{
				Skip = "Only running in interactive mode";
			}
		}
	}
}