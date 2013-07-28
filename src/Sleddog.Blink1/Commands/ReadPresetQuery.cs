using System;
using System.Drawing;
using System.Linq;

namespace Sleddog.Blink1.Commands
{
	public class ReadPresetQuery: IBlink1Query<Blink1Preset> {
		private readonly byte position;

		public ReadPresetQuery(int position) {
			if (!Enumerable.Range(0, Blink1.NumberOfPresets).Contains(position)) {
				throw new ArgumentOutOfRangeException("position");
			}

			this.position = Convert.ToByte(position);
		}

		public Blink1Preset ToResponseType(byte[] responseData) {
			Color color = Color.FromArgb(red: responseData[2], green: responseData[3], blue: responseData[4]);
			var duration =TimeSpan.FromMilliseconds(ConvertToMilliseconds(high: responseData[5], low: responseData[6]));

			return new Blink1Preset(color, duration);
		}

		private int ConvertToMilliseconds(byte high, byte low) {
			return (high + low)*10;
		}

		public byte[] ToHidCommand() {
			return new[] {
				Convert.ToByte(1), 
				(byte) Blink1Commands.ReadPreset, 
				Convert.ToByte(0),
				Convert.ToByte(0),
				Convert.ToByte(0),
				Convert.ToByte(0),
				Convert.ToByte(0),
				position
			};
		}
	}
}
