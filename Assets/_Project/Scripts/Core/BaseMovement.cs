using UnityEngine;

namespace Noko.Core
{
    public abstract class BaseMovement : MonoBehaviour
    {
        [SerializeField] protected Rigidbody characterRigidbody;
        [SerializeField] protected Animator characterAnimator;
        protected static readonly int SpeedHash = Animator.StringToHash("Speed");
        private float _cachedSpeedValue = -1f;

        public virtual void Move(Vector3 direction, float speed)
        {
            if (direction.sqrMagnitude <= 0.01f) return;
            Vector3 targetPosition = characterRigidbody.position + direction * (speed * Time.fixedDeltaTime);
            characterRigidbody.MovePosition(targetPosition);
        }

        public virtual void Rotate(Vector3 direction, float rotationSpeed)
        {
            if (direction.sqrMagnitude <= 0.01f) return;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            characterRigidbody.rotation = Quaternion.Slerp(characterRigidbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        public virtual void UpdateAnimation(float speedValue)
        {
            float smoothedValue = Mathf.MoveTowards(characterAnimator.GetFloat(SpeedHash), speedValue, Time.deltaTime * 5f);

            if (Mathf.Abs(_cachedSpeedValue - smoothedValue) > 0.01f)
            {
                _cachedSpeedValue = smoothedValue;
                characterAnimator.SetFloat(SpeedHash, smoothedValue);
            }
        }

        public void SetVelocity(Vector3 velocity) => characterRigidbody.linearVelocity = velocity;
        public void SetPosition(Vector3 position) => characterRigidbody.position = position;
        public void SetRotation(Quaternion rotation) => characterRigidbody.rotation = rotation;
    }
}