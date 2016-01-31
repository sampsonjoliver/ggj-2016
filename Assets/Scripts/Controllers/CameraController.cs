using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public float DAMPING_TIME = 0.2f;
    public float EDGE_BUFFER = 4f;           
    public float MinCamDepth = 8f;
    public float MaxCamDepth = 60f;
       
    [HideInInspector] public List<Transform> cameraTargets; 

    private Camera cam;
    private float zoomSpeed;
    private Vector3 moveVelocity;
    private Vector3 desiredPos;

    private void Awake() {
        cam = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate() {
        Move();
        Zoom();
    }

    private void Move() {
        desiredPos = FindAveragePosition();
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref moveVelocity, DAMPING_TIME);
    }

    private Vector3 FindAveragePosition() {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        for (int i = 0; i < cameraTargets.Count; i++) {
            if (cameraTargets[i].gameObject.activeSelf) {
                averagePos += cameraTargets[i].position;
                numTargets++;
            }
        }

        if (numTargets > 0)
            averagePos /= numTargets;

        // Constrain the y position
        averagePos.y = transform.position.y;

        // Return the desired position
        return averagePos;
    }

    private void Zoom() {
        Vector2 requiredSize = FindRequiredSize();
        // calculate required camera depth
        float requiredCamDepth;
        if(requiredSize.x > requiredSize.y)
            requiredCamDepth = (requiredSize.x / cam.aspect) * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        else
            requiredCamDepth = requiredSize.y * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        Vector3 pos = cam.transform.localPosition;
        float cameraZ = Mathf.Max(Mathf.Abs(requiredCamDepth), Mathf.Abs(MinCamDepth));
        // buffer
        cameraZ *= 1 + Mathf.Lerp(EDGE_BUFFER, 0.02f, (cameraZ - MinCamDepth) / (MaxCamDepth - MinCamDepth));
        pos.z = Mathf.SmoothDamp(pos.z, -cameraZ, ref zoomSpeed, DAMPING_TIME);
        cam.transform.localPosition = pos;
        //cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, requiredSize, ref zoomSpeed, DAMPING_TIME);
    }

    private Vector2 FindRequiredSize() {
        Vector3 desiredLocalPos = desiredPos;

        Vector2 requiredSize = Vector2.zero;

        foreach (Transform cameraTarget in cameraTargets) {
            if (cameraTarget.gameObject.activeSelf) {
                Vector3 targetLocalPos = cameraTarget.position;
                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

                requiredSize.x = Mathf.Max (requiredSize.x, Mathf.Abs(desiredPosToTarget.x)); // size horizontal
                requiredSize.y = Mathf.Max (requiredSize.y, Mathf.Abs(desiredPosToTarget.z)); // size vertical
            }
        }
        
        requiredSize *= 2;
        return requiredSize;
    }


    // Immediately snap to the desired position and size
    public void SetStartPositionAndSize() {
        desiredPos = FindAveragePosition();

        transform.position = desiredPos;

        Vector3 pos = cam.transform.localPosition;
        Vector2 requiredSize = FindRequiredSize();
        float requiredCamDepth;
        if(requiredSize.x > requiredSize.y)
            requiredCamDepth = -requiredSize.x * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        else
            requiredCamDepth = -requiredSize.y * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        pos.z = -Mathf.Max(Mathf.Abs(requiredCamDepth), Mathf.Abs(MinCamDepth));
        
        cam.transform.localPosition = pos;
    }
}