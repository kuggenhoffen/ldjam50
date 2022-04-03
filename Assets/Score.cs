using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    public TMP_Text scoreText;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
