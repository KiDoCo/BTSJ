using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerMovement Player;
    public BoxCollider2D SteveCollider;


    private void Awake()
    {
        Instance = this;
        Player = FindObjectOfType<PlayerMovement>();
    }

    // Use this for initialization
    void Start()
    {
        //SteveCollider = FindObjectOfType<SteveScript>().GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
