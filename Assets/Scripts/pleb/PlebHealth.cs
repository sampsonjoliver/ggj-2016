using UnityEngine;
using System.Collections;

public class PlebHealth : MonoBehaviour {
    private ActorAudioHandler audioHandler;
    public AudioClip deathClip;
    
	// Use this for initialization
	void Start () {
	   audioHandler = GetComponent<ActorAudioHandler>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    // Pleb is imminently destined to die as they fall to the pit
    public void OnImminentDeath() {
        Debug.Log("Freezing pleb conversion");
        GetComponent<PlebConversion>().enabled = false;
        
        // todo play animation and sound for fixed duration
        audioHandler.PlaySpeechClip(deathClip);
    }
    
    public void OnDeath() {
        // Destroy gameobject
        Debug.Log("Destroying pleb");
        Destroy(gameObject);
    }
}
