using System;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1.Commands
{
	internal class PlayPresetCommand : Blink1Command
	{
		private readonly byte count;
		private readonly byte endPosition;
		private readonly byte startPosition;

		public PlayPresetCommand(ushort startPosition) : this(startPosition, 0, 0)
		{ }

		public PlayPresetCommand(ushort startPosition, ushort endPosition, ushort count)
		{
			this.startPosition = Convert.ToByte(startPosition);
			this.endPosition = Convert.ToByte(endPosition);
			this.count = Convert.ToByte(count);
		}

		protected override byte[] HidCommandData()
		{
			return new[]
			{
				(byte) Blink1Commands.PresetControl,
				Convert.ToByte(true),
				startPosition,
				endPosition,
				count
			};
		}
	}
}