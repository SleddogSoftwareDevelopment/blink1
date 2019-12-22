using System;
using System.Linq;

namespace Sleddog.Blink1.ExplicitTests
{
	public class Blink1Mk2Fixture : IDisposable
	{
		public IBlink1Mk2 Device { get; }

		public Blink1Mk2Fixture()
		{
			Device = Blink1Connector.Scan().FirstOrDefault(b => b is IBlink1Mk2) as IBlink1Mk2;
		}

		public void Dispose()
		{
			Device?.Dispose();
		}
	}
}