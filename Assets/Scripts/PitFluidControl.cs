using UnityEngine;
using System.Collections;

public class PitFluidControl : MonoBehaviour {

    public float MinFluidHeight = 0.1f;
    public float MaxFluidHeight = 1.0f;

    private float FluidHeight = 0f;
    public float MaxFluidRotate = 10;
    public Transform FluidTransform;
    private Rotate FluidRotate;
    
    private float CurrentFraction = 0;
    private float CurrentVelocity = 0;
    
	// Use this for initialization
	void Start () {
	   FluidRotate = GetComponent<Rotate>();
	}
	
	// Update is called once per frame
	void Update () {
       // adjust fluid height
	   Vector3 fluidPos = FluidTransform.localPosition;
       fluidPos.y = Mathf.SmoothDamp(fluidPos.y, Mathf.Lerp(MinFluidHeight, MaxFluidHeight, CurrentFraction), ref CurrentVelocity, 0.1f);
       FluidTransform.localPosition = fluidPos;
       // adjust slosh angle
       float rotation = Mathf.Lerp(0, MaxFluidRotate, CurrentFraction);
       FluidTransform.localRotation = Quaternion.AngleAxis(rotation, Vector3.left);
	}
    
    public void Set(float fraction) {
        CurrentFraction = Mathf.Clamp(fraction, 0, 1);
    }
}
