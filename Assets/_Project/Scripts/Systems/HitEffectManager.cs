using Noko.Utilities;
using UnityEngine;
using UnityEngine.Pool;

namespace Noko.Systems
{
    public class HitEffectManager : MonoBehaviour
    {
        [SerializeField] private GameObject hitVfxPrefab;
        [SerializeField] private AudioClip hitSfx;
        [SerializeField] private float vfxLifetime;

        private IObjectPool<GameObject> _vfxPool;

        private void Start()
        {
            _vfxPool = ObjectPoolExtensions.CreatePool(hitVfxPrefab, 20, 50);

            EventBus.OnHitEffectRequested += PlayHitEffect;
        }

        private void OnDestroy()
        {
            EventBus.OnHitEffectRequested -= PlayHitEffect;
        }

        private void PlayHitEffect(Vector3 position)
        {
            var vfx = _vfxPool.Get();
            vfx.transform.position = position;

            if (hitSfx) AudioSource.PlayClipAtPoint(hitSfx, position);

            StartCoroutine(ReturnVfxAfterDelay(vfx));
        }

        private System.Collections.IEnumerator ReturnVfxAfterDelay(GameObject vfx)
        {
            yield return new WaitForSeconds(vfxLifetime);
            _vfxPool.Release(vfx);
        }
    }
}