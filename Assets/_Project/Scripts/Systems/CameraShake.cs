using UnityEngine;

namespace Noko.Systems
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float shakeDuration;
        [SerializeField] private float shakeMagnitude;

        private Vector3 _originalPosition;
        private float _shakeTimer;

        private void Start()
        {
            _originalPosition = transform.localPosition;
            EventBus.OnDamageTaken += OnDamageTaken;
        }

        private void OnDestroy()
        {
            EventBus.OnDamageTaken -= OnDamageTaken;
        }

        private void OnDamageTaken(GameObject victim, float amount)
        {
            if (victim.CompareTag("Player")) _shakeTimer = shakeDuration;
        }

        private void Update()
        {
            HandleCameraShake();
        }

        private void HandleCameraShake()
        {
            if (_shakeTimer > 0)
            {
                transform.localPosition = _originalPosition + Random.insideUnitSphere * shakeMagnitude;
                _shakeTimer -= Time.deltaTime;
            }
            else
            {
                transform.localPosition = _originalPosition;
            }
        }

        public void TriggerShake(float duration, float magnitude)
        {
            _shakeTimer = duration;
            shakeMagnitude = magnitude;
        }
    }
}