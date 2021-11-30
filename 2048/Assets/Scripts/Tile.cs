using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private int currentNumber = 0;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[currentNumber]; 
    }

    public bool CheckSame(Tile tile)
    {
        return tile.currentNumber == currentNumber;
    }

    public void UpgradeTile()
    {
        currentNumber++;
        spriteRenderer.sprite = sprites[currentNumber];
    }

}
