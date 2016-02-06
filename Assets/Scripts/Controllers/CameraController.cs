using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public float DampingTime = 0.2f;
    public float EdgeBuffer = 4f;           
    public float MinCamDepth = 8f;
    public float MaxCamDepth = 60f;
       
    [HideInInspector] public List<Transform> cameraTargets; 

    private Camera cam;
    private float zoomSpeed;
    private Vector3 moveVelocity;
    private Vector3 desiredPos;
    
    private Rect targetRect;
    private Vector2 projectedRectDims;

    private void Awake() {
        cam = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate() {
        // find the world rectangle that includes all targets
        targetRect = FindTargetRect();
        // project to account for rotation of camera
        projectedRectDims = new Vector2(targetRect.width, targetRect.height);
        projectedRectDims.y *= Mathf.Sin(transform.localEulerAngles.x * Mathf.Deg2Rad);
        Move();
        
        //Zoom();
        AngleZoom();
    }

    private Rect FindTargetRect() {
        Rect rect = new Rect();
        rect.min = new Vector2(float.MaxValue, float.MaxValue);
        rect.max = new Vector2(float.MinValue, float.MinValue);
        for (int i = 0; i < cameraTargets.Count; i++) {
            if (cameraTargets[i].gameObject.activeSelf) {
                rect.xMin = Mathf.Min(cameraTargets[i].position.x, rect.xMin);
                rect.xMax = Mathf.Max(cameraTargets[i].position.x, rect.xMax);
                rect.yMin = Mathf.Min(cameraTargets[i].position.z, rect.yMin);
                rect.yMax = Mathf.Max(cameraTargets[i].position.z, rect.yMax);
            }
        }
        // apply clamping because reasons
        rect.xMin = Mathf.Clamp(rect.xMin, -50, 50);
        rect.xMax = Mathf.Clamp(rect.xMax, -50, 50);
        rect.yMin = Mathf.Clamp(rect.yMin, -50, 50);
        rect.yMax = Mathf.Clamp(rect.yMax, -50, 50);
        // apply buffer to expand rectangle by X percent
        float width = rect.width;
        float height = rect.height;
        float max = Mathf.Max(width, height);
        // float buffer = Mathf.Clamp(Mathf.Lerp(EdgeBuffer, 0.00f, max / 100f), 0.02f, EdgeBuffer); // lerp the buffer amount 
        // rect.xMin -= 0.5f * width * buffer;
        // rect.xMax += 0.5f * width * buffer;
        // rect.yMin -= 0.5f * height * buffer;
        // rect.yMax += 0.5f * height * buffer;
        
        return rect;
    }
    private void Move() {
        Vector3 pos = targetRect.center;
        pos.z = pos.y - 0.5f * (targetRect.height - projectedRectDims.y);
        pos.y = 0;
        transform.localPosition = Vector3.SmoothDamp(transform.position, pos, ref moveVelocity, DampingTime);
    }
    
    private void AngleZoom() {
        float depth = 0;
        for (int i = 0; i < cameraTargets.Count; i++) {
            if (cameraTargets[i].gameObject.activeSelf && cameraTargets[i].transform.position.y > 0) {
                float focusTargetDist = Vector3.Distance(cameraTargets[i].transform.position, transform.position);
                float targetAngle = (180 - transform.localEulerAngles.x) * Mathf.Deg2Rad;
                float d = (focusTargetDist * Mathf.Sin(targetAngle)) / Mathf.Sin(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
                depth = Mathf.Max(depth, d);
            }
        }
        // clamp depth to bounds
        depth = Mathf.Clamp(depth, MinCamDepth, MaxCamDepth);
        // modify z of local position to zoom along axis of rig
        Vector3 pos = cam.transform.localPosition;
        pos.z = Mathf.SmoothDamp(pos.z, -depth, ref zoomSpeed, DampingTime);
        cam.transform.localPosition = pos;
    }
}