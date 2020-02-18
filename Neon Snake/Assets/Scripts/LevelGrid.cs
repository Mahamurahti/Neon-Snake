using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{
    private GameObject foodGameObject;
    private Vector2Int foodGridPos;
    private int width, height;
    private Snake snake;

    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void Setup(Snake snake)
    {
        this.snake = snake;
        SpawnFood();
    }

    private void SpawnFood()
    {
        do
        {
            foodGridPos = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        }
        while (snake.GetGridPosition() == foodGridPos);

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPos.x, foodGridPos.y);
    }

    public void SnakeMoved(Vector2Int snakeGridPos)
    {
        if(snakeGridPos == foodGridPos)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            Debug.Log("Food has been eaten");
        }
    }
}
