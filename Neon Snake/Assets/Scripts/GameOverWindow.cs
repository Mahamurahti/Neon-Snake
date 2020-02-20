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

    private void Awake()
    {
        instance = this;
        resetButton = transform.Find("resetButton").GetComponent<Button>();
        finalScore = transform.Find("ScoreDisplay").GetComponent<Text>();
        Hide();
    }

    private void Update()
    {
        finalScore.text = GameHandler.GetScore().ToString();
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
        Loader.Load(Loader.Scene.ClassicScene);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
