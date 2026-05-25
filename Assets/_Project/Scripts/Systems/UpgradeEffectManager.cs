using Noko.Data;
using UnityEngine;
using Noko.Systems;

namespace Noko.UI
{
    public class UpgradeEffectManager : MonoBehaviour
    {
        [SerializeField] private GameObject upgradeVfxPrefab;
        [SerializeField] private AudioClip upgradeSfx;

        private void Start()
        {
            EventBus.OnUpgradePurchased += PlayUpgradeEffect;
        }

        private void OnDestroy()
        {
            EventBus.OnUpgradePurchased -= PlayUpgradeEffect;
        }

        private void PlayUpgradeEffect(UpgradeType type, int level)
        {
            if (upgradeVfxPrefab && Camera.main) Instantiate(upgradeVfxPrefab, Camera.main.transform.position + Vector3.right * 2f, Quaternion.identity);

            if (upgradeSfx) AudioSource.PlayClipAtPoint(upgradeSfx, Camera.main.transform.position);
        }
    }
}