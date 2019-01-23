using System.Collections.Generic;
using UnityEngine;

namespace MyShooter.UI
{
	public class VideoSettings
	{
		public int CurrentSettings { get; set; }
		public List<VideoSettingsItems> Items { get; set; }
	}

	public struct VideoSettingsItems
	{
		public string Name { get; set; }
		public string ScreenResolution { get; set; }
		public ShadowQuality ShadowQuality { get; set; }
		public bool SoftParticles { get; set; }
	}
}