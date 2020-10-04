using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    GameObject playerObj;
    Player player;

    void Start() {
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<Player>();
    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            player.ActivateBool(transform.position);
        }
    }
}
