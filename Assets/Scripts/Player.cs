using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = false;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float xInput = Input.GetAxisRaw("Horizontal");
        if (xInput != 0) {
            Walk(xInput);
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            Jump();
        }
    }

    void Walk(float xInput) {
        Vector3 direction = new Vector3(xInput, 0f, 0f);
        transform.position += direction * Time.deltaTime * moveSpeed;
    }

    void Jump() {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
}
