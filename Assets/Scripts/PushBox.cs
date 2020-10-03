using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour {
    Rigidbody2D rb;
    public float pushSpeed = 10f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (rb.velocity == Vector2.zero) {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Player") {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.isDashing) {
                rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = new Vector2(player.lastDirection * pushSpeed, rb.velocity.y);
                player.StopDashing();
                player.Stagger();
            }
        }
    }
}
