using UnityEngine;
using System.Collections;

public class PitBottom : MonoBehaviour {
    public GameController gameController;
    public AudioSource audioSource;
    public AudioClip[] sizzleClips;
    public AudioClip[] bubbleClips;
    private float originalPitch;
    
    public float MinFluidHeight = 0.1f;
    public float MaxFluidHeight = 1.0f;
    private float FluidHeight = 0f;
    public float MaxFluidRotate = 10;
    public Transform FluidTransform;
    
	// Use this for initialization
	void Start () {
	   originalPitch = audioSource.pitch;
	}
	
	// Update is called once per frame
	void Update () {
        // PlayBubbleClip();
	}
    
    private void PlayBubbleClip() {
	   if (!audioSource.isPlaying) {
           audioSource.clip = GetBubbleClip();
           audioSource.Play();
       }
    }
    
    private void PlaySizzleClip() {
	   if (audioSource.isPlaying) {
           audioSource.Stop();
       }
        audioSource.clip = GetSizzleClip();
        audioSource.Play();
    }

    private AudioClip GetSizzleClip() {
        return sizzleClips[Random.Range(0, sizzleClips.Length-1)];
    }

    private AudioClip GetBubbleClip() {
        return bubbleClips[Random.Range(0, bubbleClips.Length-1)];
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == Tags.PLEB || other.gameObject.tag == Tags.PLAYER) {
            if (other.gameObject.tag == Tags.PLEB && other.GetComponent<PlebConversion>().conversionTarget != null)
                gameController.HandleScoreIncrement(other.GetComponent<PlebConversion>().conversionTarget.transform.parent.gameObject);
                
            PlaySizzleClip();
            other.GetComponent<IMortal>().OnDeath();
            return;
        }
    }
}
