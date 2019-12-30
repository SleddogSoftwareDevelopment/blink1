namespace Sleddog.Blink1
{
	public class PlaybackStatus
	{
		public bool IsPlaying { get; }
		public ushort StartPosition { get; }
		public ushort EndPosition { get; }
		public ushort Count { get; }
		public ushort Position { get; }

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