using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    bool hasCrashed = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !hasCrashed)
        {
            hasCrashed = true;
            GetComponent<AudioSource>().Play();
            SurfaceEffector2D surfaceEffector = FindObjectOfType<SurfaceEffector2D>().GetComponent<SurfaceEffector2D>();
            surfaceEffector.speed += 7; // Set the new surface effector speed value
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
