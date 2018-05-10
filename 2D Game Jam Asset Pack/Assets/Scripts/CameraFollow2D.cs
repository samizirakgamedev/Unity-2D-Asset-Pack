using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour {
    
    [Header("Follow Player Settings")]
    [Tooltip("Drag the player GameObject here so the camera will follow it")]
    public Transform player;

    [Tooltip("Use this Vector to offset the Camera from pointing directly at the centre of the player")]
    public Vector3 cameraOffset;

    [Tooltip("How sharply the camera will follow the player when he moves")]
    public float smoothSpeed = 10.0f;

    [Header("Camera Bounding Settings")]
    [Tooltip("If ticked the camera will not be able to follow the player past the Vector Bounds that are set below")]
    public bool isBound;

    [Tooltip("The minimum vector position that the camera will be able to follow the player until")]
    public Vector3 minCameraPosition;

    [Tooltip("The maximum vector position that the camera will be able to follow the player until")]
    public Vector3 maxCameraPosition;

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + cameraOffset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        if (isBound)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPosition.x),
                Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPosition.y),
                Mathf.Clamp(transform.position.z, minCameraPosition.z, maxCameraPosition.z));
        }
    }
}
