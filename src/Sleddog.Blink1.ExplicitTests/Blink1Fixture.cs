using System;
using System.Linq;

namespace Sleddog.Blink1.ExplicitTests
{
	public class Blink1Fixture : IDisposable
	{
		public IBlink1 Device { get; }

		public Blink1Fixture()
		{
			Device = Blink1Connector.Scan().FirstOrDefault(b => !(b is IBlink1Mk2));
		}

		public void Dispose()
		{
			Device?.Dispose();
		}
	}
}