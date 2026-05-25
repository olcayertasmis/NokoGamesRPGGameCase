using System.Collections;
using UnityEngine;
using Noko.Systems;

namespace Noko.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject spawnVfxPrefab;

        [Header("Cinematic Spawn")]
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private CanvasGroup blackScreenFader;
        [SerializeField] private float fadeDuration;

        private void Start() => StartCoroutine(SpawnSequence());

        private IEnumerator SpawnSequence()
        {
            Vector3 spawnPos = spawnPoint ? spawnPoint.position : transform.position;
            GameObject playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            EventBus.TriggerPlayerSpawned(playerInstance);

            if (blackScreenFader)
            {
                float t = 1f;
                while (t > 0f)
                {
                    t -= Time.deltaTime / fadeDuration;
                    blackScreenFader.alpha = t;
                    yield return null;
                }

                blackScreenFader.gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.2f);
            if (spawnVfxPrefab) Instantiate(spawnVfxPrefab, spawnPos, Quaternion.identity);
        }
    }
}