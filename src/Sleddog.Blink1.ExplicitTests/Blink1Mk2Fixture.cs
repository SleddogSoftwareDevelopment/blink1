namespace Sleddog.Blink1.ExplicitTests
{
    public class Blink1Mk2Fixture : IDisposable
    {
        private readonly IBlink1Mk2 blink1;

        public Blink1Mk2Fixture()
        {
            blink1 = Blink1Connector.Scan().FirstOrDefault(b => (b is IBlink1Mk2)) as IBlink1Mk2;
        }

        public IBlink1Mk2 Device => blink1;

        public void Dispose()
        {
            blink1?.Dispose();
        }
    }
}
