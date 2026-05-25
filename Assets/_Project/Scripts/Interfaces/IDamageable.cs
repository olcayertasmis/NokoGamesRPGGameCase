using UnityEngine;

namespace Noko.Interfaces
{
    public interface IDamageable
    {
        bool IsAlive { get; }
        void TakeDamage(float amount, GameObject source = null);
    }
}