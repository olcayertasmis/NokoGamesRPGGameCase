using UnityEngine;
using Noko.Data;

namespace Noko.Interfaces
{
    public interface ICharacter
    {
        Transform Transform { get; }
        GameObject GameObject { get; }
        CharacterData CharacterData { get; }
        bool IsAlive { get; }
        void Move(Vector3 direction, float speed);
        void Rotate(Vector3 direction);
        void UpdateAnimation(float speedValue);
    }
}