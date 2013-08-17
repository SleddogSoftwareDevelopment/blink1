using System;
using System.Threading;
using Xunit;

namespace Sleddog.Blink1.ExplicitTests
{
	public class Blink1ConnectorTests
	{
		[RequireBlink1Hardware]
		public void CanIdentify()
		{
			Assert.DoesNotThrow(() => Blink1Connector.Identify(TimeSpan.FromSeconds(1)));

			Thread.Sleep(TimeSpan.FromSeconds(1));
		}
	}
}