using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {
    private Vector3 playerOriginalPosition = new Vector3(-9.5f, 7, 0);

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject dialogManager = GameObject.FindWithTag("DialogManager");
        if (dialogManager) {
            Dialog.alreadyShowedOnThisLevel = false;
        }
        Player.respawnPosition = playerOriginalPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
