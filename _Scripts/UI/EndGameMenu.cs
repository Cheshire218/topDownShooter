using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using MyShooter;

namespace MyShooter.UI
{
    public class EndGameMenu : BaseMenu
    {
        enum EndGameMenuItems
        {
            EndGameText,
            Restart,
            MainMenu,
            Quit
        }

        public override void Hide()
        {
            if (!IsShow) return;
            Clear(_elementsOfInterface);
            Interface.InterfaceResources.MainPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
            IsShow = false;
        }

        public override void Show()
        {
            if (IsShow) return;
            var tempMenuItems = System.Enum.GetNames(typeof(EndGameMenuItems));
            Interface.InterfaceResources.MainPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
            CreateMenu(tempMenuItems);
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
                    case (int)EndGameMenuItems.EndGameText:
                        {
                            var tempControl = CreateControl(Interface.InterfaceResources.TextPrefab, LangManager.Instance.Text("EndGameMenu", "EndGameText"));

                            _elementsOfInterface[index] = tempControl;
                            break;
                        }
                    case (int)EndGameMenuItems.Restart:
                        {
                            var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("EndGameMenu", "Restart"));

                            tempControl.GetControl.onClick.AddListener(RestartGame);
                            _elementsOfInterface[index] = tempControl;
                            break;
                        }
                    case (int)EndGameMenuItems.MainMenu:
                        {
                            var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("EndGameMenu", "MainMenu"));

                            tempControl.GetControl.onClick.AddListener(LoadNewGame);
                            _elementsOfInterface[index] = tempControl;
                            break;
                        }
                    case (int)EndGameMenuItems.Quit:
                        {
                            var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("EndGameMenu", "Quit"));

                            tempControl.GetControl.onClick.AddListener(Interface.QuitGame);
                            _elementsOfInterface[index] = tempControl;
                            break;
                        }
                }
            }
            if (_elementsOfInterface.Length < 0) return;
            foreach (var cntrl in _elementsOfInterface)
            {
                if (cntrl.Control == null) continue;
                cntrl.Control.Select();
                cntrl.Control.OnSelect(new BaseEventData(EventSystem.current));
                break;
            }

        }

        private void RestartGame()
        {
            Hide();
            if (Main.Instance.Scenes.MainMenu == null)
            {

                Interface.LoadSceneAsync(0);
            }
            else
            {
                Interface.LoadSceneAsync(Main.Instance.Scenes.Game);
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