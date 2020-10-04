using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {
    void Start() {

    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject dialogManager = GameObject.FindWithTag("DialogManager");
        if (dialogManager) {
            Dialog script = dialogManager.GetComponent<Dialog>();
            script.ResetBool();

        }
        GameObject playerObj = GameObject.FindWithTag("Player");
        Player player = playerObj.GetComponent<Player>();
        player.ResetBool();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
