using System;
using Sleddog.Blink1.Internal;

namespace Sleddog.Blink1.Commands
{
	internal class EnableInactivityModeCommand : Blink1Command
	{
		private readonly ushort endPosition;
		private readonly bool maintainState;
		private readonly ushort startPosition;
		private readonly Blink1Duration waitDuration;

		public EnableInactivityModeCommand(Blink1Duration waitDuration)
		{
			this.waitDuration = waitDuration;
		}

		public EnableInactivityModeCommand(Blink1Duration waitDuration, bool maintainState, ushort startPosition,
			ushort endPosition)
		{
			this.waitDuration = waitDuration;
			this.maintainState = maintainState;
			this.startPosition = startPosition;
			this.endPosition = endPosition;
		}

		protected override byte[] HidCommandData()
		{
			return new[]
			{
				(byte) Blink1Commands.InactivityMode,
				Convert.ToByte(true),
				waitDuration.High,
				waitDuration.Low,
				Convert.ToByte(maintainState),
				Convert.ToByte(startPosition),
				Convert.ToByte(endPosition)
			};
		}
	}
}