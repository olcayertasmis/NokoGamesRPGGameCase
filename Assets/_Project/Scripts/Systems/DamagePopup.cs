using UnityEngine;
using TMPro;

namespace Noko.Systems
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textMesh;
        [SerializeField] private float lifetime;
        [SerializeField] private float floatSpeed;

        private float _timer;

        public void Setup(float damageAmount, Vector3 worldPosition)
        {
            transform.position = worldPosition;
            textMesh.text = Mathf.RoundToInt(damageAmount).ToString();
            _timer = lifetime;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            UpdatePopupMovement();
        }

        private void UpdatePopupMovement()
        {
            transform.position += Vector3.up * (floatSpeed * Time.deltaTime);
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}