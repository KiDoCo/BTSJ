using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public PlayerMovement Player;
    [HideInInspector] public SteveAI Steve;
    [HideInInspector] public SideEnemy sideEnemy;
    [HideInInspector] public UpEnemy upEnemy;
    [SerializeField] GameObject FadeInObject;
    [SerializeField] GameObject UI;

    private void Awake()
    {
        Instance = this;
        UI.SetActive(false);
        Player = FindObjectOfType<PlayerMovement>();
        Steve = FindObjectOfType<SteveAI>();
        sideEnemy = FindObjectOfType<SideEnemy>();
        upEnemy = FindObjectOfType<UpEnemy>();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void StartGame()
    {
        StartCoroutine(Fade(1.5f, true));
    }

    public void ResetGame()
    {
        EventManager.Broadcast(EVENT.Reset);
        StartGame();
    }

    public void EndGame()
    {
        Steve.steveAlive = false;
        StartCoroutine(Fade(1.5f, false));
        EventManager.Broadcast(EVENT.endGame);
    }


    IEnumerator Fade(float duration, bool FadeIn)
    {
        Color a = Color.black;
        Color b = Color.black;
        b.a = 0;
        while (duration > 0)
        {
            if (FadeIn)
            {
                FadeInObject.GetComponent<SpriteRenderer>().color = Color.Lerp(b, a, duration / 2);

            }
            else
            {
                UI.SetActive(false);
                FadeInObject.GetComponent<SpriteRenderer>().color = Color.Lerp(a, b, Time.deltaTime);
            }
            duration -= Time.deltaTime;
            yield return null;
        }

        if (FadeIn)
        {
            UI.SetActive(true);
            yield return null;

        }
        yield return null;
    }
}
