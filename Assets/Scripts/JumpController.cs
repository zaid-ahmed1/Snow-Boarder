using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{


    Rigidbody2D rb2d;
    bool isGrounded = false;

    [SerializeField] public float jumpForce = 10f;
    public KeyCode jumpKey = KeyCode.Space;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(jumpKey) && isGrounded) {
            Jump();
        }
        
    }

    private void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground" && this.gameObject.tag == "Boarder Bottom") {
            isGrounded = true;
            Debug.Log("Grounded");
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Ground" && this.gameObject.tag == "Boarder Bottom") {
            isGrounded = false;
            Debug.Log("Not grounded");
        }
    }     
}
