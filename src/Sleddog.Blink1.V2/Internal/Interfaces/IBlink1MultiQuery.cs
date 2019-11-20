using System.Collections.Generic;

namespace Sleddog.Blink1.V2.Internal.Interfaces
{
    internal interface IBlink1MultiQuery : IBlink1MultiCommand
    {
    }

    internal interface IBlink1MultiQuery<out T> : IBlink1MultiQuery where T : class
    {
        T ToResponseType(IEnumerable<byte[]> responseData);
    }
}