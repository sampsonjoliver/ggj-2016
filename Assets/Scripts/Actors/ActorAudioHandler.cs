using UnityEngine;
using System.Collections;

public class ActorAudioHandler : MonoBehaviour {
    public AudioSource walkAudioSource;
    public AudioSource talkAudioSource;
    public AudioClip walkClip;
    public AudioClip[] speechClips;
    public AudioClip[] conversionClips;
    public float MinTalkWaitDuration = 10f; 
    public float MaxTalkWaitDuration = 60f; 
    public static float CommonPitchRange = 0.2f;

    private float originalWalkPitch, originalTalkPitch;
    private float elapsedDurationBetweenTalk;
    private float nextTalkWaitTime;
    
	// Use this for initialization
	void Start () {
	   originalTalkPitch = talkAudioSource.pitch;
	   originalWalkPitch = walkAudioSource.pitch;
       
       elapsedDurationBetweenTalk = 0f;
       nextTalkWaitTime = GetNextWaitTime();
	}
	
	// Update is called once per frame
	void Update () {
	   elapsedDurationBetweenTalk += Time.deltaTime;
       
       if (elapsedDurationBetweenTalk >= nextTalkWaitTime) {
           PlaySpeechClip(GetTalkAudioClip());
       }
	}
    
    public void PlayConversionClip() {
        PlaySpeechClip(GetConversionAudioClip());
    }
    
    public void PlayWalkClip() {
        if (walkAudioSource.isPlaying) {
            return;
        }
        
        walkAudioSource.clip = walkClip;
        walkAudioSource.pitch = RandomisePitch(originalWalkPitch);
        walkAudioSource.Play();
    }
    
    public void PlaySpeechClip(AudioClip clip) {
        if (talkAudioSource.isPlaying) {
            talkAudioSource.Stop();
        }
        
        talkAudioSource.clip = clip;
        talkAudioSource.pitch = RandomisePitch(originalTalkPitch);
        talkAudioSource.Play();
        
        elapsedDurationBetweenTalk = 0f;
        nextTalkWaitTime = GetNextWaitTime();
    }
    
    private float GetNextWaitTime() {
        return Random.Range(MinTalkWaitDuration, MaxTalkWaitDuration);
    }
    
    private AudioClip GetTalkAudioClip() {
        return speechClips[Random.Range(0, speechClips.Length - 1)];
    }
    
    private AudioClip GetConversionAudioClip() {
        return conversionClips[Random.Range(0, conversionClips.Length - 1)];
    }
    
    private float RandomisePitch(float originalPitch) {
        return Random.Range(originalPitch - CommonPitchRange, originalPitch + CommonPitchRange);
    }
}
