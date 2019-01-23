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
        [SerializeField] protected int _maxAmmo = 20;
        [SerializeField] protected float _reloadTime = 2.1f;
        [SerializeField] protected int _currentAmmo;
        [SerializeField] protected bool _isReloading;
        [SerializeField] protected AudioClip _reloadAudio;
        [SerializeField] protected AudioClip _fireAudio;
        #endregion;

        #region Protected fields;
        protected bool _canFire = true;
        protected Transform _rootPlayer;
        protected Animator _animator;
        protected AudioSource _audio;
        #endregion;

        #region Abstract fuctions;
        public abstract void Fire(Ammunition ammo);
        protected abstract void AnimateReload();
        protected abstract void PlayParticles();
        public abstract void StopShooting();
        #endregion;

        protected override void Awake()
        {
            base.Awake();
            _currentAmmo = _maxAmmo;
            _isReloading = false;
            _rootPlayer = MyTransform.root;
            _animator = _rootPlayer.GetComponent<Animator>();
            _audio = _rootPlayer.gameObject.AddComponent<AudioSource>() as AudioSource;
        }

        protected void Start()
        {
            _audio.outputAudioMixerGroup = Main.Instance.Interface.InterfaceResources.AudioMixer.FindMatchingGroups("SoundVolume")[0];

            _audio.playOnAwake = false;
        }

        protected IEnumerator WaitCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            _canFire = true;
        }

        public void Reload()
        {
            if (_currentAmmo >= _maxAmmo || _isReloading) return;
            _isReloading = true;
            AnimateReload();
            PlayAudio(_reloadAudio);
            StartCoroutine(WaitReload());
        }

        public IEnumerator WaitReload()
        {
            yield return new WaitForSeconds(_reloadTime);
            _isReloading = false;
            _currentAmmo = _maxAmmo;
        }

        protected void PlayAudio(AudioClip clip)
        {
            _audio.PlayOneShot(clip);
        }
    }
}