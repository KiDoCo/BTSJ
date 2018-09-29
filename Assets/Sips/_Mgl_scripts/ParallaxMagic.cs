using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMagic : MonoBehaviour
{

    [SerializeField] GameObject[] parallaxes = new GameObject[3];
    float parallaxWidth = 94f;

    void Start()
    {
        
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {

            transform.parent.transform.position += Vector3.right * parallaxWidth;

            //for (int i = 0; i < parallaxes.Length; i++)
            //{
            //    parallaxes[i].transform.position += Vector3.right * parallaxWidth;
            //}
        }
    }
}
