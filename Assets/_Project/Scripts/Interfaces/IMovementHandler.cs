using UnityEngine;

namespace Noko.Interfaces
{
    public interface IMovementHandler
    {
        void SetVelocity(Vector3 velocity);
        void SetPosition(Vector3 position);
        void SetRotation(Quaternion rotation);
    }
}