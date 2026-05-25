using UnityEngine;

namespace Noko.Data
{
    public abstract class CharacterData : ScriptableObject
    {
        public float moveSpeed;
        public float rotationSpeed;
        public float maxHealth;
    }
}