using UnityEngine;
using System.Collections;

public class AnimationFix : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //    Debug.Log("Fix");
    //    
    //    animator.GetComponent<CharacterJoint>().transform.rotation = Quaternion.AngleAxis(270f, Vector3.forward);
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	   animator.GetComponentInChildren<CharacterJoint>().transform.localRotation = Quaternion.AngleAxis(270f, Vector3.forward);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//   Debug.Log("Fix");
    //    
    //    animator.GetComponentInChildren<CharacterJoint>().transform.localRotation = Quaternion.AngleAxis(270f, Vector3.forward);
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
