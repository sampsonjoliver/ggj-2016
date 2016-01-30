using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public float DAMPING_TIME = 0.2f;
    public float EDGE_BUFFER = 4f;           
    public float MinCamDepth = 8f;
       
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
        float requiredSize = FindRequiredSize();
        requiredSize *= 1 + EDGE_BUFFER;
        // calculate required camera depth
        float distance = requiredSize * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        //distance *= 1 + EDGE_BUFFER;
        distance = -Mathf.Max(Mathf.Abs(distance), Mathf.Abs(MinCamDepth));
        Vector3 pos = cam.transform.localPosition;
        pos.z = Mathf.SmoothDamp(pos.z, distance, ref zoomSpeed, DAMPING_TIME);
        cam.transform.localPosition = pos;
        //cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, requiredSize, ref zoomSpeed, DAMPING_TIME);
    }

    private float FindRequiredSize() {
        Vector3 desiredLocalPos = desiredPos;

        float size = 0f;

        foreach (Transform cameraTarget in cameraTargets) {
            if (cameraTarget.gameObject.activeSelf) {
                Vector3 targetLocalPos = cameraTarget.position;
                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

                size = Mathf.Max (size, Mathf.Abs(desiredPosToTarget.x)); // size horizontal
                size = Mathf.Max (size, Mathf.Abs(desiredPosToTarget.z)); // size vertical
            }
        }
        
        return size * 2;
    }


    // Immediately snap to the desired position and size
    public void SetStartPositionAndSize() {
        desiredPos = FindAveragePosition();

        transform.position = desiredPos;

        Vector3 pos = cam.transform.localPosition;
        pos.z = -FindRequiredSize() * cam.aspect * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        
        cam.transform.localPosition = pos;
    }
}