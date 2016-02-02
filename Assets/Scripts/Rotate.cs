using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float rotateSpeed = 180;
    
    public string axis = "y";
    
    public float moveSpeed = 10;
    
    private float elapsedTime = -1f;
    private float time = 1f;
    
    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startPosZoom;
    
    public Transform cameraRig;
    public Transform camera;
	
	// Update is called once per frame
	void Update (){
	   float rotateDelta = rotateSpeed * Time.deltaTime;
       Vector3 v3axis;
       if(axis == "z")
            v3axis = Vector3.forward;
        else if(axis == "x")
            v3axis = Vector3.right;
        else
            v3axis = Vector3.up;
       transform.rotation *= Quaternion.AngleAxis(rotateDelta, v3axis);
       if(elapsedTime >= 0f && elapsedTime < time) {
           elapsedTime += Time.deltaTime;
           transform.position = Vector3.Lerp(startPos, Vector3.zero, elapsedTime / time);
           camera.localPosition = Vector3.Lerp(startPosZoom, Vector3.forward * -15, elapsedTime / time);
           cameraRig.localRotation = Quaternion.Lerp(startRot, Quaternion.AngleAxis(40, Vector3.right), elapsedTime / time);
       }
	}
    
    public void StartStuff() {
        startPos = transform.position;
        startPosZoom = camera.localPosition;
        startRot = cameraRig.localRotation;
        elapsedTime = 0f;
    }
}
