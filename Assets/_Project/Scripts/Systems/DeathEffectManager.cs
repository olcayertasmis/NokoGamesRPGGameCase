using UnityEngine;

namespace Noko.Systems
{
    public class DeathEffectManager : MonoBehaviour
    {
        [Header("VFX & SFX")]
        [SerializeField] private GameObject deathVfxPrefab;
        [SerializeField] private AudioClip deathSfx;

        [Header("Camera Shake Settings")]
        [SerializeField] private float cameraShakeDuration;
        [SerializeField] private float cameraShakeMagnitude;

        private void OnEnable() => EventBus.OnDeathEffectRequested += PlayDeathEffect;
        private void OnDisable() => EventBus.OnDeathEffectRequested -= PlayDeathEffect;

        private void PlayDeathEffect(Vector3 position)
        {
            if (deathVfxPrefab) Instantiate(deathVfxPrefab, position, Quaternion.identity);
            if (deathSfx) AudioSource.PlayClipAtPoint(deathSfx, position);

            var cam = Camera.main;
            if (cam && cam.TryGetComponent(out CameraShake shake))
            {
                shake.TriggerShake(cameraShakeDuration, cameraShakeMagnitude);
            }
        }
    }
}