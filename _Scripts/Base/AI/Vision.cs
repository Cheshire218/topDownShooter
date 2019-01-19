using System;
using UnityEngine;

namespace MyShooter
{
    [Serializable]
    public class Vision
    {
        public float ActiveDist = 20f;
        public float ActiveAngle = 58f;

        public bool CalcVision(Transform player, Transform target)
        {
            return CheckDistance(player, target) && CheckAngle(player, target) && !CheckBlocked(player, target);
        }

        private bool CheckDistance(Transform player, Transform target)
        {
            var dist = Vector3.Distance(player.position, target.position);
            return dist <= ActiveDist;
        }

        private bool CheckBlocked(Transform player, Transform target)
        {
            RaycastHit hit;
            if (!Physics.Linecast(player.position, target.position, out hit))
            {
                return true;
            }
            return hit.transform != target;
        }

        private bool CheckAngle(Transform player, Transform target)
        {
            var angle = Vector3.Angle(player.forward, target.position - player.position);
            return angle <= ActiveAngle;
        }
    }
}