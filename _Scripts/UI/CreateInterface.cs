using UnityEngine;

namespace MyShooter.UI
{
    /// <summary>
    /// Отвечает за создания управляющих классов на сцене
    /// </summary>
    public class CreateInterface : MonoBehaviour
    {
        #region Editor
        /// <summary>
        /// Создание главного меню
        /// </summary>
        public void CreateMainMenu()
        {
            CreateComponent();
            gameObject.AddComponent<MainMenu>();
            gameObject.AddComponent<OptionsMenu>();
            Clear();
        }
        /// <summary>
        /// Создание игрового UI
        /// </summary>
        public void CreateGameMenu()
        {
            CreateComponent();
            gameObject.AddComponent<MenuPause>();
            Clear();
        }

        private void Clear()
        {
            DestroyImmediate(this);
        }

        private void CreateComponent()
        {
            gameObject.AddComponent<Interface>();
            gameObject.AddComponent<InterfaceResources>();
        }
        #endregion
    }
}