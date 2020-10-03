using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = false;

    private Rigidbody2D rb;
    private float xInput;
    private bool jumpKeyWasPressed = false;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded) {
            jumpKeyWasPressed = true;
        }
    }

    private void FixedUpdate() {
        Walk(xInput);

        if (jumpKeyWasPressed) {
            Jump();
        }
    }

    void Walk(float xInput) {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    void Jump() {
        Vector2 force = new Vector2(0f, jumpForce);
        rb.AddForce(force, ForceMode2D.Impulse);
        jumpKeyWasPressed = false;
    }
}
