using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    public float moveSpeed = 5f;
    
    private CharacterController charController;
	// Use this for initialization
	void Start () {
	   charController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
	   if(Input.GetKey(KeyCode.W))
       {
           charController.Move(Vector3.forward * moveSpeed * Time.deltaTime);
       }
       if(Input.GetKey(KeyCode.S))
       {
           charController.Move(Vector3.back * moveSpeed * Time.deltaTime);
       }
       if(Input.GetKey(KeyCode.A))
       {
           charController.Move(Vector3.left * moveSpeed * Time.deltaTime);
       }
       if(Input.GetKey(KeyCode.D))
       {
           charController.Move(Vector3.right * moveSpeed * Time.deltaTime);
       }
	}
}
