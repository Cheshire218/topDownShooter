using UnityEngine;

namespace MyShooter
{
    public class Enemy : BaseObjectScene, IDamageable
    {
        [SerializeField] private float _hp;
        [SerializeField] private float _maxHp = 100;
        private bool _isDead = false;
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
            _hp = _maxHp;
        }


        public float Hp
        {
            get
            {
                return _hp;
            }
        }

        public float MaxHp
        {
            get
            {
                return _maxHp;
            }
        }

        public void GetDamage(DamageInfo damageInfo)
        {
            if(_hp > 0)
            {
                _hp -= damageInfo.Damage;
            }
            if(_hp <= 0)
            {
                Destroy(MyGameObject, 3);
            }
        }
    }
}