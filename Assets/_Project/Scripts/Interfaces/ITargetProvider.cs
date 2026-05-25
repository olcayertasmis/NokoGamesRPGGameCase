using UnityEngine;
using System.Collections.Generic;

namespace Noko.Interfaces
{
    public interface ITargetProvider
    {
        Transform GetClosestTarget(Vector3 origin);
        IReadOnlyCollection<Transform> GetTargetsInRange();
    }
}