using System.Collections;
using UnityEngine;

namespace MyShooter
{
    /// <summary>
    /// Класс реализует поведение оружия Огнестрельного оружия, стреляющего снарядами
    /// </summary>
    public sealed class Gun : BaseWeapon
    {
        [SerializeField] private float _dispersion = 0.015f;
        [SerializeField] private float _force = 500f;
        [SerializeField] private Transform _barrel;

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
                Vector3 ammoDir = _rootPlayer.forward + (_barrel.right * Random.Range(-_dispersion, _dispersion));
                tempAmmo.MyRigidBody.AddForce(ammoDir * _force);
                _currentAmmo--;
                _canFire = false;
                AnimateAttack();
                PlayAudio(_fireAudio);
                StartCoroutine(WaitCooldown());
            }
        }

        private void AnimateAttack()
        {
            if (!_animator) return;
            _animator.SetTrigger("attack");
        }

        protected override void AnimateReload()
        {
            if (!_animator) return;
            _animator.SetTrigger("reload");
        }





    }
}