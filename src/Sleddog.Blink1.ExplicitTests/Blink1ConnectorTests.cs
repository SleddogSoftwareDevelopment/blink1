using System;
using FluentAssertions;

namespace Sleddog.Blink1.ExplicitTests
{
	public class Blink1ConnectorTests
	{
		[RequireBlink1Hardware]
		public void CanIdentify()
		{
			Action act = () => Blink1Connector.Identify(TimeSpan.FromSeconds(1));

			act.ShouldNotThrow();
		}

		[RequireBlink1Hardware]
		public void ScanWithOneDeviceConnectedFindsOneDevice()
		{
			var devices = Blink1Connector.Scan();

			devices.Should().HaveCount(1);
		}

		[RequireNoBlink1Hardware]
		public void ScanWithNoDevicesFindsNone()
		{
			var devices = Blink1Connector.Scan();

			devices.Should().BeEmpty();
		}
	}
}