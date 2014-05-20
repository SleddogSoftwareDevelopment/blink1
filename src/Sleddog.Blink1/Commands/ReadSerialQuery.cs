using System;
using System.Collections.Generic;
using System.Linq;
using Sleddog.Blink1.Internal;
using Sleddog.Blink1.Internal.Interfaces;

namespace Sleddog.Blink1.Commands
{
	internal class ReadSerialQuery : IBlink1MultiQuery<string>
	{
		public string ToResponseType(IEnumerable<byte[]> responseData)
		{
			const int serialIndex = 3;

			var serialBytes = (from rd in responseData
			                   select rd[serialIndex]).ToArray();

			var serialChars = new List<char>();

			foreach (var b in serialBytes)
			{
				var firstChar = b >> 4;
				var secondChar = b;

				serialChars.Add((char) ToHex(firstChar));
				serialChars.Add((char) ToHex(secondChar));
			}

			return string.Format("0x{0}", string.Join(string.Empty, serialChars));
		}

		public IEnumerable<byte[]> ToHidCommands()
		{
			List<byte[]> data = this;

			return data;
		}

		public static implicit operator List<byte[]>(ReadSerialQuery query)
		{
			byte eepromSerialAddress = 2;
			var baseSerialCommand = new[] {Convert.ToByte(1), (byte) Blink1Commands.EEPROMRead};

			var commands = new List<byte[]>();

			for (var i = 0; i < 4; i++)
			{
				var command = baseSerialCommand.Concat(new[] {eepromSerialAddress++}).ToArray();

				commands.Add(command);
			}

			return commands;
		}

		private int ToHex(int inputValue)
		{
			var charValue = inputValue & 0x0F;

			return (charValue <= 9) ? (charValue + '0') : (charValue - 10 + 'A');
		}
	}
}