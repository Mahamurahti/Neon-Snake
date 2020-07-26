using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;
    private static bool isDead;

    //private Text gameOverText;

    [SerializeField] private Snake snake;

    private LevelGrid levelGrid;

    private void Awake()
    {
        isDead = false;
        instance = this;
        Score.InitializeStatic();
        Time.timeScale = 1f;
    }

    void Start()
    {
        Debug.Log("Game Handler has started");

        // Creates a grid where food can be generated
        levelGrid = new LevelGrid(21, 21);

        // Creates the setups so that levelGrid and snake can reference each other
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);

        Time.timeScale = 1f;

        //gameOverText = transform.Find("GameOverText").GetComponent<Text>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused())
            {
                GameHandler.ResumeGame();
            }
            else
            {
                GameHandler.PauseGame();
            }
        }
    }

    public static void SnakeDied()
    {
        Score.TrySetNewHighscore();
        isDead = true;
        GameOverWindow.ShowStatic();
    }

    public static bool isSnakeDead()
    {
        if (isDead)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }
 
    public static void PauseGame()
    {
        PauseWindow.ShowStatic();
        Time.timeScale = 0f;
    }

    public static void ResumeGame()
    {
        PauseWindow.HideStatic();
        Time.timeScale = 1f;
    }

    public void ResumeGameBtn()
    {
        PauseWindow.HideStatic();
        Time.timeScale = 1f;
    }
}
