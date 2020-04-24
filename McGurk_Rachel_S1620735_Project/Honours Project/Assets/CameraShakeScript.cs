using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeScript : MonoBehaviour
{
    AutoScrollCameraScript autoScrollCameraScript;
    CameraShakeScript cameraShakeScript;
    LockedCameraScript lockedCameraScript;
    CameraSmoothingScript cameraSmoothingScript;

    DataManagerScript dataManagerScript;

    // Transform of player object
    private Transform transform;

    // duration of shake
    private float shakingDuration = 0f;

    //length of shake that user can chnage
    public float lengthOfShake = 0f;

    // magnitude of shake
    public float shakingMagnitude = 0f;

    // how quickly the shake will stop
    public float dampingSpeed = 0f;

    // The initial position of the camera
    Vector3 cameraInitialPosition;


    void Awake()
    {
        // sets up scripts
        autoScrollCameraScript = FindObjectOfType<AutoScrollCameraScript>();
        cameraShakeScript = FindObjectOfType<CameraShakeScript>();
        lockedCameraScript = FindObjectOfType<LockedCameraScript>();
        cameraSmoothingScript = FindObjectOfType<CameraSmoothingScript>();

        dataManagerScript = FindObjectOfType<DataManagerScript>();


        // stores camera's transform
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        // stores camera's intial position
        cameraInitialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // shakes the camera for the shake duration at random positions
        if (shakingDuration > 0)
        {
            transform.localPosition = cameraInitialPosition + Random.insideUnitSphere * shakingMagnitude;

            shakingDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            // turns the active camera back on
            shakingDuration = 0f;

            if (dataManagerScript.currentCamera == "AutoScrollCamera")
            {
                autoScrollCameraScript.enabled = true;
            }
            else if (dataManagerScript.currentCamera == "LockedCamera")
            {
                lockedCameraScript.enabled = true;
            }
            else if (dataManagerScript.currentCamera == "CameraSmoothing")
            {
                cameraSmoothingScript.enabled = true;
            }

            cameraShakeScript.enabled = false;
        }
    }

    public void TriggerShake()
    {
        // set the shake duration to lenth of speed, therefore triggering the shake
        shakingDuration = lengthOfShake;
    }
}
