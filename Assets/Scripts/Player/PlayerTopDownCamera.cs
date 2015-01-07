using UnityEngine;
using System.Collections;

enum CameraState
{
    FOLLOW,
    FIX,
    NONE
}

public class PlayerTopDownCamera : PlayerBase
{
    public Vector3 offset = Vector3.zero;

    CameraState state;

    Transform cameraTransform;
    Transform playerTransform;

    // Override Awake() incase PlayerBase's Awake() called multiple times
    void Awake()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        cameraTransform = Camera.main.transform;
        if (cameraTransform == null) {
            Debug.LogError("Fail to find mainCamera");
            return;
        }

        playerTransform = this.gameObject.transform;

        state = CameraState.FOLLOW;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (state == CameraState.FOLLOW) {
            cameraTransform.position = playerTransform.position + offset;
            cameraTransform.LookAt(playerTransform.position);
        }
    }
}
