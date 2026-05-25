using UnityEngine;
using Noko.Data;
using System;
using System.Collections.Generic;
using Noko.Interfaces;

namespace Noko.Systems
{
    public class UpgradeManager : MonoBehaviour, IUpgradeProvider
    {
        [Header("Data References")]
        [SerializeField] private WalletData playerWallet;
        [SerializeField] private List<UpgradeData> availableUpgrades;

        private Dictionary<UpgradeType, float> _cachedValues = new();

        public event Action<UpgradeData, int> OnUpgradeLeveledUp;

        private void Awake()
        {
            if (availableUpgrades != null)
            {
                foreach (var upgrade in availableUpgrades)
                {
                    if (upgrade) UpdateCachedValue(upgrade.upgradeType);
                }
            }
        }

        private void UpdateCachedValue(UpgradeType type)
        {
            _cachedValues[type] = GetUpgradeData(type)?.GetValueAtLevel(GetUpgradeLevel(type)) ?? 0f;
        }

        public int GetUpgradeLevel(UpgradeType type)
        {
            return PlayerPrefs.GetInt($"Upgrade_{type}", 0);
        }

        public float GetUpgradeValue(UpgradeType type) => _cachedValues.GetValueOrDefault(type, 0f);

        public UpgradeData GetUpgradeData(UpgradeType type)
        {
            return availableUpgrades.Find(u => u.upgradeType == type);
        }

        public bool TryBuyUpgrade(UpgradeType type)
        {
            UpgradeData data = availableUpgrades.Find(u => u.upgradeType == type);
            if (!data) return false;

            int currentLevel = GetUpgradeLevel(type);
            if (currentLevel >= data.maxLevel) return false;

            int cost = data.GetCostAtLevel(currentLevel);

            if (playerWallet.SpendGold(cost))
            {
                currentLevel++;
                PlayerPrefs.SetInt($"Upgrade_{type}", currentLevel);
                PlayerPrefs.Save();

                UpdateCachedValue(type);

                OnUpgradeLeveledUp?.Invoke(data, currentLevel);
                return true;
            }

            return false;
        }
    }
}