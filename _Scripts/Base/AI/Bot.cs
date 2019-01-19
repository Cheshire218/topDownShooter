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


        [SerializeField] private Transform _target;
        private bool _isPatrol = true;

        [SerializeField] private Vision _vision;
        private Patrol _patrol;
        private bool _isDead = false;
        private NavMeshAgent _navMeshAgent;
        private float _curIdleTime = 0;

        private BaseWeapon _weapon;
        

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

        private void Die(Vector3 direction, float forceValue)
        {
            _isDead = true;
            if (_navMeshAgent != null) _navMeshAgent.enabled = false;
            foreach (var child in GetComponentsInChildren<Transform>())
            {
                child.parent = null;
                var tempRB = child.GetComponent<Rigidbody>();
                if (!tempRB)
                {
                    child.gameObject.AddComponent<Rigidbody>().AddForce(direction * forceValue);
                    Destroy(child.gameObject, 5);
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _hp = MaxHp;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _patrol = new Patrol();
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _weapon = GetComponentInChildren<BaseWeapon>();
        }

        private void Update()
        {
            if (_isDead) return;


            if(_isPatrol)
            {
                if (!_navMeshAgent.hasPath)
                {
                    _curIdleTime += Time.deltaTime;
                    if (_curIdleTime >= _idleTime)
                    {
                        _curIdleTime = 0;
                        Vector3 destinationPoint = _patrol.GetRandomPoint(MyTransform.position, _minRadius, _maxRadius, false);
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
                _navMeshAgent.SetDestination(_target.position);
                if(_vision.CalcVision(MyTransform, _target))
                {
                    if(_weapon != null)
                    {
                        _weapon.Fire(_weapon.Ammo);
                    }
                }
                else
                {
                    _isPatrol = true;
                    _curIdleTime = 0;
                }
            }
            
        }
    }
}