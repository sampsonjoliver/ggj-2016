using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    
    public float lockY = 1f;
    
    [HideInInspector] public string horizontalInputAxis = "Horizontal";
    [HideInInspector] public string verticalInputAxis = "Vertical";
    
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
            float hMove = Input.GetAxis(horizontalInputAxis);
            float vMove = Input.GetAxis(verticalInputAxis);
            
            move += Vector3.forward * vMove * moveSpeed * Time.deltaTime;
            move += Vector3.right * hMove * moveSpeed * Time.deltaTime;
            
            // apply movement to fix y position
            if(transform.position.y - lockY > 0.001f)
                move += Vector3.up * (lockY - transform.position.y) * 0.9f;
                
            // Move the character controller by the vector determined above
            charController.Move(move);
            
            // Make the character face the direction it's moving, but clamped in the y-axis
            move.y = 0;
            // if(move.sqrMagnitude > 0.01f) not sure if need this, don't think do?
            transform.LookAt(transform.position + move.normalized);
       }
	}
    
    public void setInputAxes(string horizontalInputAxis, string verticalInputAxis) {
        this.horizontalInputAxis = horizontalInputAxis;
        this.verticalInputAxis = verticalInputAxis;
    }
}
