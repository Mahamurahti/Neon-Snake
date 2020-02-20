using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{

    // Displaying the score
    private Text scoreText;

    private void Awake()
    {
        scoreText = transform.Find("scoreText").GetComponent<Text>();
    }

    private void Update()
    {
        if (GameHandler.isSnakeDead())
        {
            scoreText.enabled = false;
        }
        else
        {
            scoreText.text = GameHandler.GetScore().ToString();
        }
    }
}
