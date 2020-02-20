using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{
    //------------------------------------------------------VARIABLES------------------------------------------------------//
    private GameObject foodGameObject;
    private Vector2Int foodGridPos;
    private int width, height;
    private Snake snake;

    //------------------------------------------------------CONSTRUCTOR------------------------------------------------------//
    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    // SpawnFood is called here so that it would be called before everything else (in handler)
    public void Setup(Snake snake)
    {
        this.snake = snake;

        /*
        // Test purposes
        for (int i = 0; i < 100000; i++)
        {
            foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
            foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSprite;
        }
        */

        SpawnFood();
    }

    //------------------------------------------------------SPAWN FOOD------------------------------------------------------//
    // Spawn Food mechanism. Food can not spawn on the snake
    private void SpawnFood()
    {
        do
        {
            foodGridPos = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        }
        while (snake.GetFullSnakePos().IndexOf(foodGridPos) != -1);

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPos.x, foodGridPos.y);
    }

    //------------------------------------------------------TRY EAT FOOD------------------------------------------------------//
    // This methods checks if the snake moves over the food. If the condition is true
    // then the food is consumed and a new food is generated
    public bool TryEatFood(Vector2Int snakeGridPos)
    {
        if(snakeGridPos == foodGridPos)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            GameHandler.AddScore();
            Debug.Log("Food has been eaten");
            return true;
        }
        else
        {
            return false;
        }
    }

    // Check if the snake has gone out of map, if yes then teleport snake to otherside of map
    public Vector2Int ValidateGridPos(Vector2Int gridPos)
    {
        // If snake goes too far left, set snake to right
        if(gridPos.x < 0)
        {
            gridPos.x = width - 1;
        }
        // If snake goes too far right, set snake to left
        if (gridPos.x > width - 1)
        {
            gridPos.x = 0;
        }
        // If snake goes too far down, set snake to up
        if (gridPos.y < 0)
        {
            gridPos.y = height - 1;
        }
        // If snake goes too far up, set snake to down
        if (gridPos.y > height - 1)
        {
            gridPos.y = 0;
        }
        return gridPos;
    }
}
