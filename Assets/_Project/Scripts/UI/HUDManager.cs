using UnityEngine;
using TMPro;
using Noko.Data;

namespace Noko.UI
{
    public class HUDManager : MonoBehaviour
    {
        [Header("Gold Display")]
        [SerializeField] private TextMeshProUGUI goldText;

        [Header("Upgrade Popup")]
        [SerializeField] private UpgradePopupUI upgradePopup;

        [Header("Data Reference")]
        [SerializeField] private WalletData walletData;

        private void Start()
        {
            if (walletData)
            {
                walletData.Initialize();
                walletData.OnGoldChanged += UpdateGoldUI;
                UpdateGoldUI(walletData.CurrentGold);
            }
        }

        private void UpdateGoldUI(int amount)
        {
            if (goldText) goldText.text = amount.ToString();
        }

        public void OnUpgradeButtonPressed()
        {
            if (upgradePopup) upgradePopup.Show();
        }

        private void OnDestroy()
        {
            if (walletData) walletData.OnGoldChanged -= UpdateGoldUI;
        }
    }
}