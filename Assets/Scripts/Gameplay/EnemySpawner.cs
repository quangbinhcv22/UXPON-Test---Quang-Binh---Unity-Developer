using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Pattern;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private AssetReference[] prefabs;

        [SerializeField] private float spawnRate = 0.5f;
        [SerializeField] private int maxAtTime = 30;

        [SerializeField] private float minRange = 15;
        [SerializeField] private float maxRange = 25;
        [SerializeField] private float boundMap = 98;

        private readonly List<GameObject> _aliveEnemies = new();


        private void OnEnable()
        {
            Gameplay.OnStart += SpawnEnemy;
            Gameplay.OnEnd += DestroyAll;
        }

        private void OnDisable()
        {
            Gameplay.OnStart -= SpawnEnemy;
            Gameplay.OnEnd -= DestroyAll;
        }


        private async void SpawnEnemy()
        {
            _aliveEnemies.RemoveAll(e => !e.gameObject.activeInHierarchy);
            if (!Gameplay.IsPlaying || _aliveEnemies.Count >= maxAtTime) return;

            var randomPrefab = prefabs[Random.Range(0, prefabs.Length)];
            Pooler.Get(randomPrefab, enemy =>
            {
                enemy.transform.position = RandomOffset;
                _aliveEnemies.Add(enemy);
            });

            await UniTask.Delay(TimeSpan.FromSeconds(spawnRate));
            SpawnEnemy();
        }

        private Vector3 RandomOffset
        {
            get
            {
                var position = Random.insideUnitSphere * maxRange;
                position.y = 0;


                position.x = Mathf.Max(Mathf.Abs(position.x), minRange) * Mathf.Sign(position.x);
                position.z = Mathf.Max(Mathf.Abs(position.z), minRange) * Mathf.Sign(position.z);

                position += Gameplay.Player.transform.position;

                position.x = Mathf.Clamp(position.x, -boundMap, boundMap);
                position.z = Mathf.Clamp(position.z, -boundMap, boundMap);

                return position;
            }
        }


        private void DestroyAll()
        {
            UniTask.Delay(1500);
            _aliveEnemies.ForEach(e => e.gameObject.SetActive(false));
        }
    }
}