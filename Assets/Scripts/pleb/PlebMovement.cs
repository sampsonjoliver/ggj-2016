using UnityEngine;
using System.Collections;

public class PlebMovement : MonoBehaviour {
    
    public float moveSpeed = 5f;
    
    public float stopRadius = 3f;
    
    private PlebConversion plebConversion;
    private CharacterController charController;
    private Rigidbody rigidBody;
    
    private bool isRagdoll = false;
    private Vector3 ragdollImpulse = Vector3.zero;
    
	// Use this for initialization
	void Start () {
	   plebConversion = GetComponent<PlebConversion>();
	   charController = GetComponent<CharacterController>();
       rigidBody = GetComponent<Rigidbody>();
	}
	
    void FixedUpdate() {
        if(ragdollImpulse.sqrMagnitude > 0) {
            SetRagdoll(true);
            rigidBody.AddForce(ragdollImpulse, ForceMode.Impulse);
            ragdollImpulse.Set(0, 0, 0);
        }
    }
	// Update is called once per frame
	void Update () {
        if (isRagdoll) {
            // check if we have come to rest, if so, un-ragdoll
            if (rigidBody.velocity.sqrMagnitude < 0.1f)
                SetRagdoll(false); 
        }
        else {
            if (plebConversion.conversionTarget != null) {
                MoveToTarget(plebConversion.conversionTarget.gameObject.transform.position);
            }
            else {
                // wander
            }
        }
	}
    
    private void MoveToTarget(Vector3 targetPos) {
        Vector3 diff = (targetPos - this.transform.position);
        Vector3 move = diff.normalized * moveSpeed * Time.deltaTime;
        if(diff.sqrMagnitude > stopRadius * stopRadius)
            charController.Move(move);
        if(move.sqrMagnitude > 0.0001f)
            transform.LookAt(transform.position + move.normalized);
    }
    
    public void SetRagdoll(bool flag) {
        charController.enabled = !flag;
        rigidBody.isKinematic = !flag;
        isRagdoll = flag;
    }
    
    public void AddRagdollImpulse(Vector3 impulse) {
        ragdollImpulse += impulse;
    }
}
