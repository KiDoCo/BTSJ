using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sliderstf : MonoBehaviour
{

    [SerializeField] GameObject handle;
    [SerializeField] Sprite[] fireSprites = new Sprite[3];
    [SerializeField] float timer = 0.1f;

    Sprite currentSprite;

    void Start()
    {
        currentSprite = handle.GetComponent<Image>().sprite;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            ChangeSprite();
            timer = 0.1f;
        }
    }

    void ChangeSprite()
    {
        int wantedSprite = Random.Range(0, fireSprites.Length);
        currentSprite = fireSprites[wantedSprite];
        handle.GetComponent<Image>().sprite = currentSprite;
    }

}
