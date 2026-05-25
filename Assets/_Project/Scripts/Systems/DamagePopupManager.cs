using UnityEngine;

namespace Noko.Systems
{
    public class DamagePopupManager : MonoBehaviour
    {
        [SerializeField] private GameObject dynamicTextPrefab;
        [SerializeField] private DynamicTextData textData;

        private void OnEnable() => EventBus.OnDamageDealt += ShowDamagePopup;
        private void OnDisable() => EventBus.OnDamageDealt -= ShowDamagePopup;

        private void ShowDamagePopup(GameObject victim, float amount, GameObject source)
        {
            if (!victim || !dynamicTextPrefab) return;

            Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), 1.5f, Random.Range(-0.5f, 0.5f));
            Vector3 worldPos = victim.transform.position + randomOffset;

            GameObject popup = Instantiate(dynamicTextPrefab, worldPos, Quaternion.identity);

            if (popup.TryGetComponent(out DynamicText dynText) && textData)
            {
                dynText.Initialise(Mathf.RoundToInt(amount).ToString(), textData);
            }
        }
    }
}