using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScrollCameraScript : MonoBehaviour
{
    // camera position
    private Vector3 cameraPos;

    // stores player object
    public GameObject player;

    // how quickly the camera should move
    public float step;

    // Start is called before the first frame update
    void Start()
    {
        //sets cameraPos to camera's postition
        cameraPos = transform.position;       
    }

    // Update is called once per frame
    void Update()
    {
        //updates the camera's x pos every frame by step amount
        cameraPos.x += step;
        transform.position = cameraPos;

        //finds the minimum and maximum screen boundaries
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        //limits the player's position to within the boundaries
        player.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1), Mathf.Clamp(player.transform.position.y, minScreenBounds.y + 1, maxScreenBounds.y - 1), player.transform.position.z);
               
    }
}
