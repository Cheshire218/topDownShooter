using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace MyShooter.UI
{
    public class MainMenu : BaseMenu
    {
        enum MainMenuItems
        {
            NewGame,
            Options,
            Quit
        }

        private void Start()
        {
            Interface.Execute(InterfaceObject.MainMenu);
        }

        protected override void CreateMenu(string[] menuItems)
        {
            _elementsOfInterface = new IControl[menuItems.Length];
            for (var index = 0; index < menuItems.Length; index++)
            {
                switch (index)
                {
                    case (int)MainMenuItems.NewGame:
                        {
                            var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MainMenuItems", "NewGame"));
                            tempControl.GetControl.onClick.AddListener(delegate
                            {
                                LoadNewGame(Main.Instance.Scenes.Game.SceneName);
                            });

                            _elementsOfInterface[index] = tempControl;
                            break;
                        }
                    case (int)MainMenuItems.Options:
                        {
                            var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MainMenuItems", "Options"));

                            tempControl.GetControl.onClick.AddListener(ShowOptions);
                            _elementsOfInterface[index] = tempControl;
                            break;
                        }
                    case (int)MainMenuItems.Quit:
                        {
                            var tempControl = CreateControl(Interface.InterfaceResources.ButtonPrefab, LangManager.Instance.Text("MainMenuItems", "Quit"));

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

        public override void Hide()
        {
            if (!IsShow) return;
            Clear(_elementsOfInterface);
            IsShow = false;
        }

        public override void Show()
        {
            if (IsShow) return;
            var tempMenuItems = System.Enum.GetNames(typeof(MainMenuItems));
            CreateMenu(tempMenuItems);
            IsShow = true;
        }

        private void ShowOptions()
        {
            Hide();
            Interface.Execute(InterfaceObject.OptionsMenu);
        }

        private void LoadNewGame(string lvl)
        {
            Hide();
            //SceneManager.sceneLoaded += delegate { Main.Instance.InitGame(); }; // подписываемся на событие загрузки сцены
            Interface.LoadSceneAsync(lvl);
        }
    }
}