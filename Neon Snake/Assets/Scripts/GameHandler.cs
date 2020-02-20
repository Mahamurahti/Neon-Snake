using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;
    private static int score;
    private static bool isDead;

    [SerializeField] private Snake snake;

    private LevelGrid levelGrid;

    private void Awake()
    {
        isDead = false;
        instance = this;
        InitializeStatic();
    }

    void Start()
    {
        Debug.Log("Game Handler has started");

        // Creates a grid where food can be generated
        levelGrid = new LevelGrid(21, 21);

        // Creates the setups so that levelGrid and snake can reference each other
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);

    }

    // Static values will stay the same even if the scene is loaded again,
    // to reset the score we need to call this method in the Awake method
    private static void InitializeStatic()
    {
        score = 0;
    }

    public static int GetScore()
    {
        return score;
    }

    public static void AddScore()
    {
        score += 100;
    }

    public static void SnakeDied()
    {
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
}
