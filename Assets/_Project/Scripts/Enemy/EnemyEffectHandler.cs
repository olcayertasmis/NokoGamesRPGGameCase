using System.Collections;
using UnityEngine;
using Noko.Systems;

namespace Noko.Enemy
{
    public class EnemyEffectHandler : MonoBehaviour
    {
        [SerializeField] private GameObject freezeEffect;

        private MaterialFlash _materialFlash;
        private Coroutine _activeSlowCoroutine;

        private void Awake() => _materialFlash = GetComponent<MaterialFlash>();

        public void ApplySlow(float multiplier, float duration, EnemyMovement movement)
        {
            if (_activeSlowCoroutine != null)
            {
                StopCoroutine(_activeSlowCoroutine);
            }

            _activeSlowCoroutine = StartCoroutine(SlowRoutine(multiplier, duration, movement));
        }

        private IEnumerator SlowRoutine(float multiplier, float duration, EnemyMovement movement)
        {
            freezeEffect.SetActive(true);

            movement.SetSpeedModifier(multiplier);

            yield return new WaitForSeconds(duration);
            
            /*
            float recoveryTime = duration * 0.3f;
            float elapsed = 0f;
            while (elapsed < recoveryTime)
            {
                elapsed += Time.deltaTime;
                float currentMod = Mathf.Lerp(multiplier, 1f, elapsed / recoveryTime);
                movement.SetSpeedModifier(currentMod);
                yield return null;
            }
            */

            movement.SetSpeedModifier(1f);

            freezeEffect.SetActive(false);

            _activeSlowCoroutine = null;
        }

        public void PlayHitFlash() => _materialFlash?.TriggerFlash();
        public void ResetFlash() => _materialFlash?.ResetFlash();
    }
}