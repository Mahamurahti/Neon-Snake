using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    //------------------------------------------------------VARIABLES------------------------------------------------------//
    // Available Movement Directions
    private enum Dir
    {
        Left,
        Right,
        Up,
        Down
    }

    // Which states the snake can be in
    private enum State
    {
        Alive,
        Dead,
    }
    private State state;

    // Information when moving on the grid
    private Dir moveDir;
    private Vector2Int gridPos;

    // Timer for the move action
    [Header("Movement Speed")]
    public float gridMoveTimerMax = 1f;
    private float gridMoveTimer;

    // Snake Body
    private int snakeBodySize;
    private List<SnakeMovePos> snakePosHistoryList;
    private List<SnakeBodyPart> snakeBodyPartList;

    private LevelGrid levelGrid;

    [Header("Effects")]
    public ParticleSystem eatEffect;

    // SetUp for referencing
    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    //------------------------------------------------------AWAKE------------------------------------------------------//
    // Initializing variables
    private void Awake()
    {
        gridPos = new Vector2Int(10, 10);
        gridMoveTimer = gridMoveTimerMax;
        moveDir = Dir.Right;
        state = State.Alive;

        snakeBodySize = 0;
        snakePosHistoryList = new List<SnakeMovePos>();
        snakeBodyPartList = new List<SnakeBodyPart>();
    }
    //------------------------------------------------------UPDATE------------------------------------------------------//
    private void Update()
    {
        switch (state)
        {
            case State.Alive:
                MoveInput();
                MoveHandler();
                break;
            case State.Dead:
                break;
        }
    }

    //------------------------------------------------------MOVE INPUT------------------------------------------------------//
    // Movement, movement is more defined in the MoveHandler
    private void MoveInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (moveDir != Dir.Down)
            {
                moveDir = Dir.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (moveDir != Dir.Up)
            {
                moveDir = Dir.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (moveDir != Dir.Left)
            {
                moveDir = Dir.Right;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (moveDir != Dir.Right)
            {
                moveDir = Dir.Left;
            }
        }
    }

    //------------------------------------------------------MOVE HANDLER------------------------------------------------------//
    // How much time must pass before the next movement action is
    // executed. Also tells the LevelGrid where the snake is.
    private void MoveHandler()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;

            SoundManager.PlaySound(SoundManager.Sound.SnakeMove);

            // Insert the first Index of snakePosHistory to the previous position
            SnakeMovePos previousSnakeMovePos = null;
            if(snakePosHistoryList.Count > 0)
            {
                previousSnakeMovePos = snakePosHistoryList[0];
            }

            // Creating a new position from the snakeMovPos class and inserting 
            // the position into the snakePosHistory list
            SnakeMovePos snakeMovePos = new SnakeMovePos(previousSnakeMovePos, gridPos, moveDir);
            snakePosHistoryList.Insert(0, snakeMovePos);

            // Movement Logic
            Vector2Int gridMoveDirVec;
            switch (moveDir)
            {
                default:
                case Dir.Right: gridMoveDirVec = new Vector2Int(1, 0); break;
                case Dir.Left:  gridMoveDirVec = new Vector2Int(-1, 0); break;
                case Dir.Up:    gridMoveDirVec = new Vector2Int(0, 1); break;
                case Dir.Down:  gridMoveDirVec = new Vector2Int(0, -1); break;
            }

            gridPos += gridMoveDirVec;

            // Denying snake access to the outer worlds
            gridPos = levelGrid.ValidateGridPos(gridPos);

            // If the snake succeeds to eat, it grows and creates a new body part
            // The new body part is created from the SnakeBodyPart class
            bool snakeAteFood = levelGrid.TryEatFood(gridPos);
            if (snakeAteFood)
            {
                snakeBodySize++;
                CreateSnakeBody();
                SoundManager.PlaySound(SoundManager.Sound.SnakeEat);
                Instantiate(eatEffect, transform.position, Quaternion.identity);
            }

            // Removing the last snake body part from the list (not to bloat the list)
            if (snakePosHistoryList.Count >= snakeBodySize + 1)
            {
                snakePosHistoryList.RemoveAt(snakePosHistoryList.Count - 1);
            }

            // Updating the body parts to angle them correctly
            // (The current sprite doesnt need a new angle so it's commented out)
            UpdateSnakeBodyParts();

            // Checking if the head position is the same as a body part position
            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartGridPos = snakeBodyPart.GetGridPosInSnakeMove();
                if (gridPos == snakeBodyPartGridPos)
                {
                    Debug.Log("GAME OVER!!!");
                    state = State.Dead;
                    GameHandler.SnakeDied();
                    SoundManager.PlaySound(SoundManager.Sound.GameOver);
                }
            }

            // Moving the snake to new position and rotating the head
            transform.position = new Vector3(gridPos.x, gridPos.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngle(gridMoveDirVec) - 90); 
        }
    }

    //------------------------------------------------------GET ANGLE------------------------------------------------------//
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

    //------------------------------------------------------GET GRID POS------------------------------------------------------//
    // Getting the position for LevelGrid to know where the snake is
    public Vector2Int GetGridPosition()
    {
        return gridPos;
    }

    //------------------------------------------------------GET FULL SNAKE POS ------------------------------------------------------//
    // Getting the position of all snakes body parts
    public List<Vector2Int> GetFullSnakePos()
    {
        List<Vector2Int> gridPosList = new List<Vector2Int>() { gridPos };
        foreach(SnakeMovePos snakeMovePos in snakePosHistoryList)
        {
            gridPosList.Add(snakeMovePos.GetGridPos());
        }
        return gridPosList;
    }

    //------------------------------------------------------CREATE SNAKE BODY------------------------------------------------------//
    private void CreateSnakeBody()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
    }

    //------------------------------------------------------UPDATE SNAKE BODY------------------------------------------------------//

    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            // Updating the snake body happens in the snake body class, 
            // but this sprite doesnt need the update so its commented out.
            snakeBodyPartList[i].SetSnakeMovPos(snakePosHistoryList[i]);
        }
    }

    //------------------------------------------------------SNAKE BODY PART CLASS------------------------------------------------------//
    private class SnakeBodyPart
    {
        private Transform transform;
        private SnakeMovePos snakeMovePos;

        // Constructor creates a new object
        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        // This method rotates the body parts correctly, but since we are using sprites
        // that occupy the whole "grid" and are square, we don't need this method.
        // If the sprites change to something non-rectangular, uncomment the switch - case portion
        public void SetSnakeMovPos(SnakeMovePos snakeMovePos)
        {
            this.snakeMovePos = snakeMovePos;
            transform.position = new Vector3(snakeMovePos.GetGridPos().x, snakeMovePos.GetGridPos().y);

            /* ----- UNCOMMENT IF THE SPRITE YOU USE IS NON-RECTANGULAR -----
            float angle;
            switch (snakeMovePos.GetDir())
            {
                default:
                case Dir.Up:
                    switch (snakeMovePos.GetPrevDir())
                    {
                        default:
                            angle = 0; break;
                        case Dir.Left:
                            angle = 45; break;
                        case Dir.Right:
                            angle = -45; break;
                    }
                    break;
                case Dir.Down:
                    switch (snakeMovePos.GetPrevDir())
                    {
                        default:
                            angle = 180; break;
                        case Dir.Down:
                            angle = 180 + 45; break;
                        case Dir.Up:
                            angle = 180 - 45; break;
                    }
                    break;
                case Dir.Left:
                    switch (snakeMovePos.GetPrevDir())
                    {
                        default:
                            angle = -90; break;
                        case Dir.Down:
                            angle = -45; break;
                        case Dir.Up:
                            angle = 45; break;
                    }
                    break;
                case Dir.Right:
                    switch (snakeMovePos.GetPrevDir())
                    {
                        default:
                            angle = 90; break;
                        case Dir.Down:
                            angle = 45; break;
                        case Dir.Up:
                            angle = -45; break;
                    }
                    break;
            }
            transform.eulerAngles = new Vector3(0, 0, angle);
            */
        }

        public Vector2Int GetGridPosInSnakeMove()
        {
            return snakeMovePos.GetGridPos();
        }
    }

    //------------------------------------------------------SNAKE MOVE POS CLASS------------------------------------------------------//
    // This class is used to return the position of the snake in different formats
    private class SnakeMovePos
    {
        private SnakeMovePos previousSnakeMovePos;
        private Vector2Int gridPos;
        private Dir dir;

        public SnakeMovePos(SnakeMovePos previousSnakeMovePos, Vector2Int gridPos, Dir dir)
        {
            this.previousSnakeMovePos = previousSnakeMovePos;
            this.gridPos = gridPos;
            this.dir = dir;
        }

        // Used for SetSnakeMovePos position
        public Vector2Int GetGridPos()
        {
            return gridPos;
        }

        // Used for the switch - case in SetSnakeMovePos
        public Dir GetDir()
        {
            return dir;
        }

        // Used for calculating if the snake has turned
        // and if the current body part needs rotating
        // in the switch - case in SetSnakeMovePos
        public Dir GetPrevDir()
        {
            if(previousSnakeMovePos == null)
            {
                return Dir.Right;
            }
            else
            {
                return previousSnakeMovePos.dir;
            }
        }
    }

}