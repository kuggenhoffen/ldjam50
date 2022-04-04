using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text endScoreText;
    public TMP_Text hintText;
    public GameObject playUi;
    public GameObject endUi;
    public PlayerController playerController;
    int score = 0;
    bool controlEnabled;

    // Start is called before the first frame update
    void Start()
    {
        playUi.SetActive(true);
        endUi.SetActive(false);
        playerController.SetActive(true);
        controlEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText("Score: " + score);
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void ShowEndScreen()
    {
        Debug.Log("Show game end screen");
        endScoreText.SetText("Your score was\n" + score);
        playerController.SetActive(false);
        playUi.SetActive(false);
        endUi.SetActive(true);
        controlEnabled = false;
    }

    public void OnContinueClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void ActivateHint(string hint)
    {
        hintText.enabled = true;
        hintText.SetText(hint);
    }

    public void DeactivateHint(string hint)
    {
        if (hintText.text == hint) {
            hintText.enabled = false;
        }
    }

    public bool IsControlEnabled()
    {
        return controlEnabled;
    }
}
