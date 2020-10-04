using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour {
    Rigidbody2D rb;
    public float pushSpeed = 10f;
    public bool isOnTopOfMovingPlatform = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (isOnTopOfMovingPlatform) {
            if (rb.bodyType == RigidbodyType2D.Dynamic) {
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
        else if (rb.velocity == Vector2.zero) {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Player") {
            if (isOnTopOfMovingPlatform) {
                transform.SetParent(null);
                isOnTopOfMovingPlatform = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            Player player = other.gameObject.GetComponent<Player>();
            if (player.isDashing) {
                rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = new Vector2(player.lastDirection * pushSpeed, rb.velocity.y);
                player.StopDashing();
                player.Stagger();
            }
        }
        else if (otherTag == "Breakable") {
            Destroy(other.gameObject);
        }
    }
}
