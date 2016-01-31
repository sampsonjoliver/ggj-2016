using UnityEngine;
using System.Collections;

public class LightPulse : MonoBehaviour {

    private Light light;
    
    public float MinPulseTime = 0.2f;
    public float MaxPulseTime = 0.8f;
    
    public float MaxPulseDelta = 0.5f;
    
    private float originalIntensity;
    
    private float pulseTime;
    private float elapsedTime;
    private float oldIntensity;
    private float newIntensity;
	// Use this for initialization
	void Start () {
	   light = GetComponent<Light>();
       originalIntensity = light.intensity;
       SetNextPulse();
	}
	
	// Update is called once per frame
	void Update () {
	   elapsedTime += Time.deltaTime;
       light.intensity = Mathf.Lerp(oldIntensity, newIntensity, elapsedTime / pulseTime);
       if(elapsedTime > pulseTime)
            SetNextPulse();
	}
    
    void SetNextPulse() {
        pulseTime = Random.Range(MinPulseTime, MaxPulseTime);
        elapsedTime = 0;
        oldIntensity = light.intensity;
        newIntensity = originalIntensity + Random.Range(-MaxPulseDelta, MaxPulseDelta);
    }
}
