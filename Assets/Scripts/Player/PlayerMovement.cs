using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    
    public float lockY = 1f;
    
    private CharacterController charController;
	// Use this for initialization
	void Start () {
	   charController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
       if (charController.enabled) {
           // check each direction and add movement in that direction if necessary
            Vector3 move = Vector3.zero;
            if(Input.GetKey(KeyCode.W))
            {
                move += Vector3.forward * moveSpeed * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.S))
            {
                move += Vector3.back * moveSpeed * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.A))
            {
                move += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.D))
            {
                move += Vector3.right * moveSpeed * Time.deltaTime;
            }
            // apply movement to fix y position
            if(transform.position.y - lockY > 0.001f)
                move += Vector3.up * (lockY - transform.position.y) * 0.9f;
            // move the character controller by the vector determined above
            charController.Move(move);
            // face the direction we are moving
            move.y = 0;
            if(move.sqrMagnitude > 0.01f)
                transform.LookAt(transform.position + move.normalized);
       }
	}
}
