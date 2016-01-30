using UnityEngine;
using System.Collections;

public class PlebMovement : MonoBehaviour {
    
    public float moveSpeed = 5f;
    private PlebConversion plebConversion;
    private CharacterController charController;
    
	// Use this for initialization
	void Start () {
	   plebConversion = GetComponent<PlebConversion>();
	   charController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (plebConversion.conversionTarget != null) {
            MoveToTarget(plebConversion.conversionTarget.gameObject.transform.position);
        }
	}
    
    private void MoveToTarget(Vector3 targetPos) {
        Vector3 moveDir = Vector3.Normalize(targetPos - this.transform.position);
        charController.Move(moveSpeed * moveDir * Time.deltaTime);
    } 
}
