using System;
using UnityEngine;
using Noko.Data;

namespace Noko.Systems
{
    public static class EventBus
    {
        // Player lifecycle
        public static event Action<GameObject> OnPlayerSpawned;
        public static event Action<GameObject> OnPlayerDied;
        
        // Upgrade events
        public static event Action<UpgradeType, int> OnUpgradePurchased;
        
        // Combat events
        public static event Action<GameObject, float, GameObject> OnDamageDealt;  // victim, amount, source
        public static event Action<GameObject, float> OnDamageTaken;             // victim, amount
        public static event Action<GameObject> OnEntityDied;                     // victim
        
        // Skill events
        public static event Action<int> OnSkillExecuted; // skill index
        
        // Game feel events
        public static event Action<Vector3> OnHitEffectRequested;
        public static event Action<Vector3> OnDeathEffectRequested;
        
        public static void TriggerPlayerSpawned(GameObject player) => OnPlayerSpawned?.Invoke(player);
        public static void TriggerPlayerDied(GameObject player) => OnPlayerDied?.Invoke(player);
        public static void TriggerUpgradePurchased(UpgradeType type, int level) => OnUpgradePurchased?.Invoke(type, level);
        public static void TriggerDamageDealt(GameObject victim, float amount, GameObject source) => OnDamageDealt?.Invoke(victim, amount, source);
        public static void TriggerDamageTaken(GameObject victim, float amount) => OnDamageTaken?.Invoke(victim, amount);
        public static void TriggerEntityDied(GameObject victim) => OnEntityDied?.Invoke(victim);
        public static void TriggerSkillExecuted(int index) => OnSkillExecuted?.Invoke(index);
        public static void TriggerHitEffect(Vector3 position) => OnHitEffectRequested?.Invoke(position);
        public static void TriggerDeathEffect(Vector3 position) => OnDeathEffectRequested?.Invoke(position);
    }
}