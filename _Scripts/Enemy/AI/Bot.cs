using UnityEngine;
using UnityEngine.AI;

namespace MyShooter
{
    public class Bot : BaseObjectScene, IDamageable
    {
        [SerializeField] private float _hp;
        [SerializeField] private float _maxHp = 100;
        [SerializeField] private float _idleTime = 3;
        [SerializeField] private float _minRadius = 4;
        [SerializeField] private float _maxRadius = 8;
        [SerializeField] private float _attackRange = 10;
        [SerializeField] private float _stopRange = 5;


        [SerializeField] private Transform _target;
        [SerializeField] private bool _isPatrol = true;

        [SerializeField] private Vision _vision;
        private Patrol _patrol;
        protected bool _isDead = false;
        private NavMeshAgent _navMeshAgent;
        private float _curIdleTime = 0;
        private Animator _animator;
        private BaseWeapon _weapon;
        
        public bool IsDead
        {
            get
            {
                return _isDead;
            }
        }

        public float Hp {
            get
            {
                return _hp;
            }
            private set
            {
                _hp = value;
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
            if (_hp > 0)
            {
                _hp -= damageInfo.Damage;
            }
            if (_hp <= 0)
            {
                Die(damageInfo.DamageDirection, damageInfo.ForceValue);
            }
        }

        void SetKinematic(bool newValue)
        {
            Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in bodies)
            {
                rb.isKinematic = newValue;
                rb.detectCollisions = !newValue;
            }
        }

        private void Die(Vector3 direction, float forceValue)
        {
            _isDead = true;
            _weapon.StopShooting();
            SetKinematic(false);
            if (_navMeshAgent != null) _navMeshAgent.enabled = false;
            if (_animator != null) _animator.enabled = false;

            Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in bodies)
            {
                rb.AddForce(direction * forceValue);
            }
            Destroy(MyGameObject, 15);
        }

        protected override void Awake()
        {
            base.Awake();
            _hp = MaxHp;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _patrol = new Patrol();
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _weapon = GetComponentInChildren<BaseWeapon>();
            _animator = GetComponent<Animator>();
            SetKinematic(true);
        }

        private void SetAnimatorWalk(bool walk)
        {
            if (!_animator) return;
            _animator.SetBool("walk", walk);
        }

        public void GoToShots(Vector3 shotPos)
        {
            if (_navMeshAgent.enabled && _isPatrol)
            {
                _curIdleTime = 0;
                _navMeshAgent.SetDestination(shotPos);
            }
        }

        void OnAnimatorIK() //действия с аватаром (который на аниматоре)
        {
            if(!_isPatrol)
            {
                _animator.SetLookAtWeight(1, 0.6f, 0.8f, 1, 1);
                _animator.SetLookAtPosition(_target.position + Vector3.up * 1.5f);
            }
            else
            {
                _animator.SetLookAtWeight(0, 0, 0, 0, 0);
            }
        }

        private void Update()
        {
            if (_isDead) return;

            
            if (_isPatrol)
            {
                if (!_navMeshAgent.hasPath)
                {
                    _curIdleTime += Time.deltaTime;
                    if (_curIdleTime >= _idleTime)
                    {
                        _curIdleTime = 0;
                        Vector3 destinationPoint = _patrol.GetRandomPoint(MyTransform.position, _minRadius, _maxRadius, true);
                        NavMeshPath path = new NavMeshPath();
                        bool isWalkable = _navMeshAgent.CalculatePath(destinationPoint, path);
                        if (isWalkable && path.status == NavMeshPathStatus.PathComplete)
                        {
                            _navMeshAgent.SetDestination(destinationPoint);
                        }
                    }
                }
                if(_vision.CalcVision(MyTransform, _target))
                {
                    _isPatrol = false;
                }
            }
            else
            {
                if(_vision.CalcVision(MyTransform, _target))
                {
                    float distance = Vector3.Distance(MyTransform.position, _target.position);
                    if(distance <= _attackRange && _weapon != null)
                    { 
                        _weapon.Fire(_weapon.Ammo);
                    }
                    if(distance <= _stopRange)
                    {
                        if (_navMeshAgent.hasPath) _navMeshAgent.ResetPath();
                        MyTransform.LookAt(_target.position);
                    }
                    else
                    {
                        _navMeshAgent.SetDestination(_target.position);
                    }
                }
                else
                {
                    _isPatrol = true;
                    _curIdleTime = 0;
                }
            }
            if (_navMeshAgent.velocity != Vector3.zero)
            {
                SetAnimatorWalk(true);
            }
            else
            {
                SetAnimatorWalk(false);
            }
        }
    }
}