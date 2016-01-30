using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public float DAMPING_TIME = 0.2f;
    public float EDGE_BUFFER = 4f;           
    public float MIN_FRUSTUM_SIZE = 8f;
       
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
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, requiredSize, ref zoomSpeed, DAMPING_TIME);
    }

    private float FindRequiredSize() {
        Vector3 desiredLocalPos = desiredPos;

        float size = 0f;

        foreach (Transform cameraTarget in cameraTargets) {
            if (cameraTarget.gameObject.activeSelf) {
                Vector3 targetLocalPos = cameraTarget.position;
                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

                size = Mathf.Max (size, Mathf.Abs(desiredPosToTarget.y));
                size = Mathf.Max (size, Mathf.Abs(desiredPosToTarget.x) / cam.aspect);
            }
        }
        
        size += EDGE_BUFFER;

        size = Mathf.Max(size, MIN_FRUSTUM_SIZE);

        return size;
    }


    // Immediately snap to the desired position and size
    public void SetStartPositionAndSize() {
        desiredPos = FindAveragePosition();

        transform.position = desiredPos;

        cam.orthographicSize = FindRequiredSize();
    }
}