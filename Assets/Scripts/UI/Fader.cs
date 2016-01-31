using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Fader : MonoBehaviour {
    private Image fader;
    public float FadeTime = 2f;
    public float elapsedFadeTime = 0f;
    public bool isFadingIn;
    public bool isFadingOut;
    public Color fadedInColor;
    public Color fadedOutColor;

	// Use this for initialization
	void Start () {
	   elapsedFadeTime = 0f;
       fader = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	   if (isFadingIn) {
           elapsedFadeTime += Time.deltaTime;
           fader.color = Color.Lerp(fadedOutColor, fadedInColor, elapsedFadeTime / FadeTime);
           
           if (elapsedFadeTime > FadeTime)
                isFadingIn = false;
       } else if (isFadingOut) {
           elapsedFadeTime += Time.deltaTime;
           fader.color = Color.Lerp(fadedInColor, fadedOutColor, elapsedFadeTime / FadeTime);
           
           if (elapsedFadeTime > FadeTime)
                isFadingOut = false;
       }
	}

    internal void FadeIn() {
        isFadingIn = true;
        elapsedFadeTime = 0f;
    }

    internal void FadeOut() {
        isFadingOut = true;
        elapsedFadeTime = 0f;
    }
}
