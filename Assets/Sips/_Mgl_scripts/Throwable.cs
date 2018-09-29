using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Steve")
        {
            if(gameObject.name == "GasCan" )
            {
                Debug.Log("HurtSteve");
                FindObjectOfType<TimerSlider>().LoseTime(1.0f);
                Destroy(gameObject);
            }
        }
    }

}
