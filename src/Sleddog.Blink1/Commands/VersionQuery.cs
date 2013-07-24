using System;

namespace Sleddog.Blink1.Commands
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
			return this;
		}

		public static implicit operator byte[](VersionQuery query)
		{
			return new[] {Convert.ToByte(1), (byte) Blink1Commands.GetVersion};
		}
	}
}