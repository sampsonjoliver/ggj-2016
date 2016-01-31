using UnityEngine;
using System.Collections;

public class PlebHealth : IMortal {
    private ActorAudioHandler audioHandler;
    public AudioClip[] deathClips;
    
	// Use this for initialization
	void Start () {
	   audioHandler = GetComponent<ActorAudioHandler>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    // Pleb is imminently destined to die as they fall to the pit
    public override void OnImminentDeath() {
        Debug.Log("On Pleb Imminent Death");
        GetComponent<PlebConversion>().enabled = false;
        
        GetComponent<PlebMovement>().SetRagdoll(true);
        
        // Play death sound
        audioHandler.PlaySpeechClip(GetDeathClip());
    }
    
    private AudioClip GetDeathClip() {
        return deathClips[Random.Range(0, deathClips.Length - 1)];  
    }
    
    public override void OnDeath() {
        // Destroy gameobject
        Debug.Log("Destroying pleb");
        Destroy(gameObject);
    }
}
