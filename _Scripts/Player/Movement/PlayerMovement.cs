using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : BaseObjectScene
    {
        private Rigidbody _rigidbody;
        //private Animator _animator;
        //private CapsuleCollider _collider;
        private bool canMove;
        private int _floorMask;

        [SerializeField] private float cameraLength = 30;

        #region Camera variables;
        private Transform _cameraTransform;
        private Vector3 _offset;
        #endregion;

        [SerializeField]
        private float speed = 10f;

        protected override void Awake()
        {
            base.Awake();
            _floorMask = LayerMask.GetMask("Ground");
            //_collider = GetComponent<CapsuleCollider>();
            //_animator = GetComponent<Animator>();
            if (MyRigidBody != null)
            {
                MyRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }

            _cameraTransform =  Camera.main.transform;
            if (_cameraTransform != null)
            {
                _offset = _cameraTransform.position - MyTransform.position;
            }
        }

        private void FixedUpdate()
        {
            LookAtMouse();
        }

        private void LateUpdate()
        {
            Vector3 desiredPosition = MyTransform.position + _offset;
            _cameraTransform.position = desiredPosition;
            _cameraTransform.LookAt(MyTransform);
        }

        public void Move(Vector3 move)
        {
            if (!MyRigidBody || !canMove) return;
            MyRigidBody.MovePosition(MyTransform.position + move * Time.fixedDeltaTime * speed);
        }

        private void LookAtMouse()
        {
            if (!_cameraTransform) return;
            Camera cam = _cameraTransform.GetComponent<Camera>();
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, cameraLength, _floorMask))
            {
                Vector3 playerToMouseDirection = hit.point - MyTransform.position;
                playerToMouseDirection.y = 0f;
                Quaternion newRotation = Quaternion.LookRotation(playerToMouseDirection);
                MyRigidBody.MoveRotation(newRotation);
            }
        }

        public void Switch(bool isActive)
        {
            canMove = isActive;
        }
    }
}