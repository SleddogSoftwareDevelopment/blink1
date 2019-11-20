using System;
using Sleddog.Blink1.V2.Internal;
using Sleddog.Blink1.V2.Internal.Interfaces;

namespace Sleddog.Blink1.V2.Commands
{
    internal class VersionQuery : IBlink1Query<Version>
    {
        public Version ToResponseType(byte[] responseData)
        {
            var major = responseData[3] - '0';
            var minor = responseData[4] - '0';

            return new Version(major, minor);
        }

        public byte[] ToHidCommand()
        {
            return new[]
            {
                (byte) Blink1Commands.GetVersion
            };
        }
    }
}