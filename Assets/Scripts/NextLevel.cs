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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
