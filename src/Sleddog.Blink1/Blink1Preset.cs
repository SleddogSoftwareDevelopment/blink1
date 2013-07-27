using System;
using System.Drawing;

namespace Sleddog.Blink1
{
	public class Blink1Preset
	{
		public Blink1Preset(Color color, TimeSpan duration) {
			Color = color;
			Duration = duration;
		}

		public Color Color { get; set; }
		public TimeSpan Duration { get; set; }
	}
}
