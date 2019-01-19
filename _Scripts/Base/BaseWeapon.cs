using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter
{
    public abstract class BaseWeapon : BaseObjectScene
    {
        public Ammunition Ammo;

        #region Serialize fields;
        [SerializeField] protected float _cooldown = 0.2f;
        #endregion;

        #region Protected fields;
        protected bool _canFire = true;
        #endregion;

        #region Abstract fuctions;
        public abstract void Fire(Ammunition ammo);
        #endregion;

        protected IEnumerator WaitCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            _canFire = true;
        }
    }
}