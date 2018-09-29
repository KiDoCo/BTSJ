using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector]public PlayerMovement Player;
    [HideInInspector]public SteveAI Steve;
    [HideInInspector]public SideEnemy sideEnemy;
    [HideInInspector] public UpEnemy upEnemy;

    private void Awake()
    {
        Instance = this;
        Player = FindObjectOfType<PlayerMovement>();
        Steve = FindObjectOfType<SteveAI>();
        sideEnemy = FindObjectOfType<SideEnemy>();
        upEnemy = FindObjectOfType<UpEnemy>();
    }

    void Start()
    {
    }

    void Update()
    {

    }
}
