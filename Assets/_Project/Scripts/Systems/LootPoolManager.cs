using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Noko.Utilities;

namespace Noko.Systems
{
    public class LootPoolManager : MonoBehaviour
    {
        private readonly Dictionary<GameObject, IObjectPool<GameObject>> _pools = new();

        public GameObject GetLoot(GameObject prefab)
        {
            if (!_pools.ContainsKey(prefab))
            {
                _pools[prefab] = ObjectPoolExtensions.CreatePool(prefab, 10, 50);
            }

            return _pools[prefab].Get();
        }

        public void ReturnLoot(GameObject prefab, GameObject item)
        {
            if (_pools.TryGetValue(prefab, out var pool)) pool.Release(item);
            else Destroy(item);
        }
    }
}