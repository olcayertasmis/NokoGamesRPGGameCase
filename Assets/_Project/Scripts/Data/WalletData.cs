using System;
using UnityEngine;

namespace Noko.Data
{
    [CreateAssetMenu(fileName = "NewWalletData", menuName = "Noko/Data/Wallet Data")]
    public class WalletData : ScriptableObject
    {
        public int CurrentGold { get; private set; }

        public event Action<int> OnGoldChanged;

        private const string GoldSaveKey = "Player_Gold_Amount";

        public void Initialize(int startingGold = 0)
        {
            CurrentGold = PlayerPrefs.GetInt(GoldSaveKey, startingGold);
            OnGoldChanged?.Invoke(CurrentGold);
        }

        public void AddGold(int amount)
        {
            CurrentGold += amount;
            SaveData();
            OnGoldChanged?.Invoke(CurrentGold);
        }

        public bool SpendGold(int amount)
        {
            if (CurrentGold >= amount)
            {
                CurrentGold -= amount;
                SaveData();
                OnGoldChanged?.Invoke(CurrentGold);
                return true;
            }

            return false;
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt(GoldSaveKey, CurrentGold);
            PlayerPrefs.Save();
        }
    }
}