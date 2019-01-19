using UnityEngine;

namespace MyShooter
{
    public class Bullet : Ammunition
    {
        private void OnTriggerEnter(Collider other)
        {
            DamageTarget(other.GetComponent<IDamageable>());
            Destroy(MyGameObject);
        }

        private void DamageTarget(IDamageable obj)
        {
            if (obj != null) obj.GetDamage(new DamageInfo(_damage, MyTransform.forward, _forceValue));
        }
    }
}