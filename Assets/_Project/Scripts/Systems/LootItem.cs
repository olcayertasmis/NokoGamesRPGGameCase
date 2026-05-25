using UnityEngine;
using Noko.Interfaces;

namespace Noko.Systems
{
    public class LootItem : MonoBehaviour, ILootable
    {
        [SerializeField] private GameObject lootObject;
            
        private float _value;
        private GameObject _prefabReference;

        private LootPoolManager _poolManager;

        private void Awake()
        {
            _poolManager = FindFirstObjectByType<LootPoolManager>();
        }

        public void Initialize(float value, GameObject prefab)
        {
            _value = value;
            _prefabReference = prefab;
        }

        public float Collect()
        {
            if (_poolManager && _prefabReference)
            {
                _poolManager.ReturnLoot(_prefabReference, lootObject);
            }
            else
            {
                Destroy(lootObject);
            }

            return _value;
        }
    }
}