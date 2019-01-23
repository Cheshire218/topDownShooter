using UnityEngine;
using UnityEngine.UI;

namespace MyShooter.UI
{
	public class DropdownUI : MonoBehaviour, IControl
	{
		public Text GetText { get; private set; }
		public Dropdown GetControl { get; private set; }

		private void Awake()
		{
			GetText = transform.GetComponentInChildren<Text>();
			GetControl = transform.GetComponentInChildren<Dropdown>();
		}

		public void Interactable(bool value)
		{
			GetControl.interactable = value;
		}

		public GameObject Instance { get { return gameObject; } }
		public Selectable Control { get { return GetControl; } }
	}
}