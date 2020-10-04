using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour {
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject blackBackground;
    public Player player;
    public Vector3 positionToSpawnDialog;
    public bool dialogActive = false;
    public bool isLevel30 = false;
    public GameObject nextLevel;
    public GameObject endGameText;
    public bool playerKeepsWalking = false;

    public GameObject continueButton;
    void Start() {
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<Player>();
    }

    void Update() {
        if (textDisplay.text == sentences[index]) {
            continueButton.SetActive(true);
        }

        float xDist = Mathf.Abs(player.transform.position.x - positionToSpawnDialog.x);
        if (!dialogActive && xDist < 0.5f) {
            ActivateDialog();
        }

        if (playerKeepsWalking) {
            player.rb.velocity = new Vector3(5f, 0f, 0f);
        }
    }

    IEnumerator Type() {
        foreach (char letter in sentences[index].ToCharArray()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence() {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1) {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else {
            textDisplay.text = "";
            continueButton.SetActive(false);
            DeactivateDialog();
        }
    }

    public void ActivateDialog() {
        player.rb.velocity = Vector3.zero;
        player.disableControls = true;
        blackBackground.SetActive(true);
        StartCoroutine(Type());
        dialogActive = true;
    }

    public void DeactivateDialog() {
        player.disableControls = false;
        blackBackground.SetActive(false);
        if (isLevel30) {
            player.disableControls = true;
            nextLevel.SetActive(false);

            GameObject camera = GameObject.FindWithTag("MainCamera");
            camera.transform.SetParent(null);

            playerKeepsWalking = true;
            endGameText.SetActive(true);
        }
    }
}
