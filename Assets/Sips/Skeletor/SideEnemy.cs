using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEnemy : MonoBehaviour
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

    private void StunEntity(object o)
    {
        //insert logic for stun here plz plox
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
            StartCoroutine(GameManager.Instance.Player.StunRecover(1.5f));
            GameManager.Instance.Player.stunned = true;
            Debug.Log("stunning");
            stunTimer = 5.0f;
            canStun = false;

        }

    }
}
