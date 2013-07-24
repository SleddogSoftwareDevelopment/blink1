using System.Diagnostics;
using Xunit;

namespace Sleddog.Blink1.ExplicitTests
{
	public class ExplicitFactAttribute : FactAttribute
	{
		public ExplicitFactAttribute()
		{
			if (!Debugger.IsAttached)
				Skip = "Only running in interactive mode";
		}
	}
}