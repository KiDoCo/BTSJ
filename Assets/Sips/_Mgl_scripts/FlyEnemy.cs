using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour, IEnemy   
{

    [SerializeField] enum Type { sinWave, crappy, };
    [SerializeField] Type currentType;

    [SerializeField] float speed, crappyFlyHeight = 1.5f;
    float timer;

    [SerializeField] GameObject crap;

    void OnEnable()
    {
        currentType = (Random.Range(0, 2) == 0) ? Type.sinWave : Type.crappy;
        if (currentType == Type.crappy)
            transform.position = new Vector3(transform.position.x, crappyFlyHeight, transform.position.z);
    }

    void FixedUpdate()
    {
        if(currentType == Type.sinWave)
        {
            // sin logic

            Vector3 wantedPos = new Vector3(-speed, Mathf.Sin(speed), transform.position.z);
            transform.position += (Vector3.right * -speed * Time.deltaTime) + (Vector3.up * 4 * Mathf.Sin(Time.time) * Time.deltaTime);
        }
        else
        {
            // crappy logic

            transform.position += Vector3.right * -speed * Time.deltaTime;

            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = Random.Range(1, 5);
                TakeAShit();
            }
        }
    }

    void TakeAShit()
    {
        GameObject shit = Instantiate(crap, transform.position + -Vector3.up * GetComponent<CircleCollider2D>().radius, Quaternion.identity);
        Destroy(shit, 1f);
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }

}
