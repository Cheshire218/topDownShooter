using UnityEngine.EventSystems;

namespace MyShooter.UI
{
	public class AudioOptions : BaseMenu
	{
		private AudioSettings _audioSettings;

		public AudioSettings AudioSettings
		{
			get { return _audioSettings ?? (_audioSettings = AudioSettingsRepositoty.AudioSettings); }
		}
		enum AudioMenuItems
		{
			Background,
			Sound,
			Back
		}

		protected override void CreateMenu(string[] menuItems)
		{
			_elementsOfInterface = new IControl[menuItems.Length];
			for (var index = 0; index < menuItems.Length; index++)
			{
				switch (index)
				{
					case (int)AudioMenuItems.Background:
						{
							var tempControl = CreateControl(Interface.InterfaceResources.SliderPrefab, LangManager.Instance.Text("AudioMenuItems", "Background"));
							
							tempControl.GetControl.minValue = -60;
							tempControl.GetControl.maxValue = 20;
							tempControl.GetControl.onValueChanged.AddListener(BackgroundVolume);

							BackgroundVolume(AudioSettings.Background);
							tempControl.GetControl.value = AudioSettings.Background;

							_elementsOfInterface[index] = tempControl;
							break;
						}

					case (int)AudioMenuItems.Sound:
						{
							var tempControl = CreateControl(Interface.InterfaceResources.SliderPrefab, LangManager.Instance.Text("AudioMenuItems", "SoundVolume"));
							
							tempControl.GetControl.minValue = -60;
							tempControl.GetControl.maxValue = 20;
							tempControl.GetControl.onValueChanged.AddListener(SoundVolume);

							SoundVolume(AudioSettings.Sound);
							tempControl.GetControl.value = AudioSettings.Sound;

							_elementsOfInterface[index] = tempControl;
							break;
						}

					case (int)AudioMenuItems.Back:
						{
							var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("AudioMenuItems", "Back"));
							
							tempControl.GetControl.onClick.AddListener(Back);
							_elementsOfInterface[index] = tempControl;
							break;
						}
				}
			}
			if (_elementsOfInterface.Length < 0) return;
			_elementsOfInterface[0].Control.Select();
			_elementsOfInterface[0].Control.OnSelect(new BaseEventData(EventSystem.current));
		}

		private void SoundVolume(float value)
		{
			Interface.InterfaceResources.AudioMixer.SetFloat("SoundVolume", value);
		}

		private void BackgroundVolume(float value)
		{
            Interface.InterfaceResources.AudioMixer.SetFloat("BackgroundMusicVolume", value);
        }

		private void Back()
		{
            Save();
            Hide();
            Interface.Execute(InterfaceObject.OptionsMenu);
        }

		public override void Hide()
		{
			if (!IsShow) return;
			Clear(_elementsOfInterface);
			AudioSettingsRepositoty.SaveData();
			IsShow = false;
		}

		public override void Show()
		{
            if (IsShow) return;
			var tempMenuItems = System.Enum.GetNames(typeof(AudioMenuItems));
			CreateMenu(tempMenuItems);
			IsShow = true;
		}

		private void Save()
		{
            float background, sound;
			Interface.InterfaceResources.AudioMixer.GetFloat("BackgroundMusicVolume", out background);
			Interface.InterfaceResources.AudioMixer.GetFloat("SoundVolume", out sound);
            _audioSettings = new AudioSettings
			{
				Background = background,
				Sound = sound
			};
			AudioSettingsRepositoty.AudioSettings = _audioSettings;
            
        }
	}

}