using UnityEngine;

namespace MyShooter
{
    public class PlayerHealth : BaseObjectScene, IDamageable
    {
        [SerializeField] private float _maxHp = 100;

        public float MaxHp
        {
            get
            {
                return _maxHp;
            }
        }
        [SerializeField] private float _currentHp;
        public float Hp
        {
            get
            {
                return _currentHp;
            }
            private set
            {
                _currentHp = Mathf.Clamp(value, 0, _maxHp);
            }
        }

        private bool _isDead;

        public bool IsDead
        {
            get
            {
                return _isDead;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _isDead = false;
            _currentHp = _maxHp;
        }


        public void GetDamage(DamageInfo damageInfo)
        {

            if (_currentHp > 0)
            {
                _currentHp -= damageInfo.Damage;
            }
            if (_currentHp <= 0)
            {
                _isDead = true;
                Die();
            }
        }

        private void Die()
        {
            Main.Instance.Interface.Execute(MyShooter.UI.InterfaceObject.EndGameMenu);
        }
    }
}