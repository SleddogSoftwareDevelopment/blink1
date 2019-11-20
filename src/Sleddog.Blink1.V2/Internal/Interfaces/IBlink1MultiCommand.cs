using System.Collections.Generic;

namespace Sleddog.Blink1.V2.Internal.Interfaces
{
    internal interface IBlink1MultiCommand
    {
        IEnumerable<byte[]> ToHidCommands();
    }
}