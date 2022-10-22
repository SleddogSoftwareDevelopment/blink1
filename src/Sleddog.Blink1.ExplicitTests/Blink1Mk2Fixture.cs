namespace Sleddog.Blink1.ExplicitTests
{
    public class Blink1Mk2Fixture : IDisposable
    {
        public Blink1Mk2Fixture()
        {
            var firstDevice = Blink1Connector.Scan().FirstOrDefault(b => b is IBlink1Mk2);

            if(firstDevice != null)
                Device = (IBlink1Mk2) firstDevice;
        }

        public IBlink1Mk2? Device { get; }

        public void Dispose()
        {
            Device?.Dispose();
        }
    }
}
