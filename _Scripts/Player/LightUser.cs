using UnityEngine;

namespace MyShooter
{
    public sealed class LightUser : BaseObjectScene
    {
        private Light _light;

        protected override void Awake()
        {
            base.Awake();
            _light = GetComponent<Light>();
        }

        public void Switch(bool isActive)
        {
            if (!_light) return;
            _light.enabled = isActive;
        }
    }
}