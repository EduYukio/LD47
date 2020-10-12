using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estaca : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Player") {
            Player player = other.gameObject.GetComponent<Player>();
            player.Die();
        }
    }
}
