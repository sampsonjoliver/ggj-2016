using UnityEngine;
using System.Collections;
using System;

public class PlayerMortality : IMortal {
    private Vector3 spawnPosition;
    private ActorAudioHandler audioHandler;
    public AudioClip deathClip;
    private bool isDead;
    private float elapsedDeathTime;
    public float RespawnTime = 5f;

    // Use this for initialization
    void Start () {
	   spawnPosition = transform.position;
	   audioHandler = GetComponent<ActorAudioHandler>();
	}
	
	// Update is called once per frame
	void Update () {
	   if (isDead) {
           elapsedDeathTime += Time.deltaTime;
           
           if (elapsedDeathTime >= RespawnTime) {
               Debug.Log("Respawning player " + gameObject);
               Respawn();
           }
       }
	}

    private void Respawn() {
        SetControlsEnabled(true);
        GetComponent<PlayerMovement>();
        isDead = false;
        gameObject.transform.position = spawnPosition;
    }

    public override void OnDeath() {
        Debug.Log("On Player Death");
        isDead = true;
        elapsedDeathTime = 0f;
    }

    public override void OnImminentDeath() {
        Debug.Log("On Player Imminent Death");
        
        // Play death sound
        audioHandler.PlaySpeechClip(deathClip);
        SetControlsEnabled(false);
    }

    private void SetControlsEnabled(bool isEnabled) {
        GetComponent<PlayerPush>().enabled = isEnabled;
        GetComponentInChildren<Converter>().enabled = isEnabled;
        GetComponent<PlayerMovement>().SetRagdoll(!isEnabled);
    }
}
