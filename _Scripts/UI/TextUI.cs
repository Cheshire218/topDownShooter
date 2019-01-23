using UnityEngine.UI;
using UnityEngine;

namespace MyShooter.UI
{
    public class TextUI : MonoBehaviour, IControl
    {
        private void Awake()
        {
            GetText = transform.GetComponentInChildren<Text>();
        }

        public Text GetText { get; private set; }

        public GameObject Instance { get { return gameObject; } }
        public Selectable Control { get { return null; } }
    }
}