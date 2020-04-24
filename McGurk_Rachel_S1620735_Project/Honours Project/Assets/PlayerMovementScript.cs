using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour

{
    // stores variables for player movement 
    public float speed;
    private Rigidbody2D rb;
    private bool canJump;
    public float jumpForce;

    // stores trigger objects
    public GameObject shakeTriggerObject;
    public GameObject zoomTriggerObject;

    // stores scripts
    AutoScrollCameraScript autoScrollCameraScript;
    CameraShakeScript cameraShakeScript;
    CameraZoomScript cameraZoomScript;
    LockedCameraScript lockedCameraScript;
    CameraSmoothingScript cameraSmoothingScript;

    DataManagerScript dataManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        // sets up scripts
        autoScrollCameraScript = FindObjectOfType<AutoScrollCameraScript>();
        cameraShakeScript = FindObjectOfType<CameraShakeScript>();
        cameraZoomScript = FindObjectOfType<CameraZoomScript>();
        lockedCameraScript = FindObjectOfType<LockedCameraScript>();
        cameraSmoothingScript = FindObjectOfType<CameraSmoothingScript>();

        dataManagerScript = FindObjectOfType<DataManagerScript>();

        //stores the player's Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // adds an up and jump force when space is pressed to make player jump
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //reloads the scene when enter is pressed
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void FixedUpdate()
    {
        //Stores the current horizontal input
        float moveHorizontal = Input.GetAxis("Horizontal");

        // creates a move vector and applies this to player rigidbody to move player horizontally
        Vector3 move = new Vector3(moveHorizontal * speed, rb.velocity.y, 0f);
        rb.velocity = move;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // only lets player jump when standing on a platform
        if(col.gameObject.tag == "Platform")
        {
            canJump = true;

        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        //stops player jumping again when jumping 
        if (col.gameObject.tag == "Platform")
        {
            canJump = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // turns the camera shake script on and disables active camera
        if (collision.gameObject.tag == "ShakeTrigger")
        {
            if (dataManagerScript.currentCamera == "AutoScrollCamera")
            {
                autoScrollCameraScript.enabled = false;
            }
            else if (dataManagerScript.currentCamera == "LockedCamera")
            {
                lockedCameraScript.enabled = false;
            }
            else if (dataManagerScript.currentCamera == "CameraSmoothing")
            {
                cameraSmoothingScript.enabled = false;
            }

            cameraShakeScript.enabled = true;
            cameraShakeScript.TriggerShake();
            shakeTriggerObject.SetActive(false);

        }

        // turns camera zoom script on and disables active camera
        if (collision.gameObject.tag == "Zoom Trigger")
        {
            if (dataManagerScript.currentCamera == "LockedCamera")
            {
                lockedCameraScript.enabled = false;
            }
            else if (dataManagerScript.currentCamera == "CameraSmoothing")
            {
                cameraSmoothingScript.enabled = false;
            }

            cameraZoomScript.enabled = true;
            cameraZoomScript.SetZoom(2);           

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // enables active camera when player leaves zoom trigger
        if (collision.gameObject.tag == "Zoom Trigger")
        {
            if (dataManagerScript.currentCamera == "LockedCamera")
            {
                lockedCameraScript.enabled = true;
            }
            else if (dataManagerScript.currentCamera == "CameraSmoothing")
            {
                cameraSmoothingScript.enabled = true;
            }

            cameraZoomScript.SetZoom(1);
            zoomTriggerObject.gameObject.SetActive(false);
        }
    }
}
