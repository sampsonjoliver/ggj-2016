using UnityEngine;
using System.Collections;

public class ActorMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    protected CharacterController charController;
    protected Animator animator;
    protected bool isRagdoll = false;
    protected Vector3 ragdollImpulse = Vector3.zero;

    public virtual void SetRagdoll(bool isRagdoll) {
        charController.enabled = !isRagdoll;
        if(!isRagdoll)
            animator.SetTrigger("return");
        animator.enabled = !isRagdoll;
        // change all rigidbodies
        Rigidbody[] rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody body in rigidBodies) {
            body.isKinematic = !isRagdoll;
            body.constraints = isRagdoll ? RigidbodyConstraints.None : RigidbodyConstraints.FreezePositionY;
        }
        this.isRagdoll = isRagdoll;
    }
    
    public void AddRagdollImpulse(Vector3 impulse) {
        ragdollImpulse += impulse;
    }
}
