using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour {
    public KeyCode key;
    public Button button;

    void Update() {
        if (Input.GetKeyDown(key)) {
            button.onClick.Invoke();
        }
    }
}
