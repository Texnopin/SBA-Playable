using UnityEngine;

namespace _Project.Code.Core.Character
{
    
    public class TestAnimatorState : StateMachineBehaviour
    {
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Updating animator");
        }
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Test", true);
            Debug.Log("Entering animator");
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Exiting animator");
        }
    }
}