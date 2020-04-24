using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothingScript : MonoBehaviour
{
    // stores player object
    public GameObject followTarget;

    // stores smoothing speed
    public float smoothingSpeed = 0f;

    // stores x and y offset
    public float xOffset = 0f;
    public float yOffset = 0f;

    void LateUpdate()
    {
        // stores x and y position on player, adds offset
        float xTarget = followTarget.transform.position.x + xOffset;
        float yTarget = followTarget.transform.position.y + yOffset;

        // calculates smoothing effect
        float interpolation = smoothingSpeed * Time.deltaTime;

        //lerps the camera's position to the player's, adds a smoothing effect
        Vector3 position = this.transform.position;
        position.x = Mathf.Lerp(this.transform.position.x, xTarget, interpolation);
        position.y = Mathf.Lerp(this.transform.position.y, yTarget, interpolation);

        this.transform.position = position;
    }
}
