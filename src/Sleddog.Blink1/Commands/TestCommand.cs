using System;
using Sleddog.Blink1.Interfaces;

namespace Sleddog.Blink1.Commands
{
    public class TestCommand : IBlink1Command
    {
        public byte[] ToHidCommand()
        {
            return new[]
            {
                Convert.ToByte(1),
                (byte) Blink1Commands.Test,
                Convert.ToByte(0),
                Convert.ToByte(0),
                Convert.ToByte(0),
                Convert.ToByte(0),
                Convert.ToByte(0),
                Convert.ToByte(0)
            };
        }
    }
}