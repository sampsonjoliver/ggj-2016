using UnityEngine;
using System.Collections;

public class Pit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void OnTriggerEnter(Collider other) {
        // destroy gameobjects with a rigid body
        if (other.GetComponent<Rigidbody>() != null) {
            Destroy(other.gameObject);
        }
    }
}
