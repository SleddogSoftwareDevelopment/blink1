using System;
using System.Collections.Generic;
using Sleddog.Blink1.Internal.Interfaces;

namespace Sleddog.Blink1.Internal
{
	internal abstract class Blink1Command : IBlink1Command
	{
		private readonly ReportId reportId;

		private static readonly Dictionary<ReportId, int> dataLengthMap = new Dictionary<ReportId, int>
		{
			{ReportId.One, 8},
			{ReportId.Two, 64}
		};

		protected Blink1Command() : this(ReportId.One)
		{ }

		protected Blink1Command(ReportId reportId)
		{
			this.reportId = reportId;
		}

		public byte[] ToHidCommand()
		{
			var reportDataLength = dataLengthMap[reportId];

			var dataBuffer = new byte[reportDataLength];

			dataBuffer[0] = (byte) reportId;

			var commandData = HidCommandData();

			var commandDataLength = Math.Min(reportDataLength - 1, commandData.Length);

			Array.Copy(commandData, 0, dataBuffer, 1, commandDataLength);

			return dataBuffer;
		}

		protected abstract byte[] HidCommandData();
	}
}