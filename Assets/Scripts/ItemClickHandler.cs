using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour {
    public KeyCode key;
    public Button button;

    void Update() {
        if (Input.GetKeyDown(key) || Input.GetButtonDown("Jump") || Input.GetButtonDown("Dash")) {
            button.onClick.Invoke();
        }
    }
}
