using UnityEngine;
using Noko.Data;
using Noko.Interfaces;

namespace Noko.Core
{
    public abstract class CharacterBase : BaseMovement, ICharacter, IMovementHandler
    {
        [Header("Data Configuration")]
        [SerializeField] protected CharacterData characterData;

        public Transform Transform => transform;
        public GameObject GameObject => gameObject;
        public CharacterData CharacterData => characterData;
        public virtual bool IsAlive => true;

        protected virtual void Awake()
        {
            if (!characterRigidbody) Debug.LogError($"CharacterBase: Rigidbody not assigned on {gameObject.name}", this);
            if (!characterAnimator) Debug.LogError($"CharacterBase: Animator not assigned on {gameObject.name}", this);
            if (!characterData) Debug.LogError($"CharacterBase: CharacterData not assigned on {gameObject.name}", this);
        }

        public override void Move(Vector3 direction, float speed) => base.Move(direction, speed);
        protected virtual void Move(Vector3 direction) => Move(direction, characterData.moveSpeed);
        public virtual void Rotate(Vector3 direction) => base.Rotate(direction, characterData.rotationSpeed);
        public override void UpdateAnimation(float speedValue) => base.UpdateAnimation(speedValue);
    }
}