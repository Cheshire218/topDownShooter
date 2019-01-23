using UnityEngine;

namespace MyShooter
{
    public class InputController : BaseController
    {
        private void Update()
        {
            #region flashlight;
            if(Input.GetKeyDown(KeyCode.F))
            {
                Main.Instance.LightController.Switch();
            }
            #endregion;

            #region Wapons;
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Main.Instance.WeaponController.Reload();
            }
            #endregion;

            #region Player movement;
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Main.Instance.PlayerMovementController.Move(v, h);
            #endregion;
        }

        private void SetWeapon(int i)
        {
            Main.Instance.WeaponController.Off();
            var tempWeapon = Main.Instance.ObjectManager.Weapons[i];
            if (!tempWeapon) return;
            Main.Instance.WeaponController.On(tempWeapon);
        }
    }
}