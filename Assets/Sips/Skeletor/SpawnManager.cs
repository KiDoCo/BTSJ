using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public List<GameObject> prefabs;

    private List<Vector3> AirPos = new List<Vector3>(3);
    private List<Vector3> GroundPos = new List<Vector3>(3);
    private Vector3 airoffset = new Vector3(30.0f, 0.0f, 0);
    private Vector3 groundoffset = new Vector3(30.0f, 0.0f, 0);
    private float spawnTimer;

    private void Start()
    {
        for (int i = 0; i < AirPos.Capacity; i++)
        {
            AirPos.Add((airoffset * (i + 1)));
        }

        for (int i = 0; i < GroundPos.Capacity; i++)
        {
            GroundPos.Add((groundoffset * (i + 1)) + new Vector3(0,-0.2f,0));
        }
    }

    void Update()
    {
        if (spawnTimer >= 0)
        {
            spawnTimer -= Time.deltaTime;
            return;
        }

        int number = Random.Range(0, 4);

        int g = 0;
        int a = 0;
        for (int i = 0; i < number; i++)
        {
            int r = Random.Range(0, 6);
            GameObject temp = prefabs[r];

            if (temp.GetComponent<MonoBehaviour>() is IEnemy)
            {
                if (temp.GetComponent<IEnemy>().id == "GroundE")
                {
                    Vector3 w = GroundPos[g] + GameManager.Instance.Player.transform.position;
                    GameObject clone = Instantiate(temp, w, Quaternion.identity);
                    clone.name = "ENEMY";
                }
                else
                {
                    Vector3 w = new Vector3(AirPos[a].x, 0) + GameManager.Instance.Player.transform.position;

                    GameObject clone = Instantiate(temp, AirPos[0] + GameManager.Instance.Player.transform.position, Quaternion.identity);
                    clone.name = "ENEMY";
                }
            }
            else
            {
                Vector3 w = new Vector3(GroundPos[g].x,GroundPos[g].y) + GameManager.Instance.Player.transform.position;
                GameObject clone = Instantiate(temp, w, Quaternion.identity);
                clone.name = "PickUp";
            }

            a++;
            g++;
        }

        spawnTimer = 2.0f;
    }
}
