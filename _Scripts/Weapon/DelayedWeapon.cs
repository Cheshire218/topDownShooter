using System.Collections;
using UnityEngine;

namespace MyShooter
{
    /// <summary>
    /// Класс реализует поведение оружия с задержкой выстрела, стреляющего снарядами
    /// </summary>
    public sealed class DelayedWeapon : BaseWeapon
    {
        [SerializeField] private float _dispersion = 0;
        [SerializeField] private float _force = 500f;
        [SerializeField] private Transform _barrel;
        [SerializeField] protected float _delayBefore = 0;

        private ParticleSystem _muzzle;

        protected override void Awake()
        {
            base.Awake();
            _muzzle = _barrel.GetComponent<ParticleSystem>();
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
                StartCoroutine(ThrowAmmo(ammo));
                _currentAmmo--;
                _canFire = false;
                AnimateAttack();
                PlayParticles();
                PlayAudio(_fireAudio);
                StartCoroutine(WaitCooldown());
            }
        }

        public override void StopShooting()
        {
            StopAllCoroutines();
        }

        private IEnumerator ThrowAmmo(Ammunition ammo)
        {
            yield return new WaitForSeconds(_delayBefore);
            var tempAmmo = Instantiate(ammo, _barrel.position, _barrel.rotation);
            tempAmmo.transform.parent = Main.Instance.ProjectileContainer;
            Vector3 ammoDir = _rootPlayer.forward + (_barrel.right * Random.Range(-_dispersion, _dispersion));
            tempAmmo.MyRigidBody.AddForce(ammoDir * _force);
        }

        protected override void PlayParticles()
        {
            if (!_muzzle) return;
            _muzzle.Play();
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