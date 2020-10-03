using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = false;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float xInput;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded) {
            Jump();
        }
    }

    private void FixedUpdate() {
        Walk(xInput);
    }

    void Walk(float xInput) {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        if (xInput < 0) {
            spriteRenderer.flipX = true;
        }
        else if (xInput > 0) {
            spriteRenderer.flipX = false;
        }
    }

    void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Die() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
