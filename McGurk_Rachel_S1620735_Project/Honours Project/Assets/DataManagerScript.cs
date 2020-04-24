using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManagerScript : MonoBehaviour
{
    // stores scripts
    AutoScrollCameraScript autoScrollCameraScript;
    LockedCameraScript lockedCameraScript;
    CameraSmoothingScript cameraSmoothingScript;
    CameraShakeScript cameraShakeScript;
    CameraZoomScript cameraZoomScript;

    PlayerMovementScript playerMovementScript;

    // toggles for camera options
    public Toggle lockedCameraToggle;
    public Toggle autoScrollCameraToggle;
    public Toggle cameraSmoothingToggle;
    public Toggle cameraShakeToggle;
    public Toggle cameraZoomToggle;

    // input fields for camera settings
    public InputField stepInputField;
    public InputField speedInputField;
    public InputField lockedXOffsetInputField;
    public InputField lockedYOffsetInputField;
    public InputField smoothingXOffsetInputField;
    public InputField smoothingYOffsetInputField;
    public InputField shakeLengthInputField;
    public InputField shakeMagnitudeInputField;
    public InputField dampingSpeedInputField;
    public InputField zoomFactorInputField;
    public InputField zoomSpeedInputField;

    // buttons
    public Button playButton;
    public Button quitButton;

    public GameObject optionsObject;
    public GameObject optionsText;

    // stores current active camera
    public string currentCamera = "";

    // Start is called before the first frame update
    void Start()
    {
        // initialises scripts
        autoScrollCameraScript = FindObjectOfType<AutoScrollCameraScript>();
        lockedCameraScript = FindObjectOfType<LockedCameraScript>();
        cameraSmoothingScript = FindObjectOfType<CameraSmoothingScript>();
        cameraShakeScript = FindObjectOfType<CameraShakeScript>();
        cameraZoomScript = FindObjectOfType<CameraZoomScript>();

        playerMovementScript = FindObjectOfType<PlayerMovementScript>();

        // adds listeners to toggles
        autoScrollCameraToggle.onValueChanged.AddListener(delegate { SelectAutoScrollCamera(autoScrollCameraToggle); });
        lockedCameraToggle.onValueChanged.AddListener(delegate { SelectLockedCamera(lockedCameraToggle); });
        cameraSmoothingToggle.onValueChanged.AddListener(delegate { SelectCameraSmoothing(cameraSmoothingToggle); });
        cameraShakeToggle.onValueChanged.AddListener(delegate { SelectCameraShake(cameraShakeToggle); });
        cameraZoomToggle.onValueChanged.AddListener(delegate { SelectCameraZoom(cameraZoomToggle); });

        // adds listeners to input fields
        stepInputField.onEndEdit.AddListener(SetStepAmount);
        speedInputField.onEndEdit.AddListener(SetStepAmount);

        lockedXOffsetInputField.onEndEdit.AddListener(SetXOffset);
        lockedYOffsetInputField.onEndEdit.AddListener(SetYOffset);
        smoothingXOffsetInputField.onEndEdit.AddListener(SetXOffset);
        smoothingYOffsetInputField.onEndEdit.AddListener(SetYOffset);

        shakeLengthInputField.onEndEdit.AddListener(SetShakeLength);
        shakeMagnitudeInputField.onEndEdit.AddListener(SetShakeMagnitude);
        dampingSpeedInputField.onEndEdit.AddListener(SetDampingSpeed);

        zoomFactorInputField.onEndEdit.AddListener(SetZoomFactor);
        zoomSpeedInputField.onEndEdit.AddListener(SetZoomSpeed);

        playButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitApplication);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // selects auto-scroll camera and disables other cameras
    void SelectAutoScrollCamera(Toggle change)
    {
        if (change.isOn)
        {
            currentCamera = "AutoScrollCamera";
            lockedCameraToggle.gameObject.SetActive(false);
            cameraSmoothingToggle.gameObject.SetActive(false);
            stepInputField.gameObject.SetActive(true);
        }
        else
        {
            currentCamera = "";
            lockedCameraToggle.gameObject.SetActive(true);
            cameraSmoothingToggle.gameObject.SetActive(true);
            stepInputField.gameObject.SetActive(false);
        }
    }

    // selects locked camera and disables other cameras
    void SelectLockedCamera(Toggle change)
    {
        if (change.isOn)
        {
            currentCamera = "LockedCamera";
            autoScrollCameraToggle.gameObject.SetActive(false);
            cameraSmoothingToggle.gameObject.SetActive(false);
            lockedXOffsetInputField.gameObject.SetActive(true);
            lockedYOffsetInputField.gameObject.SetActive(true);
        }
        else
        {
            currentCamera = "";
            autoScrollCameraToggle.gameObject.SetActive(true);
            cameraSmoothingToggle.gameObject.SetActive(true);
            lockedXOffsetInputField.gameObject.SetActive(false);
            lockedYOffsetInputField.gameObject.SetActive(false);
        }
    }

    // selects camera smoothing and disables other cameras
    void SelectCameraSmoothing(Toggle change)
    {
        if (change.isOn)
        {
            currentCamera = "CameraSmoothing";
            autoScrollCameraToggle.gameObject.SetActive(false);
            lockedCameraToggle.gameObject.SetActive(false);
            speedInputField.gameObject.SetActive(true);
            smoothingXOffsetInputField.gameObject.SetActive(true);
            smoothingYOffsetInputField.gameObject.SetActive(true);
        }
        else
        {
            currentCamera = "";
            autoScrollCameraToggle.gameObject.SetActive(true);
            lockedCameraToggle.gameObject.SetActive(true);
            speedInputField.gameObject.SetActive(false);
            smoothingXOffsetInputField.gameObject.SetActive(false);
            smoothingYOffsetInputField.gameObject.SetActive(false);
        }
    }

    //selects camera shake
    void SelectCameraShake(Toggle change)
    {
        if (change.isOn)
        {
            playerMovementScript.shakeTriggerObject.gameObject.SetActive(true);
            shakeLengthInputField.gameObject.SetActive(true);
            shakeMagnitudeInputField.gameObject.SetActive(true);
            dampingSpeedInputField.gameObject.SetActive(true);
        }
        else
        {
            playerMovementScript.shakeTriggerObject.gameObject.SetActive(false);
            shakeLengthInputField.gameObject.SetActive(false);
            shakeMagnitudeInputField.gameObject.SetActive(false);
            dampingSpeedInputField.gameObject.SetActive(false);
        }
    }

    // selects camera zoom
    void SelectCameraZoom(Toggle change)
    {
        if (change.isOn)
        {
            playerMovementScript.zoomTriggerObject.gameObject.SetActive(true);
            zoomFactorInputField.gameObject.SetActive(true);
            zoomSpeedInputField.gameObject.SetActive(true);
        }
        else
        {
            playerMovementScript.zoomTriggerObject.gameObject.SetActive(false);
            zoomFactorInputField.gameObject.SetActive(false);
            zoomSpeedInputField.gameObject.SetActive(false);
        }
    }

    void SetXOffset(string inputString)
    {
        float offset = float.Parse(inputString);

        if (currentCamera == "LockedCamera")
        {
            lockedCameraScript.xOffset = offset;
        }
        else if (currentCamera == "CameraSmoothing")
        {
            cameraSmoothingScript.xOffset = offset;
        }
    }

    void SetYOffset(string inputString)
    {
        float offset = float.Parse(inputString);

        if (currentCamera == "LockedCamera")
        {
            lockedCameraScript.yOffset = offset;
        }
        else if (currentCamera == "CameraSmoothing")
        {
            cameraSmoothingScript.yOffset = offset;
        }
    }

    void SetStepAmount(string inputString)
    {
        float stepAmount = float.Parse(inputString);

        if (currentCamera == "AutoScrollCamera")
        {
            autoScrollCameraScript.step = stepAmount;
        }
        else if (currentCamera == "CameraSmoothing")
        {
            cameraSmoothingScript.smoothingSpeed = stepAmount;
        }       
    }

    void SetShakeLength(string inputtring)
    {
        float shakeLength = float.Parse(inputtring);

        cameraShakeScript.lengthOfShake = shakeLength;
    }

    void SetShakeMagnitude(string inputtring)
    {
        float shakeMagnitude = float.Parse(inputtring);

        cameraShakeScript.shakingMagnitude = shakeMagnitude;
    }

    void SetDampingSpeed(string inputtring)
    {
        float dampingSpeed = float.Parse(inputtring);

        cameraShakeScript.dampingSpeed = dampingSpeed;
    }

    void SetZoomFactor(string inputString)
    {
        float zoomFactor = float.Parse(inputString);

        cameraZoomScript.zoomFactor = zoomFactor;
    }

    void SetZoomSpeed(string inputString)
    {
        float zoomSpeed = float.Parse(inputString);

        cameraZoomScript.zoomSpeed = zoomSpeed;
    }

    public void StartGame()
    {
        playerMovementScript.enabled = true;

        if (currentCamera == "AutoScrollCamera")
        {
            autoScrollCameraScript.enabled = true;
        }
        else if (currentCamera == "LockedCamera")
        {
            lockedCameraScript.enabled = true;
        }
        else if (currentCamera == "CameraSmoothing")
        {
            cameraSmoothingScript.enabled = true;
        }

        optionsObject.gameObject.SetActive(false);
        optionsText.gameObject.SetActive(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
