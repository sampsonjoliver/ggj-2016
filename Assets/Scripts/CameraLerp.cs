using UnityEngine;
using System.Collections;
using System;

public class CameraLerp : MonoBehaviour {
  
    public float moveSpeed = 10;
    
    private float elapsedTime = -1f;
    private float time = 1f;
    
    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startPosZoom;
    
    public Transform cameraRig;
    public Transform camera;
    
    public float TargetDepth = -18;
    public float TargetAngle = 40;
    public float TargetRotation = -1;
    public Vector3 TargetCenter;
    
    private float oldDepth;
    private float oldAngle;
    private float oldRotation;
    private Vector3 oldCenter;
    
    private Action onFinish;
	
	// Update is called once per frame
	void Update (){
       if(elapsedTime >= 0f && elapsedTime < time) {
           elapsedTime += Time.deltaTime;
           transform.localPosition = Vector3.Lerp(oldCenter, TargetCenter, elapsedTime / time);
           if(TargetRotation >= 0) // because fuck
                transform.localRotation = Quaternion.AngleAxis(Mathf.Lerp(oldRotation, TargetRotation, elapsedTime / time), Vector3.up);
           camera.localPosition = Vector3.Lerp(Vector3.forward * oldDepth, Vector3.forward * TargetDepth, elapsedTime / time);
           cameraRig.localPosition = Vector3.Lerp(cameraRig.localPosition, Vector3.zero, elapsedTime / time);
           cameraRig.localRotation = Quaternion.AngleAxis(Mathf.Lerp(oldAngle, TargetAngle, elapsedTime / time), Vector3.right);
           if(elapsedTime > time) {
               enabled = false;
               if(onFinish != null)
                onFinish();
           }
       }
	}
    
    public void StartLerp(float time, Action action) {
        this.time = time;
        this.elapsedTime = 0;
        oldDepth = camera.localPosition.z;
        oldAngle = cameraRig.localRotation.eulerAngles.x;
        oldCenter = transform.localPosition;
        oldRotation = transform.localRotation.eulerAngles.y;
        this.enabled = true;
        onFinish = action;
    }
}