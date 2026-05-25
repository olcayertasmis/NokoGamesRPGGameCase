using UnityEngine;

namespace Noko.Data
{
    [CreateAssetMenu(fileName = "NewUpgradeData", menuName = "Noko/Data/Upgrade Data")]
    public class UpgradeData : ScriptableObject
    {
        [Header("Identity")]
        public UpgradeType upgradeType;
        public string upgradeName;
        public Sprite icon;

        [Header("Stat Progression")]
        public float baseValue;
        public float incrementPerLevel;
        public int maxLevel;

        [Header("Economy")]
        public int baseCost;
        public float costMultiplierPerLevel;

        public float GetValueAtLevel(int level)
        {
            return baseValue + (incrementPerLevel * level);
        }

        public int GetCostAtLevel(int level)
        {
            return Mathf.RoundToInt(baseCost * Mathf.Pow(costMultiplierPerLevel, level));
        }
    }
}