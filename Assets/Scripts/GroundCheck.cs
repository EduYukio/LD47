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

    private void OnTriggerEnter2D(Collider2D other) {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Ground" || otherTag == "Box") {
            playerScript.isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Ground" || otherTag == "Box") {
            playerScript.isGrounded = false;
        }
    }
}
