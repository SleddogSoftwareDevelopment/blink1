namespace Sleddog.Blink1
{
    public class PlaybackStatus
    {
        public bool IsPlaying { get; private set; }
        public ushort StartPosition { get; private set; }
        public ushort EndPosition { get; private set; }
        public ushort Count { get; private set; }
        public ushort Position { get; private set; }

        internal PlaybackStatus(bool isPlaying, ushort startPosition, ushort endPosition, ushort count, ushort position)
        {
            IsPlaying = isPlaying;
            StartPosition = startPosition;
            EndPosition = endPosition;
            Count = count;
            Position = position;
        }
    }
}