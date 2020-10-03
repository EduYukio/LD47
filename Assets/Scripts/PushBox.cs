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

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.isDashing) {
                rb.velocity = new Vector2(player.lastDirection * pushSpeed, rb.velocity.y);
                player.StopDashing();
                player.Stagger();
            }
        }
    }
}
