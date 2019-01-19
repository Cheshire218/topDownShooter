using UnityEngine;

namespace MyShooter
{
    public class BaseController : MonoBehaviour
    {
        public bool Enabled { get; private set; }

        public virtual void On()
        {
            Enabled = true;
        }

        public virtual void Off()
        {
            Enabled = false;
        }
    }
}