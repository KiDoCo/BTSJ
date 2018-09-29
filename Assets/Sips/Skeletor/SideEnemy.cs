using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SideEnemy : MonoBehaviour, IEnemy
{
    private Collider2D boxCollider;
    private bool canMove = false;
    private bool canStun = true;
    private float stunTimer;
    private float moveValue = 5.0f;

    private void Awake()
    {
        boxCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {

    }

    private void OnBecameVisible()
    {
        Debug.Log("Visible");
        canMove = true;
    }

    private void Timer()
    {
        if (stunTimer < 0)
        {
            canStun = true;
        }
        else
        {
            stunTimer -= Time.deltaTime;
        }
    }

    private void Update()
    {
        Timer();
        if (!canMove) return;

        if (canStun)
            transform.Translate(Vector3.left * Time.deltaTime * moveValue);
        else
            return;




        if (boxCollider.bounds.Intersects(GameManager.Instance.Player.PLcollider))
        {
            Debug.Log("Getting here");
            GameManager.Instance.Player.stunned = true;
            StartCoroutine(GameManager.Instance.Player.StunRecover(1.5f));

            Debug.Log("stunning");
            stunTimer = 5.0f;
            canStun = false;

        }

        if (boxCollider.bounds.Intersects(GameManager.Instance.Steve.Stevebounds))
        {
            GameManager.Instance.Steve.running = false;
            GameManager.Instance.Steve.timer = 2.0f;
            stunTimer = 5.0f;
            canStun = false;
        }

    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
