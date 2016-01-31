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
            // Enable the ragdoll glory and other death logic
            if(other.gameObject.GetComponent<IMortal>() != null) {
                other.gameObject.GetComponent<IMortal>().OnImminentDeath();
                other.gameObject.GetComponent<ActorMovement>().AddRagdollImpulse(Vector3.down * 20);
            }
        }
    }
}
