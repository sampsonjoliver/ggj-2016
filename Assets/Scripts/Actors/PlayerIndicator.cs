using UnityEngine;
using System.Collections;

public class PlayerIndicator : MonoBehaviour {
    private Light light;
    private LightPulse lightPulse;
    
    public float activeIntensity = 7;
    private SkinnedMeshRenderer renderer;
    private AudioSource audioSource;
    private bool isActivated = false;
    
    private Color color = Color.white;
    
	// Use this for initialization
	void Awake() {
	   light = GetComponentInChildren<Light>();
       lightPulse = GetComponentInChildren<LightPulse>();
	   renderer = GetComponent<SkinnedMeshRenderer>();
       audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	public void Set(bool activated) {
        if(activated) {
            lightPulse.enabled = false;
            light.intensity = activeIntensity;
            light.color = color;
            renderer.material.color = color;
            if(audioSource.isPlaying)
                audioSource.Stop();
            audioSource.Play();
        }
        else {
            lightPulse.enabled = true;
            renderer.material.color = Color.gray;
        }
    }
    
    public void SetColor(Color color) {
        this.color = color;
        light.color = color;
    }
}
