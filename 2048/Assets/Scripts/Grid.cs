using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    private Tile[,] tileGrid;
    private float tileOffset;
    private int gridSize = 4;
    private int tileCount = 0;
    private bool swiped = false;
    private bool moved;


    private void Awake()
    {
        tileOffset = tilePrefab.transform.localScale.x * 2;
        tileGrid = new Tile[gridSize, gridSize];
        transform.position = (Vector2.left + Vector2.up) * tileOffset * 1.5f;
    }

    private void Start()
    {
        SpawnTile();
        InputManager.instance.onSwipe += MoveTiles;
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    SpawnTile();
        //}

    }

    private void SpawnTile()
    {
        if(tileCount == gridSize * gridSize)
        {
            // Reset Game
        }
        else
        {
            int tilePos = tileCount * gridSize;
            do
            {
                tilePos = Random.Range(0, tileGrid.Length);
            } while (tileGrid[tilePos / gridSize, tilePos % gridSize] != null);

            GameObject newTile = Instantiate(tilePrefab, transform);
            tileGrid[tilePos / gridSize, tilePos % gridSize] = newTile.GetComponent<Tile>();
            newTile.transform.position = transform.position + ( Vector3.down * (tilePos / gridSize)  + Vector3.right * (tilePos % gridSize)) * tileOffset;
            tileCount++;
        }
    }

   

    private void MoveTiles(Vector2 dir)
    {
        if(!swiped)
        {
            swiped = true;
            moved = false;
            if(dir == Vector2.up)
            {
                SwipeUp();
            }
            else if (dir == Vector2.down)
            {
                SwipeDown();
            }
            else if (dir == Vector2.right)
            {
                SwipeRight();
            }
            else if (dir == Vector2.left)
            {
                SwipeLeft();
            }
            swiped = false;
            if(moved)
                SpawnTile();
        }
        UpdateVisuals();
    }

    /// <summary>
    /// Update every row from the second to the last checking whats above and if the tile is the same add them
    /// </summary>
    private void SwipeUp()
    {
        for (int row = 1; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                if(tileGrid[row, col] != null)
                {
                    int preRow = row - 1;
                    for (; preRow >= 0 && tileGrid[preRow, col] == null; preRow--) ;
                    
                    if(preRow < 0)
                    {
                        tileGrid[0, col] = tileGrid[row, col];
                        tileGrid[row, col] = null;
                        moved = true;
                    }
                    else
                    {
                        if(tileGrid[preRow,col].CheckSame(tileGrid[row, col]))
                        {
                            tileGrid[preRow, col].UpgradeTile();
                            Destroy(tileGrid[row, col].gameObject);
                            tileGrid[row, col] = null;
                            tileCount--;
                            moved = true;
                        }
                        else
                        {
                            if(preRow + 1 != row)
                            {
                                tileGrid[preRow + 1, col] = tileGrid[row, col];
                                tileGrid[row, col] = null;
                                moved = true;
                            }
                        }
                    }
                }
            }
        }
    }

    private void SwipeDown()
    {
        for (int row = gridSize - 2; row >= 0; row--)
        {
            for (int col = 0; col < gridSize; col++)
            {
                if (tileGrid[row, col] != null)
                {
                    int nextRow = row + 1;
                    for (; nextRow < gridSize && tileGrid[nextRow, col] == null; nextRow++) ;

                    if (nextRow >= gridSize)
                    {
                        tileGrid[gridSize-1, col] = tileGrid[row, col];
                        tileGrid[row, col] = null;
                        moved = true;
                    }
                    else
                    {
                        if (tileGrid[nextRow, col].CheckSame(tileGrid[row, col]))
                        {
                            tileGrid[nextRow, col].UpgradeTile();
                            Destroy(tileGrid[row, col].gameObject);
                            tileGrid[row, col] = null;
                            tileCount--;
                            moved = true;
                        }
                        else
                        {
                            if (nextRow - 1 != row)
                            {
                                tileGrid[nextRow - 1, col] = tileGrid[row, col];
                                tileGrid[row, col] = null;
                                moved = true;
                            }
                        }
                    }
                }
            }
        }
    }

    private void SwipeRight()
    {
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = gridSize - 2; col >= 0; col--)
            {
                if (tileGrid[row, col] != null)
                {
                    int nextCol = col + 1;
                    for (; nextCol < gridSize && tileGrid[row, nextCol] == null; nextCol++) ;

                    if (nextCol >= gridSize)
                    {
                        tileGrid[row, gridSize -1] = tileGrid[row, col];
                        tileGrid[row, col] = null;
                        moved = true;
                    }
                    else
                    {
                        if (tileGrid[row, nextCol].CheckSame(tileGrid[row, col]))
                        {
                            tileGrid[row, nextCol].UpgradeTile();
                            Destroy(tileGrid[row, col].gameObject);
                            tileGrid[row, col] = null;
                            tileCount--;
                            moved = true;
                        }
                        else
                        {
                            if (nextCol - 1 != col)
                            {
                                tileGrid[row, nextCol - 1] = tileGrid[row, col];
                                tileGrid[row, col] = null;
                                moved = true;
                            }
                        }
                    }
                }
            }
        }
    }

    private void SwipeLeft()
    {
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 1; col < gridSize; col++)
            {
                if (tileGrid[row, col] != null)
                {
                    int prevCol = col - 1;
                    for (; prevCol >= 0 && tileGrid[row, prevCol] == null; prevCol--) ;

                    if (prevCol < 0)
                    {
                        tileGrid[row, 0] = tileGrid[row, col];
                        tileGrid[row, col] = null;
                        moved = true;
                    }
                    else
                    {
                        if (tileGrid[row, prevCol].CheckSame(tileGrid[row, col]))
                        {
                            tileGrid[row, prevCol].UpgradeTile();
                            Destroy(tileGrid[row, col].gameObject);
                            tileGrid[row, col] = null;
                            tileCount--;
                            moved = true;
                        }
                        else
                        {
                            if (prevCol + 1 != col)
                            {
                                tileGrid[row, prevCol + 1] = tileGrid[row, col];
                                tileGrid[row, col] = null;
                                moved = true;
                            }
                        }
                    }
                }
            }
        }
    }

    private void UpdateVisuals()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if(tileGrid[i,j] != null)
                    tileGrid[i,j].transform.position = transform.position + (Vector3.down * i + Vector3.right * j) * tileOffset;
            }
        }
    }
}
