using UnityEngine;
using System.Collections;

public class PitBottom : MonoBehaviour {
    public GameController gameController;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void OnTriggerEnter(Collider other) {
        Debug.Log("Woo");
        if (other.gameObject.tag == Tags.PLEB) {
            Debug.Log("Woo2");
            if (other.GetComponent<PlebConversion>().conversionTarget != null)
                gameController.HandleScoreIncrement(other.GetComponent<PlebConversion>().conversionTarget.transform.parent.gameObject);
                
            other.GetComponentInParent<PlebHealth>().OnDeath();
            return;
        }
        
        // destroy gameobjects with a rigid body
        if (other.GetComponent<Rigidbody>() != null) {
            Destroy(other.gameObject);
        }
    }
}
