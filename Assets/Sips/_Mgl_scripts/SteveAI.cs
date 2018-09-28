using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveAI : MonoBehaviour
{
    public bool distance_debug = true;

    [SerializeField] float speed = 2f;
    [SerializeField] GameObject goal;
    bool steveAlive;
 
    void Start()
    {

    }

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

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
