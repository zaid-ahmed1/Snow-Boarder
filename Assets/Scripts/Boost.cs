using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public GameObject objectToModify;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Ice cream");
            SurfaceEffector2D surfaceEffector = objectToModify.GetComponent<SurfaceEffector2D>();
            surfaceEffector.speed += 10; // Set the new surface effector speed value
        }
    }
}
