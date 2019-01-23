using UnityEngine.UI;
using UnityEngine;

namespace MyShooter.UI
{
    public class SliderUI : MonoBehaviour, IControl
    {
        private void Awake()
        {
            GetControl = transform.GetComponentInChildren<Slider>();
            GetText = transform.GetComponentInChildren<Text>();
        }

        public Text GetText { get; private set; }

        public Slider GetControl { get; private set; }

        public void Interactable(bool value)
        {
            GetControl.interactable = value;
        }

        public GameObject Instance { get { return gameObject; } }
        public Selectable Control { get { return GetControl; } }
    }
}