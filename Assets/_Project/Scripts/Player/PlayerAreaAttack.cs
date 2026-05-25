using System.Collections.Generic;
using UnityEngine;
using Noko.Interfaces;
using Noko.Systems;
using Noko.Data;

namespace Noko.Player
{
    public class PlayerAreaAttack : MonoBehaviour
    {
        [Header("Data & References")]
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PlayerCombatArea combatArea;

        [Header("Combat Settings")]
        [SerializeField] private float minAttackInterval;

        private float _nextAttackTime;
        private float _currentAttackInterval;
        private float _currentDamage;

        private IUpgradeProvider _upgradeProvider;
        private TargetDetector _targetDetector;

        private void Awake()
        {
            _upgradeProvider = FindFirstObjectByType<UpgradeManager>();
        }

        private void Start()
        {
            _targetDetector = combatArea ? combatArea.Detector : null;
            UpdateAttackStats();

            if (_upgradeProvider != null) _upgradeProvider.OnUpgradeLeveledUp += OnUpgradeChanged;
        }

        private void Update()
        {
            TryAreaAttack();
        }

        private void TryAreaAttack()
        {
            if (Time.time < _nextAttackTime) return;
            if (!_targetDetector) return;

            IReadOnlyCollection<Transform> targets = _targetDetector.GetTargetsInRange();

            if (targets == null || targets.Count == 0) return;

            _nextAttackTime = Time.time + _currentAttackInterval;

            foreach (Transform target in targets)
            {
                if (target && target.TryGetComponent<IDamageable>(out var damageable) && damageable.IsAlive)
                {
                    damageable.TakeDamage(_currentDamage, gameObject);
                    EventBus.TriggerHitEffect(target.position);
                    EventBus.TriggerDamageDealt(target.gameObject, _currentDamage, gameObject);
                }
            }
        }

        private void UpdateAttackStats()
        {
            if (_upgradeProvider == null) return;

            float damageBonus = _upgradeProvider.GetUpgradeValue(UpgradeType.Damage);
            float speedBonus = _upgradeProvider.GetUpgradeValue(UpgradeType.AttackSpeed);

            _currentDamage = playerData.baseAttackDamage + damageBonus;

            _currentAttackInterval = Mathf.Max(minAttackInterval, playerData.baseAttackInterval - speedBonus);
        }

        private void OnUpgradeChanged(UpgradeData data, int newLevel)
        {
            if (data.upgradeType is UpgradeType.Damage or UpgradeType.AttackSpeed) UpdateAttackStats();
        }

        private void OnDestroy()
        {
            if (_upgradeProvider != null) _upgradeProvider.OnUpgradeLeveledUp -= OnUpgradeChanged;
        }
    }
}