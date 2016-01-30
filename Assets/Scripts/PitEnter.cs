using UnityEngine;
using System.Collections;

public class PitEnter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   
	}
    
    public void OnTriggerEnter(Collider other) {
        // ragdoll stuff that lands in pit
        if(other.gameObject.tag == Tags.PLAYER || other.gameObject.tag == Tags.PLEB)
        {
            // Disable character controller
            if(other.gameObject.GetComponent<CharacterController>() != null)
                other.gameObject.GetComponent<CharacterController>().enabled = false;
                
            // Set that sweet sweet ragdoll yo
            Rigidbody body = other.gameObject.GetComponent<Rigidbody>();
            body.isKinematic = false;
            body.constraints = RigidbodyConstraints.None;
            
            // Freeze pleb conversion falloff
            if(other.gameObject.GetComponent<PlebHealth>() != null) {
                other.gameObject.GetComponent<PlebHealth>().OnImminentDeath();
            }
        }
    }
}
