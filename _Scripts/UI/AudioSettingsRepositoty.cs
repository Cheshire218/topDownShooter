using System.IO;
using UnityEngine;
using UnityEngine.Audio;

namespace MyShooter.UI
{
	public static class AudioSettingsRepositoty
	{
		private static AudioSettings _audioSettings;

		public static AudioSettings AudioSettings
		{
			get
			{
				return _audioSettings ?? (_audioSettings = DefaultSettings());
			}

			set
			{
				_audioSettings = value;
			}
		}

		private static AudioSettings DefaultSettings()
		{
            Debug.Log(1);
			var audioMixer = Resources.Load<AudioMixer>("MainAudioMixer");

			float background, sound; 

            audioMixer.GetFloat("BackgroundMusicVolume", out background);
			audioMixer.GetFloat("SoundVolume", out sound);
			return new AudioSettings
			{
				Background = background,
				Sound = sound
			};
		}

		private static readonly IData<AudioSettings> _data;
		static AudioSettingsRepositoty()
		{
			_data = new DataXMLSerializer<AudioSettings>();
			if (_data == null) return;
			_data.SetOptions(Path.Combine(Application.dataPath, "AudioSettings.xml"));
			_audioSettings = _data.Load();
		}

		public static void SaveData()
		{
			_data.Save(AudioSettings);
		}
	}
}