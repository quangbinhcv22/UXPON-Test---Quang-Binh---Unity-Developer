using UnityEngine;

namespace Entities.Behaviors
{
    public class AnimBehavior_Die : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponentInParent<Character>().OnDieDone();
        }
    }
}
