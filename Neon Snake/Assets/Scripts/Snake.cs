using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int moveDir;
    private Vector2Int gridPos;

    [Header("Movement Speed")]
    public float gridMoveTimerMax = 1f;
    private float gridMoveTimer;

    private LevelGrid levelGrid;

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    // Initializing variables
    private void Awake()
    {
        moveDir = new Vector2Int(1, 0);
        gridPos = new Vector2Int(10, 10);
        gridMoveTimer = gridMoveTimerMax;
    }

    private void Update()
    {
        MoveInput();
        MoveTimer();
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
    private void MoveTimer()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridPos += moveDir;
            gridMoveTimer = 0;

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
