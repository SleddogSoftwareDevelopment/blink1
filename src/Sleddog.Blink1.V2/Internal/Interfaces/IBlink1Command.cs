namespace Sleddog.Blink1.V2.Internal.Interfaces
{
    internal interface IBlink1Command
    {
        byte[] ToHidCommand();
    }
}