using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using MyShooter;

namespace MyShooter.UI
{
    public class MenuPause : BaseMenu
    {
        private InputController _playerInputController;

        private InputController PlayerInputController
        {
            get
            {
                return _playerInputController ?? (_playerInputController = FindObjectOfType<InputController>());
            }
        }

        private AudioMixerSnapshot _paused;

        private AudioMixerSnapshot Paused
        {
            get
            {
                return _paused ?? (_paused = Resources.Load<AudioMixer>("MainAudioMixer").FindSnapshot("Paused"));
            }
        }
        private AudioMixerSnapshot _unPaused;

        private AudioMixerSnapshot UnPaused
        {
            get
            {
                return _unPaused ?? (_unPaused = Resources.Load<AudioMixer>("MainAudioMixer").FindSnapshot("UnPaused"));
            }
        }

        enum MenuPauseItems
        {
            Resume,
            MainMenu,
            Quit
        }

        public override void Hide()
        {
            if (!IsShow) return;
            UnPaused.TransitionTo(0.001f);
            Clear(_elementsOfInterface);
            Interface.InterfaceResources.MainPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
            PlayerInputController.enabled = true;
            IsShow = false;
        }

        public override void Show()
        {
            if (IsShow) return;
            Paused.TransitionTo(0.001f);
            var tempMenuItems = System.Enum.GetNames(typeof(MenuPauseItems));
            Interface.InterfaceResources.MainPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
            CreateMenu(tempMenuItems);
            PlayerInputController.enabled = false;
            IsShow = true;
        }

        private void Start()
        {
            Interface.InterfaceResources.MainPanel.gameObject.SetActive(false);
        }

        protected override void CreateMenu(string[] menuItems)
        {
            _elementsOfInterface = new IControl[menuItems.Length];
            for (var index = 0; index < menuItems.Length; index++)
            {
                switch (index)
                {
                    case (int)MenuPauseItems.Resume:
                        {
                            var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MenuPause", "Resume"));

                            tempControl.GetControl.onClick.AddListener(Hide);
                            _elementsOfInterface[index] = tempControl;
                            break;
                        }
                    case (int)MenuPauseItems.MainMenu:
                        {
                            var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MenuPause", "MainMenu"));

                            tempControl.GetControl.onClick.AddListener(LoadNewGame);
                            _elementsOfInterface[index] = tempControl;
                            break;
                        }
                    case (int)MenuPauseItems.Quit:
                        {
                            var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MenuPause", "Quit"));

                            tempControl.GetControl.onClick.AddListener(Interface.QuitGame);
                            _elementsOfInterface[index] = tempControl;
                            break;
                        }
                }
            }
            if (_elementsOfInterface.Length < 0) return;
            _elementsOfInterface[0].Control.Select();
            _elementsOfInterface[0].Control.OnSelect(new BaseEventData(EventSystem.current));
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsShow)
                {
                    Hide();
                }
                else
                {
                    Show();
                }
            }
        }

        private void LoadNewGame()
        {
            Hide();
            if (Main.Instance.Scenes.MainMenu == null)
            {

                Interface.LoadSceneAsync(0);
            }
            else
            {
                Interface.LoadSceneAsync(Main.Instance.Scenes.MainMenu);
            }
        }
    }
}