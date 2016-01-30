using UnityEngine;
using System.Collections;

public class Converter : MonoBehaviour {
    private SphereCollider sphereCollider;
    private Light pointLight;
    public float radius = 5f;
    public Color color = Color.red;
    
	// Use this for initialization
	void Start () {
	    sphereCollider = GetComponent<SphereCollider>();
        pointLight = GetComponent<Light>(); 
        sphereCollider.radius = radius;
        
        SetColor(color);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLEB) {
            PlebConversion pleb = other.GetComponent<PlebConversion>();
            pleb.OnEnterConversionSphere(gameObject);
        }
    }
    
    void OnTriggerExit(Collider other) {
        if (other.tag == Tags.PLEB) {
            PlebConversion pleb = other.GetComponent<PlebConversion>();
            pleb.OnExitConversionSphere(gameObject);
        }
    }
    
    private void SetColor(Color color) {
        // Paint the player mesh to the correct color
        MeshRenderer[] renderers = gameObject.GetComponentsInParent<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = color;
        }
        
        pointLight.color = color;
    }
}
