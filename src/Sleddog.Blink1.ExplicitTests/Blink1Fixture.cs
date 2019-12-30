using System;
using System.Linq;

namespace Sleddog.Blink1.ExplicitTests
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class Blink1Fixture<TBlinkType> : IDisposable where TBlinkType : IBlink1
	{
		public TBlinkType Device { get; }

		public Blink1Fixture()
		{
			Device = (TBlinkType) Blink1Connector.Scan().FirstOrDefault(b => b is TBlinkType);
		}

		public void Dispose()
		{
			Device?.Dispose();
		}
	}
}