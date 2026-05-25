using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Noko.Data;
using Noko.Systems;
using System.Collections.Generic;

namespace Noko.UI
{
    public class UpgradePopupUI : MonoBehaviour
    {
        [Header("Upgrade Buttons")]
        [SerializeField] private Button damageButton;
        [SerializeField] private Button healthButton;
        [SerializeField] private Button attackSpeedButton;
        [SerializeField] private Button moveSpeedButton;

        [Header("Text Labels")]
        [SerializeField] private TextMeshProUGUI damageLevelText;
        [SerializeField] private TextMeshProUGUI damageCostText;
        [SerializeField] private TextMeshProUGUI healthLevelText;
        [SerializeField] private TextMeshProUGUI healthCostText;
        [SerializeField] private TextMeshProUGUI attackSpeedLevelText;
        [SerializeField] private TextMeshProUGUI attackSpeedCostText;
        [SerializeField] private TextMeshProUGUI moveSpeedLevelText;
        [SerializeField] private TextMeshProUGUI moveSpeedCostText;

        [Header("References")]
        [SerializeField] private UpgradeManager upgradeManager;
        [SerializeField] private WalletData walletData;

        [Header("Game Feel")]
        [SerializeField] private AudioClip purchaseSfx;
        [SerializeField] private GameObject purchaseVfxPrefab;

        private Dictionary<UpgradeType, Button> _buttons;
        private Dictionary<UpgradeType, TextMeshProUGUI> _levelTexts;
        private Dictionary<UpgradeType, TextMeshProUGUI> _costTexts;

        private void Awake()
        {
            InitializeDictionaries();
            RegisterButtonListeners();
            //UpdateAllUI();

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            UpdateAllUI();
        }

        private void InitializeDictionaries()
        {
            _buttons = new Dictionary<UpgradeType, Button>
            {
                { UpgradeType.Damage, damageButton },
                { UpgradeType.MaxHealth, healthButton },
                { UpgradeType.AttackSpeed, attackSpeedButton },
                { UpgradeType.MovementSpeed, moveSpeedButton }
            };

            _levelTexts = new Dictionary<UpgradeType, TextMeshProUGUI>
            {
                { UpgradeType.Damage, damageLevelText },
                { UpgradeType.MaxHealth, healthLevelText },
                { UpgradeType.AttackSpeed, attackSpeedLevelText },
                { UpgradeType.MovementSpeed, moveSpeedLevelText }
            };

            _costTexts = new Dictionary<UpgradeType, TextMeshProUGUI>
            {
                { UpgradeType.Damage, damageCostText },
                { UpgradeType.MaxHealth, healthCostText },
                { UpgradeType.AttackSpeed, attackSpeedCostText },
                { UpgradeType.MovementSpeed, moveSpeedCostText }
            };
        }

        private void RegisterButtonListeners()
        {
            foreach (var kvp in _buttons)
            {
                UpgradeType type = kvp.Key;
                kvp.Value.onClick.AddListener(() => OnUpgradeButtonClicked(type));
            }
        }

        private void OnUpgradeButtonClicked(UpgradeType type)
        {
            if (!upgradeManager) return;

            bool success = upgradeManager.TryBuyUpgrade(type);
            if (success)
            {
                UpdateAllUI();


                if (purchaseSfx) AudioSource.PlayClipAtPoint(purchaseSfx, Camera.main.transform.position);
                if (purchaseVfxPrefab)
                {
                    Vector3 pos = transform.position;
                    GameObject vfx = Instantiate(purchaseVfxPrefab, pos, Quaternion.identity);
                    Destroy(vfx, 2.5f);
                }

                EventBus.TriggerUpgradePurchased(type, upgradeManager.GetUpgradeLevel(type));
            }
            else
            {
                Debug.Log($"Cannot upgrade {type} - insufficient gold or max level reached");
            }
        }

        private void UpdateAllUI()
        {
            foreach (var type in _buttons.Keys)
            {
                UpdateUpgradeUI(type);
            }
        }

        private void UpdateUpgradeUI(UpgradeType type)
        {
            if (!upgradeManager) return;

            int currentLevel = upgradeManager.GetUpgradeLevel(type);
            UpgradeData data = upgradeManager.GetUpgradeData(type);

            if (!data)
            {
                _buttons[type].interactable = false;
                if (_levelTexts[type]) _levelTexts[type].text = "MAX";
                if (_costTexts[type]) _costTexts[type].text = "-";
                return;
            }

            bool isMaxLevel = currentLevel >= data.maxLevel;
            bool canAfford = walletData && walletData.CurrentGold >= data.GetCostAtLevel(currentLevel);
            _buttons[type].interactable = !isMaxLevel && canAfford;

            if (_levelTexts[type]) _levelTexts[type].text = isMaxLevel ? "MAX" : $"Level {currentLevel}";

            if (_costTexts[type] && !isMaxLevel)
            {
                int cost = data.GetCostAtLevel(currentLevel);
                _costTexts[type].text = cost.ToString();

                //bool canAfford = walletData && walletData.CurrentGold >= cost;
                _costTexts[type].color = canAfford ? Color.green : Color.red;
            }
            else if (_costTexts[type])
            {
                _costTexts[type].text = "MAX";
                _costTexts[type].color = Color.gray;
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            UpdateAllUI();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}