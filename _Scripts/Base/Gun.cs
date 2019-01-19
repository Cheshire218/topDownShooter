using System.Collections;
using UnityEngine;

namespace MyShooter
{
    /// <summary>
    /// Класс реализует поведение оружия Огнестрельного оружия, стреляющего снарядами
    /// </summary>
    public sealed class Gun : BaseWeapon
    {
        [SerializeField] private float _reloadTime = 1.5f;
        [SerializeField] private int _maxAmmo = 20;
        [SerializeField] private float _dispersion = 0.015f;
        [SerializeField] private float _force = 500f;
        [SerializeField] private Transform _barrel;
        [SerializeField] private int _currentAmmo;
        [SerializeField] private bool _isReloading;

        protected override void Awake()
        {
            base.Awake();
            _currentAmmo = _maxAmmo;
            _isReloading = false;
        }

        public override void Fire(Ammunition ammo)
        {
            if(_isReloading)
            {
                return;
            }

            if(_currentAmmo <= 0)
            {
                Reload();
                return;
            }

            if (_canFire && ammo)
            {
                var tempAmmo = Instantiate(ammo, _barrel.position, _barrel.rotation);
                Vector3 ammoDir = _barrel.forward + (_barrel.right * Random.Range(-_dispersion, _dispersion));
                tempAmmo.MyRigidBody.AddForce(ammoDir * _force);
                _currentAmmo--;
                _canFire = false;
                StartCoroutine(WaitCooldown());
            }
        }

        public void Reload()
        {
            _isReloading = true;
            StartCoroutine(WaitReload());
        }

        public IEnumerator WaitReload()
        {
            yield return new WaitForSeconds(_reloadTime);
            _isReloading = false;
            _currentAmmo = _maxAmmo;
        }
    }
}