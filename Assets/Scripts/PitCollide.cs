using UnityEngine;
using System.Collections;

public class PitCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   
	}
    
    public void OnTriggerEnter(Collider other) {
        // ragdoll stuff that lands in pit
       other.gameObject.GetComponent<CharacterController>().enabled = false;
       Rigidbody body = other.gameObject.GetComponent<Rigidbody>();
       body.isKinematic = false;
       body.constraints = RigidbodyConstraints.None;
    }
}
