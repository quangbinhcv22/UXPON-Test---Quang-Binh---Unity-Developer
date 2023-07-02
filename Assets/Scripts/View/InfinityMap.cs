using UnityEngine;

namespace View
{
    public class InfinityMap : MonoBehaviour
    {
        [SerializeField] private GameObject mapPrefab;
        [SerializeField] private float mapLength = 100f;

        public GameObject[] _maps = new GameObject[4];

        private void Awake()
        {
            var initIndexes = new[] { Vector2Int.zero, Vector2Int.up, Vector2Int.right, new Vector2Int(1, 1) };

            for (int i = 0; i < initIndexes.Length; i++)
            {
                _maps[i] = i == 0 ? mapPrefab : Instantiate(mapPrefab);
            }
        }

        private void Update()
        {
            var target = Gameplay.Gameplay.Player.transform;
            var visibleChunks = VisibleChunks(target.position);

            for (int i = 0; i < _maps.Length; i++)
            {
                _maps[i].transform.position = CenterOf(visibleChunks[i]);
            }
        }


        private Vector2Int[] VisibleChunks(Vector3 position)
        {
            var chunks = new Vector2Int[4];

            var currentIndex = IndexAt(position);
            var currentCenter = CenterOf(currentIndex);

            var xNeighborOffset = position.x - currentCenter.x >= 0 ? 1 : -1;
            var zNeighborOffset = position.z - currentCenter.z >= 0 ? 1 : -1;

            chunks[0] = currentIndex;
            chunks[1] = currentIndex + new Vector2Int(xNeighborOffset, 0);
            chunks[2] = currentIndex + new Vector2Int(0, zNeighborOffset);
            chunks[3] = currentIndex + new Vector2Int(xNeighborOffset, zNeighborOffset);

            return chunks;
        }

        private Vector2Int IndexAt(Vector3 position)
        {
            var x = IndexAxis(position.x);
            var z = IndexAxis(position.z);

            return new(x, z);

            int IndexAxis(float value)
            {
                var index = (Mathf.FloorToInt(Mathf.Abs(value)) - mapLength / 2) / mapLength;
                var sign = Mathf.Sign(value);

                return (int)(index * sign);
            }
        }

        private Vector3 CenterOf(Vector2Int chunkIndex)
        {
            var offset = mapPrefab.transform.position;
            return new Vector3(chunkIndex.x, 0f, chunkIndex.y) * mapLength + offset;
        }
    }
}