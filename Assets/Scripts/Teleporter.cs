using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    public Player player;
    public bool shouldTeleport = false;

    void Start() {
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<Player>();
    }

    void Update() {
        if (shouldTeleport) {
            StartCoroutine(Teleport(0.1f));
            shouldTeleport = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        shouldTeleport = true;
    }

    IEnumerator Teleport(float waitTime) {
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.rb.position = new Vector3(-9.55000019f, 9.03999996f, 0);
        yield return new WaitForSeconds(waitTime);
        player.GetComponent<SpriteRenderer>().enabled = true;
    }
}
