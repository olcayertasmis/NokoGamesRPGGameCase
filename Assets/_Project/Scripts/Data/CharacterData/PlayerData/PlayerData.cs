using UnityEngine;

namespace Noko.Data
{
    [CreateAssetMenu(fileName = "NewPlayerData", menuName = "Noko/Data/Player Data")]
    public class PlayerData : CharacterData
    {
        [Header("Combat Settings")]
        public float baseAttackDamage;
        public float baseAttackInterval;
    }
}