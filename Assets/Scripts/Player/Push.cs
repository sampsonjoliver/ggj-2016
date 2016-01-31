using UnityEngine;
using System.Collections;

public class Push : MonoBehaviour {

    public float speed = 5f;
    public float time = 0.5f;
    private float elapsedTime = 0f;
    
    public float endWidth = 4f;
    public float startWidth = 1f;
    private float width;
    
    public float startForce = 1f;
    public float endForce = 0.1f;
    private float force = 0f;
    
    public Transform player;
    
    public ParticleSystem waveParticles;
    public ParticleSystem dustParticles;
    
	// Use this for initialization
	void Start () {
	   elapsedTime = 0f;
       
       width = startWidth;
       force = startForce;
       
       Color playerColor = player.GetComponentInChildren<Converter>().color;
       waveParticles.startColor = Color.Lerp(playerColor, Color.white, 0.75f);
       dustParticles.startColor = Color.Lerp(playerColor, Color.white, 0.75f);
       GetComponentInChildren<Light>().color = playerColor;
	}
	
	// Update is called once per frame
	void Update () {
       // lerp stuff
       elapsedTime += Time.deltaTime;
       width = Mathf.Lerp(startWidth, endWidth, elapsedTime / time);
       force = Mathf.Lerp(startForce, endForce, elapsedTime / time);
       if(elapsedTime >= time) {
           if(waveParticles != null)
            waveParticles.Stop();
           if(dustParticles != null)
            dustParticles.Stop();
           Destroy(gameObject);
           Destroy(transform.parent.gameObject, time * 2f);
       }
	   // move block forward
       transform.Translate(Vector3.forward * speed * Time.deltaTime);
       // adjust scale
       transform.localScale = new Vector3(width, transform.localScale.y, transform.localScale.z);
       //transform.localScale.Set(width, transform.localScale.y, transform.localScale.z);
	}
    
    // When colliding with rigid body, push it!
    public void OnTriggerEnter(Collider other) {
        Rigidbody otherRb = other.GetComponent<Rigidbody>();
        if(otherRb != null) {
            // calculate direction
            Vector3 pushVec = (other.transform.position - player.position).normalized * force;
            if(other.gameObject.tag == Tags.PLEB)
                other.GetComponent<PlebMovement>().AddRagdollImpulse(pushVec);
            else
                otherRb.AddForce(pushVec, ForceMode.Impulse);
        }
    }
}
