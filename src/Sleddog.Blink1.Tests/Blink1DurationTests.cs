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
        public void HighIsSetCorrectlyFromTimeSpanCtorInput(double timeInMilliseconds, byte expected)
        {
            var ts = TimeSpan.FromMilliseconds(timeInMilliseconds);

            var sut = new Blink1Duration(ts);

            var actual = sut.High;

            Assert.Equal(expected, actual);
        }

        [Theory, PropertyData("LowTestData")]
        public void LowIsSetCorrectlyFromTimeSpanCtorInput(double timeInMilliseconds, byte expected)
        {
            var ts = TimeSpan.FromMilliseconds(timeInMilliseconds);

            var sut = new Blink1Duration(ts);

            var actual = sut.Low;

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> HighTestData
        {
            get
            {
                yield return new object[] {0, new byte()};

                foreach (var val in GenerateSampleData())
                {
                    var expected = Convert.ToByte((val/10) >> 8);

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
                    var expected = Convert.ToByte((val/10) & 0xFF);

                    yield return new object[] {val, expected};
                }
            }
        }

        private static IEnumerable<int> GenerateSampleData()
        {
            return Enumerable.Range(0, 15).Select(_ => rnd.Next(0, 365000));
        }
    }
}