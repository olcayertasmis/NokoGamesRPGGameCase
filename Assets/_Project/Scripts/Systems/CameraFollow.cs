using UnityEngine;

namespace Noko.Systems
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Follow Settings")]
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothTime;

        private Transform _target;
        private Vector3 _velocity = Vector3.zero;

        private void OnEnable()
        {
            EventBus.OnPlayerSpawned += HandlePlayerSpawned;
        }

        private void OnDisable()
        {
            EventBus.OnPlayerSpawned -= HandlePlayerSpawned;
        }

        private void HandlePlayerSpawned(GameObject player)
        {
            if (player) _target = player.transform;
        }

        private void FixedUpdate() // LateUpdate'i bununla değiştirin
        {
            if (!_target) return;
            Vector3 targetPosition = _target.position + offset;
            // SmoothDamp'in fizik ile uyumlu çalışması için Time.fixedDeltaTime kullanın
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime, Mathf.Infinity, Time.fixedDeltaTime);
        }
    }
}