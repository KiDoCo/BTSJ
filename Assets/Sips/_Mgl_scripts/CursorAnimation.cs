using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorAnimation : MonoBehaviour
{

    [SerializeField] Texture2D[] cursorSprites;
    Texture2D currentSprite;
    [SerializeField] float time = 0.01f;
    float timer;

    [SerializeField] int index;

    CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        index = 0;
        currentSprite = cursorSprites[0];
        timer = time;
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = time;
            index--;
            if (index < 0)
            {
                index = cursorSprites.Length - 1;
            }
            currentSprite = cursorSprites[index];
            Cursor.SetCursor(currentSprite, Vector2.zero, cursorMode);
        }
    }
}
