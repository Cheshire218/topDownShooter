using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MyShooter
{
    public class Patrol
    {
        private List<Vector3> _points = new List<Vector3>();
        private int _indexPoint = 0;

        public Patrol()
        {
            var _tempPoints = GameObject.FindObjectsOfType<WayPoint>();

            _points = _tempPoints.Select(point => point.transform.position).ToList();
        }

        public Vector3 GetRandomPoint(Vector3 agentPosition, float minRadius, float maxRadius, bool isRandom = true)
        {
            Vector3 result;

            if (isRandom)
            {
                float distance = UnityEngine.Random.Range(minRadius, maxRadius);
                Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * distance;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(agentPosition + randomPoint, out hit, distance, NavMesh.AllAreas))
                {
                    result = hit.position;
                }
                else
                {
                    result = agentPosition;
                }
            }
            else
            {
                if (_indexPoint < _points.Count - 1)
                {
                    _indexPoint++;
                }
                else
                {
                    _indexPoint = 0;
                }
                result = _points[_indexPoint];
            }
            return result;
        }
    }
}