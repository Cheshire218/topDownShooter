using UnityEngine;

namespace MyShooter
{
    public abstract class Ammunition : BaseObjectScene
    {
        [SerializeField] protected float _timeToDestroy = 10f;
        [SerializeField] protected float _baseDamage = 20f;
        [SerializeField] protected float _forceValue = 1f;

        protected float _damage;

        protected override void Awake()
        {
            base.Awake();
            _damage = _baseDamage;
            Destroy(MyGameObject, _timeToDestroy);
        }
    }
}