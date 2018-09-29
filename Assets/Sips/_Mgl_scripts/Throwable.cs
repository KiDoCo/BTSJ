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
            if(gameObject.tag == "GasCan" )
            {
                Debug.Log("HurtSteve");
                FindObjectOfType<TimerSlider>().LoseTime(1.0f);
                Destroy(gameObject);
            }

            if(gameObject.tag == "WaterBottle")
            {
                Debug.Log("HealStebvesvff ");
                FindObjectOfType<TimerSlider>().AddTime(1.5f);
                Destroy(gameObject);
            }
        }
    }


    private void Update()
    {
        if((GameManager.Instance.Player.transform.position -gameObject.transform.position).magnitude >= 150.0f)
        {
            Debug.Log("Destroying");
            Destroy(gameObject);
        }
    }
}
