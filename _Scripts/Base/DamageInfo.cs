using UnityEngine;

namespace MyShooter
{
    public struct DamageInfo
    {
        private readonly float _damage;
        private readonly Vector3 _damageDirection;
        private readonly float _forceValue;
        
        public DamageInfo(float damage, Vector3 damageDir, float forceValue)
        {
            _damage = damage;
            _damageDirection = damageDir;
            _forceValue = forceValue;
        }

        public float Damage
        {
            get
            {
                return _damage;
            }
        }

        public Vector3 DamageDirection
        {
            get
            {
                return _damageDirection;
            }
        }

        public float ForceValue
        {
            get
            {
                return _forceValue;
            }
        }
    }
}