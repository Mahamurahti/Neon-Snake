using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Information when moving on the grid
    private Vector2Int moveDir;
    private Vector2Int gridPos;

    // Timer for the move action
    [Header("Movement Speed")]
    public float gridMoveTimerMax = 1f;
    private float gridMoveTimer;

    // Snake Body
    private int snakeBodySize;
    private List<Vector2Int> snakePosHistoryList;

    private LevelGrid levelGrid;

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    // Initializing variables
    private void Awake()
    {
        gridPos = new Vector2Int(10, 10);
        gridMoveTimer = gridMoveTimerMax;
        moveDir = new Vector2Int(1, 0);

        snakeBodySize = 1;
    }

    private void Update()
    {
        MoveInput();
        MoveHandler();
    }

    // Movement, we use coordinates to move
    private void MoveInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (moveDir.y != -1)
            {
                moveDir.x = 0;
                moveDir.y = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (moveDir.y != 1)
            {
                moveDir.x = 0;
                moveDir.y = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (moveDir.x != -1)
            {
                moveDir.x = 1;
                moveDir.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (moveDir.x != -1)
            {
                moveDir.x = -1;
                moveDir.y = 0;
            }
        }
    }

    // How much time must pass before the next movement action is
    // executed. Also tells the LevelGrid where the snake is.
    private void MoveHandler()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;

            snakePosHistoryList.Insert(0, gridPos);

            gridPos += moveDir;

            if (snakePosHistoryList.Count >= snakeBodySize + 1)
            {
                snakePosHistoryList.RemoveAt(snakePosHistoryList.Count - 1);
            }

            for (int i = 0; i < snakePosHistoryList.Count; i++)
            {
                Vector2Int snakeMovePos = snakePosHistoryList[i];
                GameObject snakeBodyGameObject = new GameObject();
                SpriteRenderer snakeSpriteRenderer = snakeBodyGameObject.AddComponent<SpriteRenderer>();
                snakeSpriteRenderer.sprite = GameAssets.instance.snakeBodySprite;
                snakeBodyGameObject.transform.position = new Vector3(snakeMovePos.x, snakeMovePos.y);
                Object.Destroy(snakeBodyGameObject, gridMoveTimerMax);
            }

            transform.position = new Vector3(gridPos.x, gridPos.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngle(moveDir) - 90);

            levelGrid.SnakeMoved(gridPos);
        }
    }

    // Calculating the angle where the snake sprite must face
    private float GetAngle(Vector2Int direction)
    {
        float result = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(result < 0)
        {
            result += 360;
        }
        return result;
    }

    // Getting the position for LevelGrid to know where the snake is
    public Vector2Int GetGridPosition()
    {
        return gridPos;
    }
}
