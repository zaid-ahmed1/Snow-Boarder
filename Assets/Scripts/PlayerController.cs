using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    Rigidbody2D rb2d;
    public TextMeshProUGUI scoreText;
    private int frontFlipCount = 1;
    private int backFlipCount = -1;
    private int flipCount = 0;
    float flips = 0;
    float deltaRotation = 0;
    float currentRotation = 0;
    float windupRotation = 0;
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                rb2d.AddTorque(torqueAmount);
            }
            else if (Input.GetKey(KeyCode.RightArrow)) {
                rb2d.AddTorque(-torqueAmount);
            }
        }

    }

    void FixedUpdate() {
        scoreText.text = "Flips: " + flipsCounter().ToString();
    }

    public void DisableControls() {
        canMove = false;
    }

float flipsCounter() {
    deltaRotation = (currentRotation - transform.eulerAngles.z);
    currentRotation = transform.eulerAngles.z;

    if (deltaRotation >= 300) {
        deltaRotation -= 360;
    }
    if (deltaRotation <= -300) {
        deltaRotation += 360;                           
    }                           
                            
    windupRotation += deltaRotation;                            
    flips = (windupRotation / 360);                         
    int flipsRounded = Mathf.RoundToInt(flips);                         
    if (flipsRounded == frontFlipCount) {
        frontFlipCount++;
        backFlipCount++;
        flipCount++;
    }
    else if (flipsRounded == backFlipCount) {
        frontFlipCount--;
        backFlipCount--;
        flipCount++;
    }
    return (flipCount);
}                           
}                           
                            