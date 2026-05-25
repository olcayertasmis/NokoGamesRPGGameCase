using UnityEngine;
using Noko.Interfaces;
using Noko.Data;

namespace Noko.Player
{
    public class LootCollector : MonoBehaviour
    {
        [SerializeField] private WalletData playerWallet;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ILootable loot))
            {
                float lootValue = loot.Collect();
                playerWallet.AddGold(Mathf.RoundToInt(lootValue));
            }
        }
    }
}