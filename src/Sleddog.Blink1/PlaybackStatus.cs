namespace Sleddog.Blink1
{
    public class PlaybackStatus
    {
        public bool IsPlaying { get; set; }
        public ushort StartPosition { get; set; }
        public ushort EndPosition { get; set; }
        public ushort Count { get; set; }
        public ushort Position { get; set; }
    }
}