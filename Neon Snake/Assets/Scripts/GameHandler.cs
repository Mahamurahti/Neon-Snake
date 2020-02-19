using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private Snake snake;

    private LevelGrid levelGrid;

    void Start()
    {
        Debug.Log("Game Handler has started");

        // Creates a grid where food can be generated
        levelGrid = new LevelGrid(21, 21);

        // Creates the setups so that levelGrid and snake can reference each other
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }
}
