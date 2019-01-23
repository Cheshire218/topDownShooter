using UnityEngine;

namespace MyShooter
{
    public class Bullet : Ammunition
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Ammunition>() != null) return;

            if (DamageTarget(other.GetComponent<IDamageable>()))
            {
                Destroy(MyGameObject);
            }
        }

        private bool DamageTarget(IDamageable obj)
        {
            if (obj != null && !obj.IsDead)
            {
                obj.GetDamage(new DamageInfo(_damage, MyTransform.forward, _forceValue));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}