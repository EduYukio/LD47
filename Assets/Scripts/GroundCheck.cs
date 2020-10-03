using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    Player playerScript;

    void Start() {
        GameObject playerObject = transform.parent.gameObject;
        playerScript = playerObject.GetComponent<Player>();
    }

    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            playerScript.isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            playerScript.isGrounded = false;
        }
    }
}
