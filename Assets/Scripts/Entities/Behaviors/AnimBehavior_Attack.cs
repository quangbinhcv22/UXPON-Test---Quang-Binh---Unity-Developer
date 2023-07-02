using UnityEngine;

namespace Entities.Behaviors
{
    public class AnimBehavior_Attack : MonoBehaviour
    {
        private Character _owner;

        private void Awake()
        {
            _owner = GetComponentInParent<Character>();
        }

        public void DoAttack()
        {
            _owner.OnDoAttack();
        }
        
        public void EndAttack()
        {
            _owner.OnEndAttack();
        }
    }
}