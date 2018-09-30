using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public List<GameObject> prefabs;

    private List<Transform> AirPos = new List<Transform>(3);
    private List<Transform> GroundPos = new List<Transform>(3);
    [SerializeField] private float valueOffset;
    [SerializeField] private float distance;
    bool canspawn;
    private List<GameObject> entities = new List<GameObject>();
    int a = 0;
    int g = 0;


    private Vector3 airoffset = new Vector3(30.0f, 0.0f, 0);
    private Vector3 groundoffset = new Vector3(30.0f, 0.0f, 0);
    private float spawnTimer;

    private void Start()
    {
        EventManager.ActionAddHandler(EVENT.endGame, Endgame);
        canspawn = true;
        for (int i = 0; i < AirPos.Capacity; i++)
        {
            Transform a = GetComponent<Transform>();
            a.position = airoffset * (i + 1);
            AirPos.Add(a);
        }

        for (int i = 0; i < GroundPos.Capacity; i++)
        {
            Transform a = GetComponent<Transform>();
            a.position = (groundoffset * (i + 1)) + new Vector3(0, -0.2f, 0);
            GroundPos.Add(a);
        }
    }

    void Update()
    {
        if (!canspawn) return;

        airoffset = new Vector3(valueOffset, 0.0f, 0);
        groundoffset = new Vector3(valueOffset, 0.0f, 0);
        if (spawnTimer >= 0)
        {
            spawnTimer -= Time.deltaTime;
            return;
        }

        int number = Random.Range(0, 4);
        for (int i = 0; i < number; i++)
        {
            int r = Random.Range(0, prefabs.Count);
            GameObject temp = prefabs[r];


            if (temp.GetComponent<MonoBehaviour>() is IEnemy)
            {
                if (temp.GetComponent<IEnemy>().id == "GroundE")
                {

                    Vector3 w = GroundPos[g].position + GameManager.Instance.Player.transform.position;
                    GameObject clone = Instantiate(temp, w, Quaternion.identity);
                    entities.Add(clone);
                    clone.name = "ENEMY";
                }
                else
                {


                    Vector3 w = new Vector3(AirPos[a].position.x, 0) + GameManager.Instance.Player.transform.position;

                    GameObject clone = Instantiate(temp, AirPos[0].position + GameManager.Instance.Player.transform.position, Quaternion.identity);
                    entities.Add(clone);
                    clone.name = "ENEMY";
                }
            }
            else
            {
                Vector3 w = new Vector3(GroundPos[g].position.x, GroundPos[g].position.y) + GameManager.Instance.Player.transform.position;
                GameObject clone = Instantiate(temp, w, Quaternion.identity);
                entities.Add(clone);
                clone.name = "PickUp";

            }
            a++;
            g++;

            if (g >= 3)
            {
                g = 0;
                a = 0;
            }
        }

        spawnTimer = 3.5f;
    }

    void Endgame()
    {
        canspawn = false;
        for (int i = 0; i < entities.Count; i++)
        {
            Destroy(entities[i]);
        }

        entities.Clear();
    }


}
