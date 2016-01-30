using UnityEngine;
using System.Collections;

public class Converter : MonoBehaviour {
    private SphereCollider sphereCollider;
    public float radius = 5f;
    public Color color = Color.red;
    
	// Use this for initialization
	void Start () {
	    sphereCollider = GetComponent<SphereCollider>(); 
        sphereCollider.radius = radius;
        
        PaintMesh(color);
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
    
    private void PaintMesh(Color color) {
        // Paint the player to the correct color
        Renderer[] renderers = gameObject.transform.parent.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = color;
        }
    }
}
