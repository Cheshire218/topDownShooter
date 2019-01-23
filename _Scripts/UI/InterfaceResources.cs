using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace MyShooter.UI
{
    public class InterfaceResources : MonoBehaviour
    {

        public ButtonUi ButtonPrefab { get; private set; }
        public Canvas MainCanvas { get; private set; }
        public LayoutGroup MainPanel { get; private set; }
        public SliderUI ProgressbarPrefab { get; private set; }
        public DropdownUI DropdownPrefab { get; private set; }
        public ToggleUI TogglePrefab { get; private set; }
        public AudioMixer AudioMixer { get; private set; }
        public SliderUI SliderPrefab { get; private set; }
        public TextUI TextPrefab { get; private set; }

        private void Awake()
        {
            ButtonPrefab = Resources.Load<ButtonUi>("Button");
            MainCanvas = FindObjectOfType<Canvas>();
            ProgressbarPrefab = Resources.Load<SliderUI>("Progressbar");
            MainPanel = MainCanvas.GetComponentInChildren<LayoutGroup>();
            DropdownPrefab = Resources.Load<DropdownUI>("Dropdown");
            TogglePrefab = Resources.Load<ToggleUI>("Toggle");
            SliderPrefab = Resources.Load<SliderUI>("SliderUI");
            TextPrefab = Resources.Load<TextUI>("TextUI");
            AudioMixer = Resources.Load<AudioMixer>("MainAudioMixer");
        }
    }
}