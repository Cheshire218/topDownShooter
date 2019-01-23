using UnityEngine;
using UnityEngine.UI;

namespace MyShooter.UI
{
    public class ButtonUi : MonoBehaviour, IControl
    {
        public Text GetText { get; private set; }
        public Button GetControl { get; private set; }

        private void Awake()
        {
            GetControl = transform.GetComponent<Button>();
            GetText = transform.GetComponentInChildren<Text>();
        }
        public void Interactable(bool value)
        {
            GetControl.interactable = value;
        }
        public GameObject Instance { get { return gameObject; } }
        public Selectable Control { get { return GetControl; } }
    }
}