using System;
using System.Reactive.Linq;

namespace Sleddog.Blink1
{
    internal static class ObservableExt
    {
        public static IObservable<long> TimerMaxTick(int numberOfTicks, TimeSpan dueTime, TimeSpan interval)
        {
            return Observable.Generate(
                0L,
                i => i <= numberOfTicks,
                i => i + 1,
                i => i,
                i => i == 0 ? dueTime : interval);
        }
    }
}