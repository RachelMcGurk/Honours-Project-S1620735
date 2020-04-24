using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomScript : MonoBehaviour
{
    // stores zoom factor
    public float zoomFactor = 1.0f;

    // stores zoom speed
    public float zoomSpeed = 0.0f;

    // stores camera's original size
    private float originalSize = 0f;

    // stores camera
    private Camera thisCamera;

    // stores camera's position
    private Vector3 cameraPos;


    // Use this for initialization
    void Start()
    {
        // initialises variables
        thisCamera = GetComponent<Camera>();
        originalSize = thisCamera.orthographicSize;
        cameraPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // creates a target size by multiplying camera's orginial size by zoom factor
        float targetSize = originalSize * zoomFactor;

        // lerps the camera to the target size using the zoom factor
        if (targetSize != thisCamera.orthographicSize)
        {
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
        }
        // lerps the camera to the original size using the zoom factor
        else
        {
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, originalSize, Time.deltaTime * zoomSpeed);
        }
    }

    public void SetZoom(float zoomFactor)
    {
        // sets zoom factor, therefore triggering zoom
        this.zoomFactor = zoomFactor;
    }
}
