using UnityEngine;
using System.Collections.Generic;

public class PlebConversion : MonoBehaviour {
    public static float MinConversion = 0;
    public static float MaxConversion = 100;
    public float ConversionFalloffTime = 4f;
    public float conversionFalloffElapsedTime;
    public float conversionPerc;
    public Color color = Color.gray;
    private Light pointLight;
    
    public List<GameObject> availableConversionTargets;
    public GameObject conversionTarget;
    private ActorAudioHandler audioHandler;
    
	// Use this for initialization
	void Start () {
	   conversionFalloffElapsedTime = 0f;
	   conversionPerc = 0f;
       pointLight = GetComponentInChildren<Light>();
       availableConversionTargets = new List<GameObject>();
       audioHandler = GetComponent<ActorAudioHandler>();
	}
	
	// Update is called once per frame
	void Update () {
	   UpdateTargets();
	}
    
    private void UpdateTargets() {
        if (conversionTarget != null) {
            // If we have a target but we are not in its influence sphere            
            if (!availableConversionTargets.Contains(conversionTarget)) {
                if (HasAvailableConversionTargets()) {
                    // Check if there is a secondary target we can switch to
                    ConvertToNextTarget();
                } else {
                    // We aren't in any influence sphere, so we need to do conversion falloff
                    conversionFalloffElapsedTime += Time.deltaTime;
                    float tFrac = conversionFalloffElapsedTime / ConversionFalloffTime;
                    conversionPerc = Mathf.Lerp(MaxConversion, MinConversion, tFrac);
                    //Debug.Log("Current conversion = " + conversionPerc);
                    
                    Color targetColor = conversionTarget.GetComponentInChildren<Converter>().color;
                    SetColor(Color.Lerp(targetColor, color, tFrac));
                    
                    if (conversionPerc <= 0f) {
                        //Debug.Log("Pleb conversion falling off.");
                        conversionTarget = null;
                    }
                }
            }
        }
    }
    
    public void OnEnterConversionSphere(GameObject target) {
        // Add the new available target to the list of available targets
        if (!availableConversionTargets.Contains(target))
            availableConversionTargets.Add(target);
       
        ConvertToNextTarget();
    }
    
    public void OnExitConversionSphere(GameObject target) {
        // Remove the target from the list of available targets
        availableConversionTargets.Remove(target);
        
        // Set the target to the next available target if one exists
        ConvertToNextTarget();
    }
    
    public bool HasAvailableConversionTargets() {
        return availableConversionTargets.Count > 0;
    }
    
    public GameObject GetNextAvailableConversionTarget() {
        if (HasAvailableConversionTargets())
            return availableConversionTargets[0];
        else
            return null;
    }
    
    private void ConvertToNextTarget() {
        if (HasAvailableConversionTargets()) {
            if (conversionTarget != GetNextAvailableConversionTarget()) {
                PlayConvertAudioClip();
            }
            
            //Debug.Log("Pleb acquiring new target " + GetNextAvailableConversionTarget());
            conversionTarget = GetNextAvailableConversionTarget();
            conversionFalloffElapsedTime = 0f;
            conversionPerc = MaxConversion;
            
            SetColor(conversionTarget.GetComponentInChildren<Converter>().color);
        }
    }
    
    private void SetColor(Color color) {
        // Paint the player to the correct color
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = color;
        }
        
        pointLight.color = color;
    }
    
    private void PlayConvertAudioClip() {
        audioHandler.PlayConversionClip();
    }
}
