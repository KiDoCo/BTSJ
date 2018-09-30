using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMagic : MonoBehaviour
{

    Vector3 startPos;
    float parallaxWidth = 94f;

    void Start()
    {
        startPos = transform.parent.transform.position;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {

            transform.parent.transform.position += Vector3.right * parallaxWidth;
        }
    }

    public void Reset()
    {
        transform.parent.transform.position = startPos;
        Debug.Log("reset");
    }
}
