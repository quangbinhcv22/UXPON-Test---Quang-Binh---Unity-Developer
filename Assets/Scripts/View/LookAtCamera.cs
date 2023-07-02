using UnityEngine;

namespace View
{
    [RequireComponent(typeof(Transform))]
    public class LookAtCamera : MonoBehaviour
    {
        private Transform _target;

        private void Awake()
        {
            _target = Camera.main.transform;

            var reverseScale = transform.localScale;
            reverseScale.x *= Mathf.Abs(reverseScale.x) * -1;
            transform.localScale = reverseScale;
        }

        private void Update()
        {
            transform.LookAt(_target);
        }
    }
}