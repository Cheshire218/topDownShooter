using UnityEngine;

namespace MyShooter
{
    public class PlayerHealth : BaseObjectScene, IDamageable
    {
        [SerializeField] private float _maxHp = 100;

        [SerializeField] private UnityEngine.UI.Slider _hpBar;
        [SerializeField] private UnityEngine.UI.Text _hpText;

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
                if (_hpText != null) _hpText.text = _currentHp.ToString();
                if (_hpBar != null) _hpBar.value = _currentHp/_maxHp;
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
            SetKinematic(true);
            _isDead = false;
            Hp = _maxHp;
        }


        public void GetDamage(DamageInfo damageInfo)
        {
            if (_isDead) return;
            if (_currentHp > 0)
            {
                Hp -= damageInfo.Damage;
            }
            if (_currentHp <= 0)
            {
                _isDead = true;
                Die(damageInfo.DamageDirection, damageInfo.ForceValue);
            }
        }

        private void Die(Vector3 direction, float forceValue)
        {
            _isDead = true;
            Main.Instance._inputController.Off();
            SetKinematic(false);
            Collider col = GetComponent<Collider>();
            if(col != null)col.enabled = false;
            MyRigidBody.useGravity = false;
            MyRigidBody.isKinematic = true;
            Animator _animator = GetComponent<Animator>();
            if (_animator != null) _animator.enabled = false;
            Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rb in bodies)
            {
                rb.AddForce(direction * forceValue);
            }
            //Destroy(MyGameObject, 3);
            Invoke(nameof(EndGame), 3);
        }

        private void EndGame()
        {
            Main.Instance.Interface.Execute(MyShooter.UI.InterfaceObject.EndGameMenu);
        }

        void SetKinematic(bool newValue)
        {
            Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in bodies)
            {
                rb.isKinematic = newValue;
                rb.detectCollisions = !newValue;
            }
            MyRigidBody.detectCollisions = newValue;
            MyRigidBody.isKinematic = !newValue;
        }



    }
}