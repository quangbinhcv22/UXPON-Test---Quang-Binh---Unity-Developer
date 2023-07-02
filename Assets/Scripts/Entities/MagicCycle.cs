using UnityEngine;

namespace Entities
{
    public class MagicCycle : MonoBehaviour
    {
        [SerializeField] private float speed = 50;

        private void Update()
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }
}