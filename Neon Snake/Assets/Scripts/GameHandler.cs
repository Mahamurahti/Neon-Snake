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

        /*
        GameObject snakeHeadGameObject = new GameObject();
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.instance.snakeHeadSprite;
        */

        levelGrid = new LevelGrid(19, 19);

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }

    void Update()
    {

    }
}
