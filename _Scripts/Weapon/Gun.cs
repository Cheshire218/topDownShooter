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
        [SerializeField] private float _hearRadius = 30f;

        private ParticleSystem _muzzle;

        protected override void Awake()
        {
            base.Awake();
            _muzzle = _barrel.GetComponent<ParticleSystem>();
        }

        public override void StopShooting()
        {
            //пока не знаю что тут делать
        }

        private void makeShotSound()
        {
            Collider[] colliders = Physics.OverlapSphere(MyTransform.position, _hearRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                Bot bot = colliders[i].GetComponent<Bot>();
                if (bot != null)
                {
                    bot.GoToShots(MyTransform.position);
                }
            }
        }

        public override void Fire(Ammunition ammo)
        {
            if(_isReloading)
            {
                return;
            }

            if(CurrentAmmo <= 0)
            {
                Reload();
                return;
            }

            if (_canFire && ammo)
            {
                var tempAmmo = Instantiate(ammo, _barrel.position, _barrel.rotation);
                tempAmmo.transform.parent = Main.Instance.ProjectileContainer;
                Vector3 ammoDir = _rootPlayer.forward + (_barrel.right * Random.Range(-_dispersion, _dispersion));
                tempAmmo.MyRigidBody.AddForce(ammoDir * _force);
                makeShotSound();
                CurrentAmmo--;
                _canFire = false;
                AnimateAttack();
                PlayParticles();
                PlayAudio(_fireAudio);
                StartCoroutine(WaitCooldown());
            }
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