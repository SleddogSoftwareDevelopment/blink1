using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace Sleddog.Blink1.Tests
{
	public class Blink1DurationTests
	{
		private static readonly Random rnd = new Random();

		[Theory, PropertyData("HighTestData")]
		public void HighIsSetCorrectlyFromTimeSpanCtorInput(int timeInMilliseconds, byte expected)
		{
			var ts = TimeSpan.FromMilliseconds(timeInMilliseconds);

			var sut = new Blink1Duration(ts);

			var actual = sut.High;

			Assert.Equal(expected, actual);
		}

		[Theory, PropertyData("LowTestData")]
		public void LowIsSetCorrectlyFromTimeSpanCtorInput(int timeInMilliseconds, byte expected)
		{
			var ts = TimeSpan.FromMilliseconds(timeInMilliseconds);

			var sut = new Blink1Duration(ts);

			var actual = sut.Low;

			Assert.Equal(expected, actual);
		}

		[Theory, PropertyData("ImplicitTestData")]
		public void ImplicitConversionToTimeSpan(int timeInMilliseconds, double expected)
		{
			var ts = TimeSpan.FromMilliseconds(timeInMilliseconds);

			var sut = new Blink1Duration(ts);

			TimeSpan actual = sut;

			Assert.Equal(expected, actual.TotalMilliseconds);
		}

		public static IEnumerable<object[]> HighTestData
		{
			get
			{
				yield return new object[] {0, new byte()};

				foreach (var val in GenerateSampleData())
				{
					var expected = Convert.ToByte(Convert.ToInt32(val/Blink1Duration.Blink1UpdateFrequency) >> 8);

					yield return new object[] {val, expected};
				}
			}
		}

		public static IEnumerable<object[]> LowTestData
		{
			get
			{
				yield return new object[] {0, new byte()};

				foreach (var val in GenerateSampleData())
				{
					var expected = Convert.ToByte(Convert.ToInt32(val/Blink1Duration.Blink1UpdateFrequency) & 0xFF);

					yield return new object[] {val, expected};
				}
			}
		}

		public static IEnumerable<object[]> ImplicitTestData
		{
			get
			{
				yield return new object[] { 0, 0d };
				yield return new object[] { 250, 250d };
				yield return new object[] { 254, 250d };
				yield return new object[] { 255, 260d };
				yield return new object[] { 256, 260d };

				foreach (var val in GenerateSampleData())
				{
					double expected = Convert.ToInt32(val / Blink1Duration.Blink1UpdateFrequency) * Blink1Duration.Blink1UpdateFrequency;

					yield return new object[] { val, expected };
				}
			}
		}


		private static IEnumerable<int> GenerateSampleData()
		{
			return Enumerable.Range(0, 15).Select(_ => rnd.Next(0, 365000));
		}
	}
}