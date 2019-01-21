using UnityEngine;

namespace MyShooter
{
    public sealed class Main : MonoBehaviour
    {
        private GameObject _allControllers;
        public InputController _inputController { get; private set; }

        public static Main Instance { get; private set; }

        public LightController LightController { get; private set; }
        public WeaponController WeaponController { get; private set; }
        public PlayerMovementController PlayerMovementController { get; private set; }
        public ObjectManager ObjectManager { get; private set; }
        

        private void Awake()
        {
            Instance = this;
            _allControllers = new GameObject("AllControllers");
            LightController = _allControllers.AddComponent<LightController>();
            WeaponController = _allControllers.AddComponent<WeaponController>();
            ObjectManager = _allControllers.AddComponent<ObjectManager>();
            _inputController = _allControllers.AddComponent<InputController>();
            PlayerMovementController = _allControllers.AddComponent<PlayerMovementController>();
            PlayerMovementController.On();
        }
    }
}