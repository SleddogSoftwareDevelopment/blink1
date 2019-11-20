using System;
using Sleddog.Blink1.V2.Internal;
using Sleddog.Blink1.V2.Internal.Interfaces;

namespace Sleddog.Blink1.V2.Commands
{
    public class ReadPlaybackStateQuery : IBlink1Query<PlaybackStatus>
    {
        public PlaybackStatus ToResponseType(byte[] responseData)
        {
            var isPlaying = Convert.ToBoolean(responseData[2]);

            var start = Convert.ToUInt16(responseData[3]);
            var end = Convert.ToUInt16(responseData[4]);

            var count = Convert.ToUInt16(responseData[5]);
            var position = Convert.ToUInt16(responseData[6]);

            return new PlaybackStatus(isPlaying, start, end, count, position);
        }

        public byte[] ToHidCommand()
        {
            return new[]
            {
                (byte) Blink1Commands.ReadPlaybackState
            };
        }
    }
}