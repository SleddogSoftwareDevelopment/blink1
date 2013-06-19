using System.Drawing;

namespace Sleddog.Blink1
{
	public class Blink1Identifier
	{
		public Blink1 Blink1 { get; set; }
		public Color Color { get; set; }

		public Blink1Identifier(Blink1 blink1, Color color)
		{
			Blink1 = blink1;
			Color = color;
		}
	}
}