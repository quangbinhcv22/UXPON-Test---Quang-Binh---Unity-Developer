using Entities;
using UnityEngine;

namespace Movement
{
    public class MoveByKeyboard : MonoBehaviour
    {
        private Character _owner;

        private void Awake()
        {
            _owner = GetComponent<Character>();
        }

        private void Update()
        {
            if (!Gameplay.Gameplay.IsPlaying || _owner.HpCurrent <= 0) return;

            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            var direction = new Vector3(horizontal, 0, vertical).normalized;

            var velocity = direction * _owner.speed;
            _owner.Rb.velocity = velocity;

            if (_owner.IsAttacking && _owner.Target)
            {
                var targetTrans = _owner.Target.transform.position;
                var lookAtPoint = new Vector3(targetTrans.x, _owner.model.position.y, targetTrans.z);
                _owner.model.LookAt(lookAtPoint);
            }
            else
            {
                _owner.model.LookAt(_owner.model.position + velocity);
            }

            _owner.animator.SetFloat(Character.HashSpeed, velocity.magnitude);
        }
    }
}