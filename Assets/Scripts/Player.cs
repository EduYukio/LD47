using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = false;
    public bool canDoubleJump = true;
    public bool canDash = true;
    public float dashSpeed = 30;
    public float startDashTime = 0.2f;
    public float startDashCooldownTime = 0.5f;
    public bool isStaggered = false;
    public float staggerDuration = 0.7f;
    public bool isOnTopOfMovingPlatform = false;
    public bool gotDoubleJumpItem = false;
    public bool gotDashItem = false;
    public float deathAnimDuration = 0.4f;
    private bool disableControls = false;
    public bool isLevel30 = false;
    public Teleporter teleporterScript;

    [HideInInspector] public bool isDashing = false;
    [HideInInspector] public int lastDirection = 1;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public MoveNearPlayer movingPlatform;
    [HideInInspector] public float xInput;
    [HideInInspector] public float dashTime;
    [HideInInspector] public float dashCooldownTime;
    [HideInInspector] public float originalGravityScale;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isLevel30) {
            teleporterScript = FindObjectOfType<Teleporter>();
        }

        dashTime = startDashTime;
        dashCooldownTime = 0f;

        if (GameObject.FindWithTag("DoubleJump")) {
            gotDoubleJumpItem = true;
        }

        if (GameObject.FindWithTag("Dash")) {
            gotDashItem = true;
        }
    }

    void Update() {
        if (disableControls) return;

        xInput = Input.GetAxisRaw("Horizontal");

        if (isGrounded) {
            canDoubleJump = true;
            canDash = true;
        }

        ProcessDashCooldown();
        if (isStaggered) {
            return;
        }
        ProcessDashRequest();
        ProcessDashAction();
        ProcessJumpAction();
    }

    private void FixedUpdate() {
        if (isStaggered || disableControls) {
            return;
        }
        ProcessWalkAction();
    }

    void Walk(float xInput) {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        if (isOnTopOfMovingPlatform) {
            rb.velocity += movingPlatform.rb.velocity;
        }

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
        if (isLevel30) {
            teleporterScript.shouldTeleport = true;
        }
        else {
            StartCoroutine(deathAnimation(deathAnimDuration));
        }
    }

    void Dash() {
        if (dashTime > 0) {
            dashTime -= Time.deltaTime;
            rb.velocity = new Vector2(lastDirection * dashSpeed, 0f);
        }
        else {
            StopDashing();
        }
    }

    void ProcessDashCooldown() {
        if (dashCooldownTime > 0) {
            dashCooldownTime -= Time.deltaTime;
        }
    }

    void ProcessDashRequest() {
        if (!gotDashItem) return;

        if (Input.GetButtonDown("Dash") && canDash && !isDashing && dashCooldownTime <= 0) {
            isDashing = true;
        }
    }

    void ProcessDashAction() {
        if (isDashing) {
            Dash();
        }
    }

    void ProcessJumpAction() {
        if (Input.GetButtonDown("Jump")) {
            if (isGrounded) {
                Jump();
            }
            else if (gotDoubleJumpItem && canDoubleJump) {
                Jump();
                canDoubleJump = false;
            }
        }
    }

    void ProcessWalkAction() {
        if (!isDashing) {
            Walk(xInput);
        }
    }

    public void StopDashing() {
        rb.velocity = Vector2.zero;
        isDashing = false;
        canDash = false;
        dashCooldownTime = startDashCooldownTime;
        dashTime = startDashTime;
    }

    public void Stagger() {
        isStaggered = true;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        StartCoroutine(staggerTimer(staggerDuration));
    }

    IEnumerator staggerTimer(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        isStaggered = false;
        rb.gravityScale = originalGravityScale;
    }

    IEnumerator deathAnimation(float waitTime) {
        rb.bodyType = RigidbodyType2D.Static;
        disableControls = true;
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        disableControls = false;
    }
}
