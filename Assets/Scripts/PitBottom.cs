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
        if (other.gameObject.tag == Tags.PLEB || other.gameObject.tag == Tags.PLAYER) {
            if (other.gameObject.tag == Tags.PLEB && other.GetComponent<PlebConversion>().conversionTarget != null)
                gameController.HandleScoreIncrement(other.GetComponent<PlebConversion>().conversionTarget.transform.parent.gameObject);
                
            other.GetComponent<IMortal>().OnDeath();
            return;
        }
    }
}
