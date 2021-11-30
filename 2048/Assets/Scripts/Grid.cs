using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    private Tile[] tileGrid;
    private float tileOffset;


    private void Start()
    {
        tileOffset = tilePrefab.transform.localScale.x * 2;
        tileGrid = new Tile[16];
        transform.position = (Vector2.left + Vector2.up) * tileOffset * 1.5f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnTile();
        }
    }

    private void SpawnTile()
    {
        int tilePos;
        do
        {
            tilePos = Random.Range(0, tileGrid.Length);
        } while (tileGrid[tilePos] != null);

        GameObject newTile = Instantiate(tilePrefab, transform);
        print(tileGrid[tilePos]);
        tileGrid[tilePos] = newTile.GetComponent<Tile>();
        print(tileGrid[tilePos]);
        newTile.transform.position = transform.position + ( Vector3.down * (tilePos / 4)  + Vector3.right * (tilePos % 4)) * tileOffset;
    }
}
