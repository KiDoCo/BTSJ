using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualEnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject[] prefabs = new GameObject[3];
    [SerializeField] Vector2 timeBetweenSpawns;
    [SerializeField] float timer = 5f, offsetX = 10f;

    void Update()
    {
        transform.position = new Vector3(GameManager.Instance.Player.transform.position.x + offsetX, transform.position.y, transform.position.z);

        if(timer > 0)
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
