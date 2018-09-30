using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SideEnemy : MonoBehaviour, IEnemy
{
    private Collider2D boxCollider;
    private bool canStun = true;
    private float stunTimer;
    private float moveValue = 5.0f;

    public string id
    {
        get
        {
            return "GroundE";
        }
    }

    private void Awake()
    {
        boxCollider = GetComponent<Collider2D>();
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

        if (canStun)
            transform.Translate(Vector3.left * Time.deltaTime * moveValue);
        else
            return;




        if (boxCollider.bounds.Intersects(GameManager.Instance.Player.PLcollider))
        {
            GameManager.Instance.Player.stunned = true;
            GameManager.Instance.Player.Recover();
            stunTimer = 5.0f;
            canStun = false;
        }

        if (boxCollider.bounds.Intersects(GameManager.Instance.Steve.Stevebounds))
        {
            Debug.Log("Stun steve");
            GameManager.Instance.Steve.running = false;
            GameManager.Instance.Steve.timer = 2.0f;
            GameManager.Instance.Steve.state = AnimStates.Stunned;
            GameManager.Instance.Steve.GetComponentInChildren<Animator>().SetTrigger("Stunned");
            stunTimer = 2.0f;
            canStun = false;
            Destroy(gameObject);
        }

        if ((GameManager.Instance.Player.transform.position - gameObject.transform.position).magnitude >= 150.0f)
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        
        Destroy(gameObject);
    }


}
