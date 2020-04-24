using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedCameraScript : MonoBehaviour
{
    // stores player object
    public GameObject player;

    // stores offset
    private Vector3 offset;

    //stores x and y offset
    public float xOffset = 0f;
    public float yOffset = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // calculates the distance between player and camera and stores as offset 
        offset = transform.position - player.transform.position;

        // sets the camera's position to same as the player's position, plus offset
        transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z) + offset;
    }

    private void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance on every frame
        transform.position = new Vector3 (player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z) + offset;
    }
}
