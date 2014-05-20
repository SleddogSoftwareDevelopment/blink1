namespace Sleddog.Blink1.Internal.Interfaces
{
    internal interface IBlink1Command
    {
        byte[] ToHidCommand();
    }
}