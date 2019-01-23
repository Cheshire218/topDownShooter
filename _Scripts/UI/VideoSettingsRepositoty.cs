using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MyShooter.UI
{
	public static class VideoSettingsRepositoty
	{
		private static VideoSettings _videoSettings;
		private static readonly IData<VideoSettings> _data;

		/// <summary>
		/// В конструкторе загружаем сохраненные настройки
		/// </summary>
		static VideoSettingsRepositoty()
		{
			_data = new DataXMLSerializer<VideoSettings>();
			_data.SetOptions(Path.Combine(Application.dataPath, "VideoSettings.xml"));
			_videoSettings = _data.Load();
		}

		public static VideoSettings VideoSettings
		{
			get
			{
				return _videoSettings ?? (_videoSettings = DefaultSettings());
			}

			set
			{
				_videoSettings = value;
			}
		}

		private static VideoSettings DefaultSettings()
		{
			var result = new VideoSettings();
			var qualityNamesList = QualitySettings.names;
			var i = 0;
			result.Items = new List<VideoSettingsItems>();
			foreach (var name in qualityNamesList)
			{
				QualitySettings.SetQualityLevel(i++);
				result.Items.Add(
					new VideoSettingsItems
					{
						Name = name,
						SoftParticles = QualitySettings.softParticles,
						ShadowQuality = QualitySettings.shadows
					});
			}

			return result;
		}

		public static void SaveData()
		{
			_data.Save(VideoSettings);
			Debug.Log(Path.Combine(Application.dataPath, "VideoSettings.xml"));
		}
	}
}