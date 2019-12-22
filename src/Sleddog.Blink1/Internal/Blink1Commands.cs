namespace Sleddog.Blink1.Internal
{
	/*
   * mk3 commands: https://github.com/todbot/blink1mk3/blob/master/firmware/firmware-v30x/main.c#L968
   */
	internal enum Blink1Commands : byte
	{
		Test = (byte) '!',
		GetVersion = (byte) 'v',
		SetColor = (byte) 'n',
		FadeToColor = (byte) 'c',
		InactivityMode = (byte) 'D',
		PresetControl = (byte) 'p',
		ReadPreset = (byte) 'R',
		SavePreset = (byte) 'P',
		SavePresetMk2 = (byte) 'W',
		ReadPlaybackState = (byte) 'S'
	}
}