using Xunit;
using Xunit.Abstractions;

namespace Sleddog.Blink1.ExplicitTests
{
    [Trait("Explicit", "True")]
    public class Blink1ConnectorTests
    {
        private readonly ITestOutputHelper output;
        public Blink1ConnectorTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [RequireBlinkHardware]
        public void ScanFindsDevices()
        {
            var devices = Blink1Connector.Scan();

            foreach(var device in devices)
            {
                output.WriteLine(device.SerialNumber);
            }

            Assert.NotEmpty(devices);
        }

        [RequireNoBlinkHardware]
        public void ScanWithNoDevicesFindsNone()
        {
            var devices = Blink1Connector.Scan();

            Assert.Empty(devices);
        }

        [RequireBlinkHardware]
        public void ConnectToSpecificDevice()
        {
            var serialNumber = "0x20002BCE";

            var device = Blink1Connector.Connect(serialNumber);

            Assert.NotNull(device);
        }
    }
}
