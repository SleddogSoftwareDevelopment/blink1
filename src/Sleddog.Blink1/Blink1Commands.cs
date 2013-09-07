namespace Sleddog.Blink1
{
	internal enum Blink1Commands : byte
	{
		GetVersion = (byte) 'v',
		SetColor = (byte) 'n',
		FadeToColor = (byte) 'c',
		InactivityMode = (byte) 'D',
		PresetControl = (byte) 'p',
		ReadPreset = (byte) 'R',
		SavePreset = (byte) 'P',
		EEPROMRead = (byte) 'e',
		EEPROMWrite = (byte) 'E'
	}
}