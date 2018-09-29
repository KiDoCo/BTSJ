using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerMovement Player;
    public SteveAI Steve;


    private void Awake()
    {
        Instance = this;
        Player = FindObjectOfType<PlayerMovement>();
        Steve = FindObjectOfType<SteveAI>();
    }

    void Start()
    {
    }

    void Update()
    {

    }
}
