using System;
using UnityEngine;

namespace MyShooter
{
    [Serializable]
    public class Vision
    {
        public float ActiveDist = 10f;
        public float ActiveAngle = 35f;

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
            return !RayToScan(player, target);
        }

        public int rays = 14;

        bool RayToScan(Transform player, Transform target)
        {
            bool result = false;
            bool a = false;
            bool b = false;
            float j = 0;
            for (int i = 0; i < rays; i++)
            {
                var x = Mathf.Sin(j);
                var y = Mathf.Cos(j);

                j += ActiveAngle * Mathf.Deg2Rad / rays;

                Vector3 dir = player.TransformDirection(new Vector3(x, 0, y));
                if (GetRaycast(dir, player, target)) a = true;

                if (x != 0)
                {
                    dir = player.TransformDirection(new Vector3(-x, 0, y));
                    if (GetRaycast(dir, player, target)) b = true;
                }
            }

            if (a || b) result = true;
            return result;
        }

        bool GetRaycast(Vector3 dir, Transform player, Transform target)
        {
            bool result = false;
            RaycastHit hit = new RaycastHit();
            Vector3 pos = player.position + Vector3.up * 1;
            if (Physics.Raycast(pos, dir, out hit, ActiveDist))
            {
                if (hit.transform == target)
                {
                    result = true;
#if UNITY_EDITOR
                    Debug.DrawLine(pos, hit.point, Color.green);
#endif
                }
                else
                {
#if UNITY_EDITOR
                    Debug.DrawLine(pos, hit.point, Color.blue);
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.DrawRay(pos, dir * ActiveDist, Color.red);
#endif
            }
            return result;
        }

        private bool CheckAngle(Transform player, Transform target)
        {
            var angle = Vector3.Angle(player.forward, target.position - player.position);
            return angle <= ActiveAngle;
        }
    }
}