using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private Vector2 firstMousePos;
    private Vector2 mousePos;
    public delegate void MoveDir(Vector2 dir);
    public MoveDir onSwipe;

    private void Awake()
    {
        if (instance != this && instance != null)
            Destroy(gameObject);

        instance = this;
    }

    private void Update()
    {
        //CheckTouch();
        CheckInput();
    }

    private void CheckTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstMousePos = Input.mousePosition;
        }

        mousePos = Input.mousePosition;
        if (firstMousePos != Vector2.zero && firstMousePos != mousePos)
        {
            Vector2 dif = firstMousePos - mousePos;
            Vector2 dir;
            if (Mathf.Abs(dif.x) > Mathf.Abs(dif.y))
            {
                if (mousePos.x > firstMousePos.x)
                {
                    onSwipe(Vector2.right);
                }
                else
                {
                    onSwipe(Vector2.left);
                }
            }
            else
            {
                if (mousePos.y > firstMousePos.y)
                {
                    onSwipe(Vector2.up);
                }
                else
                {
                    onSwipe(Vector2.down);
                }
            }
            firstMousePos = Vector2.zero;
        }
    }

    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            onSwipe(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            onSwipe(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            onSwipe(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            onSwipe(Vector2.right);
        }
    }
}

