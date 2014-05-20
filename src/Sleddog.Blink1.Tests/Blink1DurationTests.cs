using System;
using System.Collections.Generic;
using System.Linq;
using Sleddog.Blink1.Internal;
using Xunit;
using Xunit.Extensions;

namespace Sleddog.Blink1.Tests
{
    public class Blink1DurationTests
    {
        private static readonly Random rnd = new Random();

        [Theory, PropertyData("HighTestData")]
        public void HighIsSetCorrectlyFromTimeSpanCtorInput(uint timeInMilliseconds, byte expected)
        {
            var ts = TimeSpan.FromMilliseconds(timeInMilliseconds);

            var sut = new Blink1Duration(ts);

            var actual = sut.High;

            Assert.Equal(expected, actual);
        }

        [Theory, PropertyData("LowTestData")]
        public void LowIsSetCorrectlyFromTimeSpanCtorInput(uint timeInMilliseconds, byte expected)
        {
            var ts = TimeSpan.FromMilliseconds(timeInMilliseconds);

            var sut = new Blink1Duration(ts);

            var actual = sut.Low;

            Assert.Equal(expected, actual);
        }

        [Theory, PropertyData("ImplicitTestData")]
        public void ImplicitConversionToTimeSpan(uint timeInMilliseconds, uint expected)
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
                yield return new object[] {0u, new byte()};

                foreach (var val in GenerateSampleData())
                {
                    var expected = Convert.ToByte(CalculateOutcomeValue(val) >> 8);

                    yield return new object[] {val, expected};
                }
            }
        }

        public static IEnumerable<object[]> LowTestData
        {
            get
            {
                yield return new object[] {0u, new byte()};

                foreach (var val in GenerateSampleData())
                {
                    var expected = Convert.ToByte(CalculateOutcomeValue(val) & 0xFF);

                    yield return new object[] {val, expected};
                }
            }
        }

        public static IEnumerable<object[]> ImplicitTestData
        {
            get
            {
                yield return new object[] {0u, 0u};
                yield return new object[] {250u, 250u};
                yield return new object[] {254u, 250u};
                yield return new object[] {255u, 260u};
                yield return new object[] {256u, 260u};

                foreach (var val in GenerateSampleData())
                {
                    var expected = CalculateOutcomeValue(val)*10;

                    yield return new object[] {val, expected};
                }
            }
        }

        private static uint CalculateOutcomeValue(uint val)
        {
            var blink1Duration = (double) val/10;
            var roundedDuration = Math.Round(blink1Duration, 0, MidpointRounding.ToEven);

            return Convert.ToUInt32(roundedDuration);
        }

        private static IEnumerable<uint> GenerateSampleData()
        {
            return Enumerable.Range(0, 15).Select(_ => (uint) rnd.Next(0, 365000));
        }
    }
}