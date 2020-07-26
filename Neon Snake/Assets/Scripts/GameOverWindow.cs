using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverWindow : MonoBehaviour
{
    private static GameOverWindow instance;

    private Button resetButton;
    private Text finalScore;
    private Text currentHighscore;
    private Text gameOverText;

    private void Awake()
    {
        instance = this;
        resetButton = transform.Find("resetButton").GetComponent<Button>();
        finalScore = transform.Find("ScoreDisplay").GetComponent<Text>();
        currentHighscore = transform.Find("CurrentHighscore").GetComponent<Text>();
        gameOverText = transform.Find("GameOverText").GetComponent<Text>();
        Hide();
    }

    private void Update()
    {
        finalScore.text = Score.GetScore().ToString();
        currentHighscore.text = "Current highscore: " + Score.GetHighscore().ToString();

        int nowScore = Score.GetScore();
        int highscore = Score.GetHighscore();

        if (nowScore == highscore)
        {
            gameOverText.text = "New Highscore!";
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    public static void ShowStatic()
    {
        instance.Show();
    }

    // Just for restarting the scene
    public void RestartScene()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click);
        Loader.Load(Loader.Scene.ClassicScene);
    }

    public void BackToMainMenu()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
