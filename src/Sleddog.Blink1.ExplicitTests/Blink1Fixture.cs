namespace Sleddog.Blink1.ExplicitTests
{
    public class Blink1Fixture : IDisposable
    {
        public Blink1Fixture()
        {
            var firstDevice = Blink1Connector.Scan().FirstOrDefault(b => !(b is IBlink1Mk2));

            if(firstDevice != null)
                Device = firstDevice;
        }

        public IBlink1? Device { get; }

        public void Dispose()
        {
            Device?.Dispose();
        }
    }
}
