using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = false;
    public bool canDoubleJump = true;
    public float dashSpeed = 30;
    public float startDashTime = 0.2f;
    public float startDashCooldownTime = 0.5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float xInput;
    private int lastDirection = 1;
    private float dashTime;
    private float dashCooldownTime;
    private bool isDashing = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dashTime = startDashTime;
        dashCooldownTime = 0f;
    }

    void Update() {
        if (isGrounded) {
            canDoubleJump = true;
        }

        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump")) {
            if (isGrounded) {
                Jump();
            }
            else if (canDoubleJump) {
                Jump();
                canDoubleJump = false;
            }
        }

        if (dashCooldownTime <= 0 && !isDashing && Input.GetKeyDown(KeyCode.LeftShift)) {
            isDashing = true;
        }

        if (isDashing) {
            Dash();
        }
        else if (dashCooldownTime > 0) {
            dashCooldownTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        if (!isDashing) {
            Walk(xInput);
        }
    }

    void Walk(float xInput) {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        if (xInput < 0) {
            lastDirection = -1;
            spriteRenderer.flipX = true;
        }
        else if (xInput > 0) {
            lastDirection = 1;
            spriteRenderer.flipX = false;
        }
    }

    void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Die() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Dash() {
        if (dashTime > 0) {
            dashTime -= Time.deltaTime;
            rb.velocity = new Vector2(lastDirection * dashSpeed, 0f);
        }
        else {
            dashTime = startDashTime;
            rb.velocity = new Vector2(0, 0);
            isDashing = false;
            dashCooldownTime = startDashCooldownTime;
        }
    }
}
