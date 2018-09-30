using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{

    [HideInInspector] public bool held;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (held) return;
        if(other.gameObject.tag == "Enemy")
        {
            if(gameObject.tag == "GasCan")
            {
                Destroy(other.gameObject);
                GameObject o = Instantiate(GameManager.Instance.particlesystems[1], transform.position, Quaternion.identity);

                for (int i = 0; i < o.GetComponentsInChildren<ParticleSystem>().Length; i++)
                {
                    o.GetComponentsInChildren<ParticleSystem>()[i].Play();

                }
                Destroy(o, 3.0f);
                Destroy(gameObject);
            }
            else if(gameObject.tag == "Pickup")
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        if(other.gameObject.tag == "Steve")
        {
            if(gameObject.tag == "GasCan")
            {
                EventManager.SoundBroadcast(EVENT.PlaySFX, AudioManager.SFXSource, 1);
                FindObjectOfType<TimerSlider>().LoseTime(0.1f);
                GameObject o = Instantiate(GameManager.Instance.particlesystems[1], transform.position, Quaternion.identity);

                for (int i = 0; i < o.GetComponentsInChildren<ParticleSystem>().Length; i++)
                {
                    o.GetComponentsInChildren<ParticleSystem>()[i].Play();

                }
                Destroy(o, 3.0f);
                Destroy(gameObject);


            }

            if (gameObject.tag == "WaterBottle")
            {
                EventManager.SoundBroadcast(EVENT.PlaySFX, AudioManager.SFXSource, 3);
                FindObjectOfType<TimerSlider>().AddTime(0.15f);

                GameObject o = Instantiate(GameManager.Instance.particlesystems[0], transform.position, Quaternion.identity);

                for (int i = 0; i < o.GetComponentsInChildren<ParticleSystem>().Length; i++)
                {
                    o.GetComponentsInChildren<ParticleSystem>()[i].Play();

                }
                Destroy(o, 3.0f);
                Destroy(gameObject);
            }
        }

        if(gameObject.tag == "Crap")
        {

            if(other.gameObject.tag == "Player")
            {
                GameManager.Instance.Player.stunned = true;
                GameManager.Instance.Player.Recover();

                Destroy(gameObject);
            }

            if(other.gameObject.tag == "Steve")
            {
                GameManager.Instance.Steve.timer = 2.0f;
                GameManager.Instance.Steve.GetComponentInChildren<Animator>().SetTrigger("Stunned");

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
