using System;
using System.Collections.Generic;
using System.Linq;
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

	public class Blink1Tests
	{
		private const string LowestSerialNumber = "0x1A001000";
		private const string HighestSerialNumber = "0x1A002FFF";

		[RequireBlink1Hardware]
		public void ReadSerialReadsValidSerialNumber()
		{
			var connector = new Blink1Connector();

			var blink1 = connector.Scan().First();

			var actual = blink1.ReadSerial();

			Assert.InRange(actual, LowestSerialNumber, HighestSerialNumber);
		}
	}
}