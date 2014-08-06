using System;
using Xunit;

namespace Sleddog.Blink1.ExplicitTests
{
    public class Blink1ConnectorTests
    {
        [RequireBlink1Hardware]
        public void CanIdentify()
        {
            Assert.DoesNotThrow(() => Blink1Connector.Identify(TimeSpan.FromSeconds(1)));
        }

        [RequireBlink1Hardware]
        public void ScanFindsDevices()
        {
            var devices = Blink1Connector.Scan();

            Assert.NotEmpty(devices);
        }

        [RequireNoBlink1Hardware]
        public void ScanWithNoDevicesFindsNone()
        {
            var devices = Blink1Connector.Scan();

            Assert.Empty(devices);
        }
    }
}