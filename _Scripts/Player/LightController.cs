using UnityEngine;

namespace MyShooter
{
    public sealed class LightController : BaseController
    {
        private LightUser _lightUser;

        private void Awake()
        {
            _lightUser = FindObjectOfType<LightUser>();
        }

        private void Update()
        {
            if (!Enabled) return;
        }

        public override void On()
        {
            if (Enabled) return;
            base.On();
            if (_lightUser != null)
            {
                _lightUser.Switch(isActive: true);
            }
        }

        public override void Off()
        {
            if (!Enabled) return;
            base.Off();
            if (_lightUser != null)
            {
                _lightUser.Switch(isActive: false);
            }
        }

        public void Switch()
        {
            if(Enabled)
            {
                Off();
            }
            else
            {
                On();
            }
        }
    }
}