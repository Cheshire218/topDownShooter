using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter
{
    public class PlayerMovementController : BaseController
    {
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
        }

        #region On/Off controller;
        public override void On()
        {
            if (Enabled) return;
            base.On();
            if (_playerMovement != null)
            {
                _playerMovement.Switch(isActive: true);
            }
        }

        public override void Off()
        {
            if (!Enabled) return;
            base.Off();
            if (_playerMovement != null)
            {
                _playerMovement.Switch(isActive: false);
            }
        }
        #endregion;

        public void Move(float v, float h)
        {
            if (!Enabled) return;
            Vector3 move = h * Vector3.right + v * Vector3.forward;
            if (move.magnitude > 1f)
            {
                move.Normalize();
            }
            _playerMovement.Move(move);
        }
    }
}