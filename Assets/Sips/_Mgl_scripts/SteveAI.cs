using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveAI : MonoBehaviour
{
    public bool distance_debug = true;

    [SerializeField] float speed = 2f;
    [SerializeField] GameObject goal;
    bool steveAlive;

    [SerializeField] float timer = 1f;
    bool running;

    void Start()
    {

    }

    void Update()
    {

        if(!running)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if(timer <= 0)
        {
            running = true;
        }

        float distance = Vector3.Distance(transform.position, goal.transform.position);

        if (distance_debug)
        {
            Debug.Log(distance);
        }

        if (distance <= 1f && steveAlive)
        {
            Win();
        }
    }

    void Win()
    {
        Debug.Log("Steve saved!");
        Destroy(gameObject);
    }
}
