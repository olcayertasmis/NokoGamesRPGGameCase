using UnityEngine;
using Noko.Data;
using Noko.Combat;

namespace Noko.Systems
{
    public class LootDropper : MonoBehaviour
    {
        [SerializeField] private LootData lootData;
        [SerializeField] private HealthSystem healthSystem;

        private LootPoolManager _poolManager;

        private void Awake()
        {
            _poolManager = FindFirstObjectByType<LootPoolManager>();
        }

        private void OnEnable()
        {
            if (healthSystem) healthSystem.OnDeath += DropLoot;
        }

        private void OnDisable()
        {
            if (healthSystem) healthSystem.OnDeath -= DropLoot;
        }

        private void DropLoot()
        {
            if (!lootData || !lootData.lootPrefab || !_poolManager) return;

            GameObject lootItemObj = _poolManager.GetLoot(lootData.lootPrefab);
            lootItemObj.transform.position = transform.position + Vector3.up * 0.1f;

            LootItem lootItem = lootItemObj.GetComponentInChildren<LootItem>();
            if (lootItem)
            {
                float amount = Random.Range(lootData.minAmount, lootData.maxAmount);
                lootItem.Initialize(amount, lootData.lootPrefab);
            }
        }
    }
}