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
			var sut = new Blink1Connector();

			Assert.DoesNotThrow(() => sut.Identify(TimeSpan.FromSeconds(1)));

			Thread.Sleep(TimeSpan.FromSeconds(1));
		}
	}
}