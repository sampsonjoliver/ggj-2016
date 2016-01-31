using UnityEngine;
using System.Collections;

public class PlayerPush : MonoBehaviour {

    public Vector3 spawnPosition;
    
    public GameObject spawnPrefab;
    public string inputKey;
    
    public AudioSource pushAudioSource;
    public AudioClip pushClip;
    
    private float originalPushPitch;
    
	// Use this for initialization
	void Start () {
        originalPushPitch = pushAudioSource.pitch;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(inputKey)) {
            // spawn a push volume
            GameObject pushGo = (GameObject)Instantiate(spawnPrefab, transform.position + (transform.rotation * spawnPosition), transform.rotation);
            Push push = pushGo.GetComponentInChildren<Push>();
            
            // useless check, will always be there as long as correct prefab used
            if (push == null)  
                Debug.LogError("Spawned PushVolume does not have Push component!!!! :'(");
                
            push.player = gameObject.transform;
            
            PlayAudio();
        }
    }
    
    void PlayAudio() {
        if(pushAudioSource.isPlaying)
            pushAudioSource.Stop();
        pushAudioSource.pitch = originalPushPitch + Random.Range(-0.1f, 0.1f);
        pushAudioSource.clip = pushClip;
        pushAudioSource.Play();
    }
}
