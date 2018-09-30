using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualItemSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] prefabs = new GameObject[2];
    [SerializeField] Vector2 timeBetweenSpawns;
    [SerializeField] float timer = 3f;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = Random.Range(timeBetweenSpawns.x, timeBetweenSpawns.y);
            Spawn();
        }
    }

    void Spawn()
    {
        int num = Random.Range(0, prefabs.Length);
        GameObject spawn = prefabs[num];
        Instantiate(spawn, transform.position, Quaternion.identity);
    }
}
