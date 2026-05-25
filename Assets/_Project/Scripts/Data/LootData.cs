using UnityEngine;

namespace Noko.Data
{
    [CreateAssetMenu(fileName = "NewLootData", menuName = "Noko/Data/Loot Data")]
    public class LootData : ScriptableObject
    {
        public GameObject lootPrefab;
        public float minAmount;
        public float maxAmount;
    }
}