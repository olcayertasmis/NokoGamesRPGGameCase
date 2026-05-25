using System.Collections;
using UnityEngine;

namespace Noko.Systems
{
    public class MaterialFlash : MonoBehaviour
    {
        [SerializeField] private Material flashMaterial;
        [SerializeField] private Renderer flashRenderer;
        [SerializeField] private float flashDuration;

        private Material _originalMaterial;
        private bool _isFlashing;

        private Coroutine _flashCoroutine;

        private void Awake()
        {
            _originalMaterial = flashRenderer.material;
        }

        public void TriggerFlash()
        {
            if (_isFlashing) return;
            _flashCoroutine = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            _isFlashing = true;
            flashRenderer.material = flashMaterial;
            yield return new WaitForSeconds(flashDuration);
            flashRenderer.material = _originalMaterial;
            _isFlashing = false;
        }

        public void ResetFlash()
        {
            if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
            if (flashRenderer) flashRenderer.material = _originalMaterial;
        }
    }
}