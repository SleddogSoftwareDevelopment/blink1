using System;
using System.Linq;

namespace Sleddog.Blink1.ExplicitTests
{
	public class Blink1Fixture : IDisposable
	{
		private readonly IBlink1 blink1;

		public Blink1Fixture()
		{
			blink1 = Blink1Connector.Scan().FirstOrDefault();
		}

		public IBlink1 Device
		{
			get { return blink1; }
		}

		public void Dispose()
		{
			if (blink1 != null)
				blink1.Dispose();
		}
	}
}