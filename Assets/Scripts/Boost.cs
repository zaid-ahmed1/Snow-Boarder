using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SurfaceEffector2D surfaceEffector = FindObjectOfType<SurfaceEffector2D>().GetComponent<SurfaceEffector2D>();
            surfaceEffector.speed += 10; // Set the new surface effector speed value
            Destroy(gameObject);
        }
    }
}
