using System.Collections.Generic;
using UnityEngine;
using Noko.Interfaces;
using Noko.Systems;

namespace Noko.Player
{
    public class TargetDetector : MonoBehaviour, ITargetProvider
    {
        private readonly HashSet<Transform> _targetsInRange = new();

        private void OnEnable() => EventBus.OnEntityDied += RemoveDeadTarget;
        private void OnDisable() => EventBus.OnEntityDied -= RemoveDeadTarget;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && damageable.IsAlive) _targetsInRange.Add(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable _)) _targetsInRange.Remove(other.transform);
        }

        private void RemoveDeadTarget(GameObject victim) => _targetsInRange.Remove(victim.transform);

        public Transform GetClosestTarget(Vector3 origin)
        {
            Transform closest = null;
            float minDist = float.MaxValue;
            foreach (var t in _targetsInRange)
            {
                float dist = (t.position - origin).sqrMagnitude;
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = t;
                }
            }

            return closest;
        }

        public IReadOnlyCollection<Transform> GetTargetsInRange() => _targetsInRange;
    }
}