using UnityEngine;
using Noko.Core;
using Noko.Systems;
using Noko.Data;
using Noko.Combat;

namespace Noko.Player
{
    public class PlayerController : CharacterBase
    {
        private UpgradeManager _upgradeManager;

        protected override void Awake()
        {
            base.Awake();
            _upgradeManager = FindFirstObjectByType<UpgradeManager>();
        }

        private void OnEnable()
        {
            if (_upgradeManager) _upgradeManager.OnUpgradeLeveledUp += OnUpgradeLeveledUp;
        }

        private void OnDisable()
        {
            if (_upgradeManager) _upgradeManager.OnUpgradeLeveledUp -= OnUpgradeLeveledUp;
        }

        private void OnUpgradeLeveledUp(UpgradeData data, int newLevel)
        {
            if (data.upgradeType == UpgradeType.MaxHealth) ApplyMaxHealthUpgrade();
        }

        private void ApplyMaxHealthUpgrade()
        {
            if (!_upgradeManager) return;

            float bonusHealth = _upgradeManager.GetUpgradeValue(UpgradeType.MaxHealth);
            if (bonusHealth > 0 && TryGetComponent<HealthSystem>(out var healthSystem))
            {
                healthSystem.IncreaseMaxHealth(bonusHealth);
            }
        }
    }
}