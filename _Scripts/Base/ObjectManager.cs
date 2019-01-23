using UnityEngine;

namespace MyShooter
{
    public class ObjectManager : MonoBehaviour
    {
        private BaseWeapon[] _weapons = new BaseWeapon[2];
        private Transform _player;

        public BaseWeapon[] Weapons
        {
            get
            {
                return _weapons;
            }
        }

        void Start()
        {
            _player = FindObjectOfType<PlayerMovement>().transform;
            Weapons[0] = _player.GetComponentInChildren<BaseWeapon>();

            foreach (var weapon in _weapons)
            {
                if (weapon != null) weapon.IsVisible = false;
            }

            //Так как оружие пока что у нас одно ...
            if (_weapons[0] != null) Main.Instance.WeaponController.On(_weapons[0]);
        }
    }
}