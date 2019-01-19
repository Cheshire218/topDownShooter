using UnityEngine;

namespace MyShooter
{
    public class WeaponController : BaseController
    {
        private BaseWeapon _weapon;
        private Ammunition _ammo;
        private int _mouseButton = (int)MouseButton.LeftButton;

        private void Update()
        {
            if (!Enabled) return;

            if (Input.GetMouseButton(_mouseButton))
            {
                if (_weapon)
                {
                    _weapon.Fire(_ammo);
                }
            }
        }

        public void On(BaseWeapon weapon)
        {
            if (Enabled) return;
            base.On();
            _weapon = weapon;
            _weapon.IsVisible = true;
            _ammo = _weapon.Ammo;
        }

        public override void Off()
        {
            if (!Enabled) return;
            base.Off();
            _weapon.IsVisible = false;
            _weapon = null;
            _ammo = null;
        }
    }
}