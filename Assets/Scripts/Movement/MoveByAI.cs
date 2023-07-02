using Entities;
using UnityEngine;
using UnityEngine.AI;

namespace Movement
{
    public class MoveByAI : MonoBehaviour
    {
        private Character _owner;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _owner = GetComponent<Character>();
            _agent = GetComponent<NavMeshAgent>();

            _owner.onDie += OnDie;
        }

        private void Update()
        {
            if (!Gameplay.Gameplay.IsPlaying || _owner.HpCurrent <= 0) return;

            if (!_agent.isOnNavMesh)
            {
                _agent.enabled = false;
                _agent.enabled = true;
            }
            else
            {
                _agent.destination = Gameplay.Gameplay.Player.transform.position;
                _agent.speed = _owner.speed;

                _owner.animator.SetFloat(Character.HashSpeed, _agent.velocity.magnitude);
            }
        }

        private void OnDie()
        {
            _agent.enabled = false;
        }
    }
}