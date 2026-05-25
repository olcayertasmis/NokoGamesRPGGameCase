using UnityEngine;
using UnityEngine.Pool;

namespace Noko.Utilities
{
    public static class ObjectPoolExtensions
    {
        // For Component types (MonoBehaviour, etc.)
        public static IObjectPool<T> CreatePool<T>(T prefab, int defaultCapacity = 10, int maxSize = 30) where T : Component
        {
            return new ObjectPool<T>(
                () => Object.Instantiate(prefab),
                obj => obj.gameObject.SetActive(true),
                obj => obj.gameObject.SetActive(false),
                obj => Object.Destroy(obj.gameObject),
                true, defaultCapacity, maxSize
            );
        }

        // For GameObject types (direct prefabs)
        public static IObjectPool<GameObject> CreatePool(GameObject prefab, int defaultCapacity = 10, int maxSize = 30)
        {
            return new ObjectPool<GameObject>(
                () => Object.Instantiate(prefab),
                obj => obj.SetActive(true),
                obj => obj.SetActive(false),
                obj => Object.Destroy(obj),
                true, defaultCapacity, maxSize
            );
        }
    }
}