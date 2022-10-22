using System;
using System.Linq;

namespace Sleddog.Blink1.ExplicitTests
{
    public class Blink1Fixture : IDisposable
    {
        private readonly IBlink1 blink1;

        public Blink1Fixture()
        {
            blink1 = Blink1Connector.Scan().FirstOrDefault(b => !(b is IBlink1Mk2));
        }

        public IBlink1 Device => blink1;

        public void Dispose()
        {
            blink1?.Dispose();
        }
    }
}