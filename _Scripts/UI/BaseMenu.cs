using Object = UnityEngine.Object;
using UnityEngine;
using System;

namespace MyShooter.UI
{
    public abstract class BaseMenu : MonoBehaviour
    {
        protected IControl[] _elementsOfInterface;

        protected bool IsShow { get; set; }
        protected Interface Interface;
        protected virtual void Awake()
        {
            Interface = FindObjectOfType<Interface>();
        }

        public abstract void Hide();
        public abstract void Show();
        protected abstract void CreateMenu(string[] menuItems);

        protected void Clear(IControl[] controls)
        {
            foreach (var t in controls)
            {
                if (t == null) continue;
                Destroy(t.Instance);
            }
        }

        protected T CreateControl<T>(T prefab, string text) where T : Object, IControl
        {
            if (!prefab) throw new Exception(String.Format("Отсутствует ссылка на {0}", typeof(T)));
            var tempControl = Instantiate(prefab, Interface.InterfaceResources.MainPanel.transform.position, Quaternion.identity,
                Interface.InterfaceResources.MainPanel.transform);

            if (tempControl.GetText != null)
            {
                tempControl.GetText.text = text;
            }
            return tempControl;
        }
    }
}