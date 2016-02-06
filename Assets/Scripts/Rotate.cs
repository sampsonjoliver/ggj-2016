using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float rotateSpeed = 180;
    
    public string axis = "y";    
	
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
	}
}
